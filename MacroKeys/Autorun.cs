using Microsoft.Win32;
using System.Runtime.Versioning;

namespace Autorun;
[SupportedOSPlatform("windows")]
public static class Autorun
{
    private const string autoStartKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";

    public static bool IsEnabled(string ApplicationName)
    {
        using RegistryKey? key = Registry.CurrentUser.OpenSubKey(autoStartKey, false);
        if (key != null)
        {
            return key.GetValue(ApplicationName, null) != null;
        }
        else
        {
            return false;
        }
    }

    public static void Enable(string ApplicationName)
    {
        using RegistryKey? key = Registry.CurrentUser.OpenSubKey(autoStartKey, true);
        key?.SetValue(ApplicationName, "\"" + Application.ExecutablePath + "\"");
    }

    public static void Disable(string ApplicationName)
    {
        using RegistryKey? key = Registry.CurrentUser.OpenSubKey(autoStartKey, true);
        key?.DeleteValue(ApplicationName, false);
    }

    public static bool UpdatePathIfEnabled(string ApplicationName)
    {
        bool enabled = IsEnabled(ApplicationName);
        if (enabled)
        {
            Enable(ApplicationName);
            return true;
        }
        else
        {
            return false;
        }
    }
}