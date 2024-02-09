using Hotkeys;
using MacroKeys.Properties;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace MacroKeys
{
    public partial class MainForm : Form
    {
        DateTime lastKeypress = DateTime.MinValue;
        int lastMacroUsed = -1;
        List<Macro> Macros = [];
        string MacroFolder = @".\macros\";
        int NameLine = 0;
        int CategoryLine = 1;
        int DescriptionLine = 2;
        int EnabledLine = 3;
        int KeyLine = 4;
        int ModifiersLine = 5;
        int WaitForModifierReleaseLine = 6;
        int ActionLine = 7;

        public MainForm()
        {
            InitializeComponent();
            UpdateMacroFileList(MacroFolder);
            LoadMacros(MacroFiles);
            ListMacros(Macros);           
            HotkeyTools.RegisterHotkeys(Macros, this);            
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
                if (lines.Length < 7) return null;

                Macro newMacro = new Macro();
                newMacro.Name = LineWithoutComment(lines, NameLine);
                newMacro.Category = LineWithoutComment(lines, CategoryLine);
                newMacro.Description = LineWithoutComment(lines, DescriptionLine);
                newMacro.HotkeyEnabled = bool.Parse(LineWithoutComment(lines, EnabledLine));
                newMacro.HotkeyKey = LineWithoutComment(lines, KeyLine);

                (newMacro.HotkeyCtrl, newMacro.HotkeyAlt, newMacro.HotkeyShift, newMacro.HotkeyWin) = ParseModifiers(LineWithoutComment(lines, ModifiersLine));
                newMacro.WaitForModifierRelease = LineWithoutComment(lines, WaitForModifierReleaseLine).ToLower() == "true";

                newMacro.Action = LineWithoutComment(lines, ActionLine);

                Debug.WriteLine($"Added macro {newMacro.Name}, in category {newMacro.Category}, {newMacro.HotkeyKey} {newMacro.HotkeyCtrl}");
                return newMacro;
            }
            return null;
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

        private void ListMacros(List<Macro> macros)
        {
            textBoxLog.Clear();
            foreach (Macro macro in macros)
            {
                string keyWithMods = "";
                if (macro.HotkeyCtrl) keyWithMods += "Ctrl+";
                if (macro.HotkeyAlt) keyWithMods += "Alt+";
                if (macro.HotkeyShift) keyWithMods += "Shift+";
                if (macro.HotkeyWin) keyWithMods += "Win+";
                string enabled = macro.HotkeyEnabled? "Enabled" : "Disabled";
                if (macro.HotkeyError) enabled = "Hotkey Error";
                if (macro.MacroError) enabled += ", Macro Action Error";
                keyWithMods += macro.HotkeyKey;
                textBoxLog.Text += $"{macro.Name} ({macro.Category}) - {enabled}{Environment.NewLine}" +
                    $"{macro.Description}{Environment.NewLine}" +
                    $"{keyWithMods}{Environment.NewLine}{macro.Action}" +
                    $"{Environment.NewLine}{Environment.NewLine}";
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
            ListMacros(Macros);
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
                }
                catch
                {
                    macro.MacroError = true;
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
