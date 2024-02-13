// add using for the active project's Properties here
using MacroKeys;
using System.Diagnostics;

namespace Hotkeys
{
    public class HotkeyTools
    {
        public static void AssignGlobalHotkeyToMacro(Macro macro, Form parent)
        {
            string key = macro.HotkeyKey;
            if (key.Length == 1) key = key.ToUpper(); // hotkey A-Z must be upper case
            macro.ghk = new GlobalHotkey(macro.Modifiers(), key, parent, macro.Name);
        }

        public static bool RegisterHotKey(GlobalHotkey ghk, bool warning = true)
        {
            Debug.WriteLine($"RegisterHotKey: name:{ghk.displayName}");
            if (ghk == null)
            {
                Debug.WriteLine("RegisterHotkey: ghk is null");
                return false;
            }
            if (ghk.Register())
            {
                Debug.WriteLine("Registered hotkey named " + ghk.displayName + ", key: " + ghk.key + ", modifiers:" + ghk.modifier);
                return true;
            }
            else
            {
                if (ghk != null)
                {
                    if (warning) MessageBox.Show("Could not register hotkey named " + ghk.displayName + ", key " + ghk.key);
                }
                else
                {
                    if (warning) MessageBox.Show("Could not register unknown hotkey");
                }
                return false;
            }
        }

        public static string[] RegisterHotkeys(List<Macro> macros, Form parent, bool warning = false)
        {
            Debug.WriteLine($"RegisterHotkeys: items: {macros.Count}");
            string warningText = "Could not register hotkeys:";
            List<string> warningKeys = new List<string>();
            foreach (Macro macro in macros)
            {
                string? addWarning = RegisterHotkeyFromMacro(parent, macro);
                if (addWarning != null)
                {
                    warningKeys.Add(addWarning);
                }
            }

            if (warningKeys.Count > 0)
            {
                foreach (string key in warningKeys)
                {
                    warningText += Environment.NewLine + key;
                }
                Debug.WriteLine(warningText);
                if (warning)
                    MessageBox.Show(warningText);
            }

            return warningKeys.ToArray();
        }

        public static string? RegisterHotkeyFromMacro(Form parent, Macro macro)
        {
            Debug.WriteLine($"RegisterHotkeyFromMacro: name:{macro.Name}");
            AssignGlobalHotkeyToMacro(macro, parent);
            if (macro.HotkeyEnabled)
            {
                if (RegisterHotKey(macro.ghk, false)) //register the key, add a warning to the list if it fails
                {
                    Debug.WriteLine($"Hotkey registered {macro.Name}");
                    macro.HotkeyError = false;
                }
                else
                {
                    Debug.WriteLine($"Hotkey error, name:{macro.Name}, key:{macro.HotkeyKey}");
                    macro.HotkeyError = true;
                    return macro.Name;
                }
            }
            return null;
        }

        public static void ReleaseHotkeys(List<Macro> macros)
        {
            foreach (Macro macro in macros)
            {
                ReleaseHotkey(macro);
            }
        }

        public static void ReleaseHotkey(Macro macro)
        {
            if (macro.ghk != null)
            {
                macro.ghk.Unregister();
            }
        }

        public static void UpdateHotkeys(List<Macro> macros, Form parent)
        {
            ReleaseHotkeys(macros);
            RegisterHotkeys(macros, parent, true);
        }
    }
}
