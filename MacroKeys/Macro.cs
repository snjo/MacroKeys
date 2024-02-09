using Hotkeys;
using System;
using System.Collections.Generic;
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
        public List<string> Actions = new List<string>{ "start", "middle", "end" };
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

        public Macro()
        {
            ghk = new GlobalHotkey();
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
    }
}
