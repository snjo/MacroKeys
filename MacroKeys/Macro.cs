using Hotkeys;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        bool setupDone = false;
        bool IsSaved = true;

        public Macro()
        {
            ghk = new GlobalHotkey();
            hotkeyPanel = new HotkeyPanel(this);
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
            UpdateSavedStatus(true);
        }

        private void NameChanged(object? sender, EventArgs e)
        {
            Name = hotkeyPanel.textBoxName.Text;
            UpdateSavedStatus(false);
        }

        private void SaveClick(object? sender, EventArgs e)
        {
            Save(FileName);
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
                stringBuilder.AppendLine($"{Name} // Name");
                stringBuilder.AppendLine($"{Category} // Category");
                stringBuilder.AppendLine($"{Description} // Description");
                stringBuilder.AppendLine($"{HotkeyEnabled.ToString()} // Enabled True or False");
                stringBuilder.AppendLine($"{HotkeyKey} // Hotkey Key, see https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.keys?view=windowsdesktop-7.0");
                stringBuilder.Append(HotkeyCtrl ? "Ctrl ":"");
                stringBuilder.Append(HotkeyAlt ? "Alt ":"");
                stringBuilder.Append(HotkeyShift ? "Shift ":"");
                stringBuilder.Append(HotkeyWin ? "Win ":"");
                stringBuilder.AppendLine(" // Modifiers, choose 0-4 of Ctrl Alt Shift Win");
                stringBuilder.AppendLine($"{WaitForModifierRelease.ToString()} // Wait for all modifiers to be released before firing the hotkey, True or False");
                stringBuilder.Append(Action);
                stringBuilder.AppendLine(" // The sequence of keys to fire, for special keys, see https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.sendkeys?view=windowsdesktop-8.0");
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
            UpdateHotkey();
            UpdateSavedStatus(false);
        }

        private void UpdateHotkey()
        {
            if (setupDone == false) return;
            if (hotkeyPanel.checkBoxEnabled.Checked)
            {
                if (ghk.registered)
                {
                    Debug.WriteLine($"* UnRegistering hotkey {Name}, in order to re-register");
                    ghk.Unregister();
                }
                ghk.SetKey(HotkeyKey);
                ghk.modifier = Modifiers();
                Debug.WriteLine($"* Registering hotkey {Name}");
                ghk.Register();

            }
            else
            {
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
