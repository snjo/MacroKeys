using Hotkeys;
using MacroKeys.Properties;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace MacroKeys
{
    public partial class MainForm : Form
    {
        DateTime lastKeypress = DateTime.MinValue;
        int lastMacroUsed = -1;
        List<Macro> Macros = [];
        string MacroFolder = @".\macros\";
        int NameLine = 2;
        int CategoryLine = 5;
        int DescriptionLine = 8;
        int EnabledLine = 11;
        int KeyLine = 14;
        int ModifiersLine = 17;
        int WaitForModifierReleaseLine = 20;
        // Line 7 is for Action comment, so // can be included in Action
        int ActionLine = 23;

        public MainForm()
        {
            InitializeComponent();
            UpdateMacroFileList(MacroFolder);
            LoadMacros(MacroFiles);
            //ListMacros(Macros);
            AddHotkeyPanels(Macros);
            foreach (Macro macro in Macros)
            {
                //HotkeyTools.RegisterHotkeyFromMacro(this, macro);
                HotkeyTools.AssignGlobalHotkeyToMacro(macro, this);
                macro.UpdateHotkey();
            }
        }

        public Dictionary<string, Hotkey> HotkeyList = new Dictionary<string, Hotkey>();

        List<string> MacroFiles = [];

        private void UpdateMacroFileList(string folder)
        {
            Debug.WriteLine($"Updating macros from folder: {folder}");
            if (Directory.Exists(folder) == false)
            {
                Debug.WriteLine($"Can't find macro folder {folder}");
                return;
            }
            MacroFiles.Clear();
            string[] files = Directory.GetFiles(folder);

            foreach (string file in files)
            {
                if (Path.GetExtension(file) == ".txt")
                {
                    Debug.WriteLine($"Adding macro file {file}");
                    MacroFiles.Add(file);
                }
            }
        }

        private void LoadMacros(List<string> macros)
        {
            foreach (string macrofile in macros)
            {
                Debug.WriteLine($"Loading macro file: {macrofile}");
                Macro? newMacro = LoadMacro(macrofile);
                if (newMacro != null)
                {
                    Macros.Add(newMacro);
                }
            }
        }

        private Macro? LoadMacro(string filename)
        {
            if (File.Exists(filename))
            {

                string[] lines = File.ReadAllLines(filename);
                if (lines.Length < ActionLine + 1) return null;

                Macro newMacro = new Macro(this);
                newMacro.FileName = filename;
                newMacro.Name = LineWithoutComment(lines, NameLine);
                newMacro.Category = LineWithoutComment(lines, CategoryLine);
                newMacro.Description = LineWithoutComment(lines, DescriptionLine);
                newMacro.HotkeyEnabled = bool.Parse(LineWithoutComment(lines, EnabledLine));
                newMacro.HotkeyKey = LineWithoutComment(lines, KeyLine);

                (newMacro.HotkeyCtrl, newMacro.HotkeyAlt, newMacro.HotkeyShift, newMacro.HotkeyWin) = ParseModifiers(LineWithoutComment(lines, ModifiersLine));
                newMacro.WaitForModifierRelease = LineWithoutComment(lines, WaitForModifierReleaseLine).ToLower() == "true";

                newMacro.Action = ReadLines(lines, ActionLine);

                Debug.WriteLine($"Added macro {newMacro.Name}, in category {newMacro.Category}, {newMacro.HotkeyKey} {newMacro.HotkeyCtrl}");
                return newMacro;
            }
            return null;
        }

        private string ReadLines(string[] lines, int startFromLine)
        {
            string result = "";
            for (int i = startFromLine; i < lines.Length; i++)
            {
                result += lines[i];
                if (i < lines.Length - 1)
                {
                    result += Environment.NewLine;
                }
            }
            return result;
        }

        private string LineWithoutComment(string[] lines, int lineNumber)
        {
            if (lineNumber >= lines.Length) return "";
            string[] split = lines[lineNumber].Split("//");
            if (split.Length > 0)
            {
                return split[0].Trim();
            }
            else
            {
                return "";
            }
        }

        private void AddHotkeyPanels(List<Macro> macros)
        {
            for (int i = 0; i < macros.Count; i++)
            {
                HotkeyPanel hkp = macros[i].hotkeyPanel;
                panelMacros.Controls.Add(hkp);
                macros[i].FillPanelValues();
                hkp.Location = new Point(2, 2 + (i * hkp.Height + 2));
            }
        }

        private static void UpdateHotkeyPanelLocations(List<Macro> macros)
        {
            for (int i = 0; i < macros.Count; i++)
            {
                HotkeyPanel hkp = macros[i].hotkeyPanel;
                hkp.Location = new Point(2, 2 + (i * (hkp.Height + 2)));
            }
        }

        private (bool Ctrl, bool Alt, bool Shift, bool Win) ParseModifiers(string modifierText)
        {
            return (modifierText.Contains("Ctrl"), modifierText.Contains("Alt"), modifierText.Contains("Shift"), modifierText.Contains("Win"));
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            HotkeyTools.ReleaseHotkeys(Macros);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == Hotkeys.Constants.WM_HOTKEY_MSG_ID)
            {
                Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);                  // The key of the hotkey that was pressed.
                KeyModifier modifier = (KeyModifier)((int)m.LParam & 0xFFFF);       // The modifier of the hotkey that was pressed.
                int id = m.WParam.ToInt32();                                        // The id of the hotkey that was pressed.
                HandleHotkey(id);
            }
        }

        private void HandleHotkey(int id)
        {
            foreach (Macro macro in Macros)
            {
                //Debug.WriteLine($"hotkey id {id}, macro id {macro.ghk.id}");
                if (id == macro.ghk.id)
                {
                    if (macro.WaitForModifierRelease && ModifierKeys != Keys.None)
                    {
                        delayedActionActive = true;
                        delayedMacro = macro;
                        timerDelayAction.Start();
                    }
                    else
                    {
                        SendMacro(macro);
                    }
                }
            }
        }


        private void SendMacro(Macro macro)
        {
            TimeSpan timeSinceLastKeyPress = DateTime.Now - lastKeypress;
            if (timeSinceLastKeyPress.TotalMilliseconds > 300)// || lastMacroUsed != id)
            {
                try
                {
                    SendKeys.Send(macro.Action);
                    macro.MacroError = false;
                    macro.hotkeyPanel.textBoxActions.BackColor = Color.White;
                }
                catch
                {
                    macro.MacroError = true;
                    macro.hotkeyPanel.textBoxActions.BackColor = Color.Orange;
                    Debug.WriteLine("Macro Error");
                }
                lastKeypress = DateTime.Now;
                //lastMacroUsed = id;
                //Debug.WriteLine($"sent macro {macro.Name}");
            }
            else
            {
                //Debug.WriteLine("key held?");
            }
        }


        bool delayedActionActive = false;
        Macro? delayedMacro = null;
        private void timerDelayAction_Tick(object sender, EventArgs e)
        {
            if (delayedActionActive && delayedMacro != null)
            {
                if (ModifierKeys == Keys.None)
                {
                    timerDelayAction.Stop();
                    delayedActionActive = false;
                    SendMacro(delayedMacro);
                    delayedMacro = null;
                }
            }
            else
            {
                timerDelayAction.Stop();
            }
        }

        private void NewMacroClick(object sender, EventArgs e)
        {
            Macro newMacro = new Macro(this, "", true);
            //newMacro.WaitForModifierRelease = true;
            Macros.Add(newMacro);
            panelMacros.Controls.Add(newMacro.hotkeyPanel);
            UpdateHotkeyPanelLocations(Macros);
            newMacro.setupDone = true;
            HotkeyTools.RegisterHotkeyFromMacro(this, newMacro);
        }

        public void DeleteMacro(Macro macro)
        {
            Macros.Remove(macro);
            panelMacros.Controls.Remove(macro.hotkeyPanel);
            UpdateHotkeyPanelLocations(Macros);
        }

        public string BrowseFolderInExplorer(string folder)
        {
            if (folder.Length < 1)
            {
                folder = ".";
            }
            if (Directory.Exists(folder))
            {
                Debug.WriteLine("Opening folder: " + folder);
                Process.Start(new ProcessStartInfo() { FileName = folder, UseShellExecute = true });
            }
            else
            {
                Debug.WriteLine("Can't open folder " + folder);
            }

            return folder;
        }

        private void OpenFolderClick(object sender, EventArgs e)
        {
            BrowseFolderInExplorer(MacroFolder);//Path.GetFullPath(MacroFolder));
        }

        private void ExitClick(object sender, EventArgs e)
        {
            Close();
        }

        // https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.sendkeys.send?view=windowsdesktop-7.0

        // https://stackoverflow.com/questions/18299216/send-special-character-with-sendkeys
        // string txt = Regex.Replace(txt1.Text, "[+^%~()]", "{$0}");
        // SendKeys.Send(txt);
        //
        // foreach (char c in pString) {
        //   if (c.ToString() == "(")
        //      SendKeys.SendWait("{(}");
        //   else if (c.ToString() == ")")
        //      SendKeys.SendWait("{)}");
        //   else if (c.ToString() == "^")
        //      SendKeys.SendWait("{^}");
        //   else if (c.ToString() == "+")
        //      SendKeys.SendWait("{+}");
        //   else if (c.ToString() == "%")
        //      SendKeys.SendWait("{%}");
        //   else if (c.ToString() == "~")
        //      SendKeys.SendWait("{~}");
        //   else if (c.ToString() == "{")
        //      SendKeys.SendWait("{{}");
        //   else if (c.ToString() == "}")
        //      SendKeys.SendWait("{}}");
        //   else
        //      SendKeys.SendWait(c.ToString());
        // }
    }
}
