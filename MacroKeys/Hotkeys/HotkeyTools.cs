// add using for the active project's Properties here
using MacroKeys;
using MacroKeys.Properties;
using System.Configuration;
using System.Diagnostics;

namespace Hotkeys
{
    public class HotkeyTools
    {
        public static void LoadHotkeyFromMacro(Macro macro, Form parent)
        {
            macro.ghk = new GlobalHotkey(macro.Modifiers(), macro.HotkeyKey, parent, macro.Name);
        }

        public static bool RegisterHotKey(GlobalHotkey ghk, bool warning = true)
        {
            if (ghk == null) return false;
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
            string warningText = "Could not register hotkeys:";
            List<string> warningKeys = new List<string>();
            foreach (Macro macro in macros)
            {
                LoadHotkeyFromMacro(macro, parent);
                if (RegisterHotKey(macro.ghk, false)) //register the key, add a warning to the list if it fails
                {
                    Debug.WriteLine($"Hotkey registered {macro.Name}");
                }
                else
                {
                    warningKeys.Add(macro.Name);
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
