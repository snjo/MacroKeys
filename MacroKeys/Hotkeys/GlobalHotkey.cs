using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace Hotkeys;
[SupportedOSPlatform("windows")]

public class GlobalHotkey
{
    [DllImport("user32.dll")]
    private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

    [DllImport("user32.dll")]
    private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

    public int modifier;
    public int key;
    private IntPtr hWnd;
    public int id;
    public bool registered;
    private bool validKey;
    public string displayName = "Unnamed";

    public Keys StringToKey(string keystring)
    {
        if (keystring.Length > 0)
        {
            if (keystring.Length == 1)
            {
                char ch = keystring[0];
                validKey = true;
                return (Keys)ch;
            }
            else
            {
                validKey = Enum.TryParse(keystring, out Keys key);
                return (Keys)key;
            }
        }
        return new Keys();
    }

    public void SetKey(string keystring)
    {
        Keys key = StringToKey(keystring);  // assigns validKey
        this.key = (int)key;
    }

    public GlobalHotkey(int modifier, string keystring, Form form, string name = "Unnamed")
    {
        this.modifier = modifier;
        Keys key = StringToKey(keystring);  // assigns validKey
        this.key = (int)key;
        this.hWnd = form.Handle;
        displayName = name;
        id = GetHashCode();
    }

    public GlobalHotkey()
    {
        validKey = false;
    }

    public override int GetHashCode()
    {
        //Debug.WriteLine($"Get hash code: mod:{modifier} key:{key} hwnd:{hWnd.ToInt32()}");
        return modifier ^ key ^ hWnd.ToInt32();
    }

    public bool Register()
    {
        if (registered)
        {
            Debug.WriteLine($"Hotkey {displayName} is already register, release first. hash code: {id}");
            return false;
        }
        id = GetHashCode();
        if (validKey == false)
        {
            registered = false;
            Debug.WriteLine($"Hotkey {displayName} not registered, Validkey false: key:{key}, modifiers:{modifier}");
            return registered;
        }
        if (id != 0)
        {
            registered = RegisterHotKey(hWnd, id, modifier, key);
            Debug.WriteLine($"Registered hotkey {displayName}:{registered} key:{key}, modifiers:{modifier} hash code: {id}");
            return registered;
        }
        else
        {
            registered = false;
            Debug.WriteLine($"Unknown error registering hotkey {displayName}: key:{key}, modifiers:{modifier} hash code: {id}");
            return registered;
        }
    }

    public bool Unregister()
    {
        //Debug.WriteLine("Releasing hotkey: " + key + " / " + modifier);
        registered = false;
        return UnregisterHotKey(hWnd, id);
    }
}
