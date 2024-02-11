using Hotkeys;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MacroKeys
{

    public class Macro
    {
        public string Name = "none";
        public string Description = "";
        public string Category = "";
        public string FileName = "";
        //public List<string> Actions = new List<string>{ "start", "middle", "end" };
        public string Action = "";
        public bool HotkeyEnabled;
        public string HotkeyKey = "";
        public bool HotkeyCtrl;
        public bool HotkeyAlt;
        public bool HotkeyShift;
        public bool HotkeyWin;
        public bool WaitForModifierRelease;
        public Hotkeys.Hotkey hotkey = new Hotkeys.Hotkey();
        public GlobalHotkey ghk;
        public bool HotkeyError = false;
        public bool MacroError = false;
        public HotkeyPanel hotkeyPanel;
        public bool setupDone = false;
        bool IsSaved = true;
        MainForm Parent;

        public Macro(MainForm parent, string name = "", bool waitForModifierRelease = true)
        {
            Parent = parent;
            ghk = new GlobalHotkey();
            hotkeyPanel = new HotkeyPanel(this);
            hotkeyPanel.textBoxName.Text = name;
            Name = name;
            hotkeyPanel.checkBoxWait.Checked = waitForModifierRelease;
            WaitForModifierRelease = waitForModifierRelease;
            hotkeyPanel.checkBoxEnabled.Checked = HotkeyEnabled;
            hotkeyPanel.comboBoxKey.Text = HotkeyKey;
            hotkeyPanel.checkBoxCtrl.Checked = HotkeyCtrl;
            hotkeyPanel.checkBoxAlt.Checked = HotkeyAlt;
            hotkeyPanel.checkBoxShift.Checked = HotkeyShift;
            hotkeyPanel.checkBoxWin.Checked = HotkeyWin;
            
            hotkeyPanel.textBoxActions.Text = Action;

            hotkeyPanel.textBoxName.TextChanged += NameChanged;
            hotkeyPanel.checkBoxEnabled.MouseClick += EnabledClick;
            hotkeyPanel.checkBoxCtrl.MouseClick += CheckboxModifierClick;
            hotkeyPanel.checkBoxAlt.MouseClick += CheckboxModifierClick;
            hotkeyPanel.checkBoxShift.MouseClick += CheckboxModifierClick;
            hotkeyPanel.checkBoxWin.MouseClick += CheckboxModifierClick;
            hotkeyPanel.checkBoxWait.MouseClick += CheckboxWaitClick;
            hotkeyPanel.textBoxActions.TextChanged += ActionTextChanged;
            hotkeyPanel.comboBoxKey.TextChanged += KeyChanged;
            hotkeyPanel.buttonSave.Click += SaveClick;
            hotkeyPanel.buttonDelete.Click += DeleteClick;
            hotkeyPanel.buttonEditAction.Click += EditActionClick;
            
            UpdateSavedStatus(true);
        }

        private void EditActionClick(object? sender, EventArgs e)
        {
            MacroTextEntry textEntry = new MacroTextEntry(Action);
            DialogResult result = textEntry.ShowDialog();
            if (result == DialogResult.OK)
            {
                hotkeyPanel.textBoxActions.Text = textEntry.TextResult;
            }
        }

        private void DeleteClick(object? sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show($"Are you sure you want to delete {Name}?", "Delete macro and file", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                
            if (result == DialogResult.Yes)
            {
                ghk.Unregister();
                if (File.Exists(FileName))
                {
                    Debug.WriteLine("Deleting file: " + FileName);
                    File.Delete(FileName);
                }
                Parent.DeleteMacro(this);
            }
        }

        private void NameChanged(object? sender, EventArgs e)
        {
            Name = hotkeyPanel.textBoxName.Text;
            UpdateSavedStatus(false);
        }

        private void SaveClick(object? sender, EventArgs e)
        {
            if (FileName == "")
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.InitialDirectory = Path.GetFullPath(".\\Macros");
                saveFileDialog.Filter = "Text File|*.txt|All Files|*.*";
                if (Name.Length > 0)
                {
                    saveFileDialog.FileName = Name;
                }
                DialogResult result = saveFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Save(saveFileDialog.FileName);
                    FileName = saveFileDialog.FileName;
                }
            }
            else
            {
                Save(FileName);
            }
        }

        private void UpdateSavedStatus(bool isSaved)
        {
            IsSaved = isSaved;
            hotkeyPanel.buttonSave.Enabled = !isSaved;
        }

        private void Save(string filename)
        {
            string? dir = Path.GetDirectoryName(filename);
            if (Directory.Exists(dir))
            {
                Debug.WriteLine("Saving to Directory " + dir);
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("Don't add or remove any lines below, the line numbers are used to read the file correctly:");  // Line 0

                stringBuilder.AppendLine("NAME:");                                              // Line 1
                stringBuilder.AppendLine(Name);                                                 // Line 2
                stringBuilder.AppendLine("");                                                   // Line 3

                stringBuilder.AppendLine("CATEGORY:");                                          // Line 4
                stringBuilder.AppendLine(Category);                                             // Line 5
                stringBuilder.AppendLine("");                                                   // Line 6

                stringBuilder.AppendLine("DESCRIPTION:");                                       // Line 7
                stringBuilder.AppendLine(Description);                                          // Line 8
                stringBuilder.AppendLine("");                                                   // Line 9

                stringBuilder.AppendLine("ENABLED:             // Enabled True or False");      // Line 10
                stringBuilder.AppendLine(HotkeyEnabled.ToString());                             // Line 11
                stringBuilder.AppendLine("");                                                   // Line 12

                stringBuilder.AppendLine("KEY:                 // Hotkey Key, see https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.keys?view=windowsdesktop-7.0");  // Line 13
                stringBuilder.AppendLine(HotkeyKey);                                            // Line 14
                stringBuilder.AppendLine("");                                                   // Line 15

                stringBuilder.AppendLine("MODIFIERS:           // Modifiers, choose 0-4 of Ctrl Alt Shift Win"); // Line 16
                stringBuilder.Append(HotkeyCtrl ? "Ctrl ":"");
                stringBuilder.Append(HotkeyAlt ? "Alt ":"");
                stringBuilder.Append(HotkeyShift ? "Shift ":"");
                stringBuilder.AppendLine(HotkeyWin ? "Win ":"");                                // Line 17
                stringBuilder.AppendLine("");                                                   // Line 18

                stringBuilder.AppendLine("WAIT FOR MODIFIER RELEASE: ");                        // Line 19
                stringBuilder.AppendLine(WaitForModifierRelease.ToString());                    // Line 20
                stringBuilder.AppendLine("");                                                   // Line 21

                stringBuilder.AppendLine("ACTION:              // The sequence of keys to fire, for special keys, see https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.sendkeys?view=windowsdesktop-8.0");
                stringBuilder.AppendLine(Action);                                               // Line 23

                Debug.WriteLine($"Saving file {filename}: " + stringBuilder.ToString());
                File.WriteAllText(filename, stringBuilder.ToString());
                UpdateSavedStatus(true);
            }
            else
            {
                Debug.WriteLine("Directory no found " + dir);
            }
        }

        private void KeyChanged(object? sender, EventArgs e)
        {
            Debug.WriteLine($"Key changed: index {hotkeyPanel.comboBoxKey.SelectedIndex} / key {hotkeyPanel.comboBoxKey.Text}");
            HotkeyKey = hotkeyPanel.comboBoxKey.Text;
            UpdateHotkey();
            UpdateSavedStatus(false);
        }

        private void ActionTextChanged(object? sender, EventArgs e)
        {
            Action = hotkeyPanel.textBoxActions.Text;
            UpdateSavedStatus(false);
        }

        private void EnabledClick(object? sender, MouseEventArgs e)
        {
            Debug.WriteLine("Enabled click");
            HotkeyEnabled = hotkeyPanel.checkBoxEnabled.Checked;
            UpdateHotkey();
            UpdateSavedStatus(false);
        }

        public void UpdateHotkey()
        {
            if (setupDone == false)
            {
                Debug.WriteLine("Update hotkey: Setup isn't done yet");
                return;
            }
            if (HotkeyEnabled)
            {
                Debug.WriteLine("Update hotkey: Enabled");
                if (ghk.registered)
                {
                    Debug.WriteLine($"* UnRegistering hotkey {Name}, in order to re-register");
                    ghk.Unregister();
                }
                ghk.SetKey(HotkeyKey);
                ghk.modifier = Modifiers();
                Debug.WriteLine($"* Registering hotkey {Name}");
                if (ghk.Register() == false)
                {
                    hotkeyPanel.comboBoxKey.BackColor = Color.Yellow;
                }
                else
                {
                    hotkeyPanel.comboBoxKey.BackColor = Color.White;
                }

            }
            else
            {
                Debug.WriteLine("Update hotkey: Disabled");
                if (ghk.registered == true)
                {
                    Debug.WriteLine($"* UnRegistering hotkey {Name}");
                    ghk.Unregister();
                }
                else
                {
                    Debug.WriteLine($"* No change to hotkey {Name}, not registered");
                }
            }
        }

        private void CheckboxModifierClick(object? sender, MouseEventArgs e)
        {
            HotkeyCtrl = hotkeyPanel.checkBoxCtrl.Checked;
            HotkeyAlt = hotkeyPanel.checkBoxAlt.Checked;
            HotkeyShift = hotkeyPanel.checkBoxShift.Checked;
            HotkeyWin = hotkeyPanel.checkBoxWin.Checked;
            UpdateHotkey();
            UpdateSavedStatus(false);
        }


        private void CheckboxWaitClick(object? sender, MouseEventArgs e)
        {
            WaitForModifierRelease = hotkeyPanel.checkBoxWait.Checked;
            UpdateSavedStatus(false);
        }

        public int Modifiers() // bool Ctrl, bool Alt, bool Shift, bool Win)
        {
            int result = 0;
            if (HotkeyCtrl) result += (int)KeyModifier.Control;
            if (HotkeyAlt) result += (int)KeyModifier.Alt;
            if (HotkeyShift) result += (int)KeyModifier.Shift;
            if (HotkeyWin) result += (int)KeyModifier.WinKey;
            return result;
        }

        public void FillPanelValues()
        {
            hotkeyPanel.textBoxName.Text = Name;
            hotkeyPanel.textBoxActions.Text = Action;
            hotkeyPanel.checkBoxEnabled.Checked = HotkeyEnabled;
            hotkeyPanel.comboBoxKey.Text = HotkeyKey;
            hotkeyPanel.checkBoxCtrl.Checked = HotkeyCtrl;
            hotkeyPanel.checkBoxAlt.Checked = HotkeyAlt;
            hotkeyPanel.checkBoxShift.Checked = HotkeyShift;
            hotkeyPanel.checkBoxWin.Checked = HotkeyWin;
            hotkeyPanel.checkBoxWait.Checked = WaitForModifierRelease;
            setupDone = true;
            UpdateSavedStatus(true);
        }
    }
}
