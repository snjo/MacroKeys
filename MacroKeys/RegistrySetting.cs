using Microsoft.Win32;
using System.Diagnostics;
using System.Runtime.Versioning;

namespace MacroKeys;
[SupportedOSPlatform("windows")]
public class RegistrySetting
{
    readonly static string RegKeyName = @"SOFTWARE\MacroKeys";

    public static string? LoadStringFromRegistry(string name)
    {
        Debug.WriteLine($"LoadStringFromRegistry: Loading '{name}' as string");
        object? value = LoadValueFromRegistry(name);
        if (value == null) { return null; }
        if (value is string s) { return s; }
        return null;
    }

    public static object? LoadValueFromRegistry(string name)
    {
        RegistryKey RegKey = Registry.CurrentUser.CreateSubKey(RegKeyName);
        object? value = RegKey.GetValue(name);
        if (value == null)
        {
            Debug.WriteLine($"LoadValueFromRegistry: '{name}' is null");
            return null;
        }

        if (value is string || value is bool || value is int)
        {
            if (value.ToString() == "True" || value.ToString() == "False")
            {
                value = bool.Parse(value.ToString() + "");
            }
            Debug.WriteLine($"LoadValueFromRegistry: Loading '{name}' from registry with value: {value}");
            return value;
        }
        Debug.WriteLine($"LoadValueFromRegistry: Setting '{name}' value is of unknown type, not string int or bool");

        RegKey.Close();
        RegKey.Dispose();
        return null;
    }

    public static void SaveSettingToRegistry(string name, string value)
    {
        RegistryKey RegKey = Registry.CurrentUser.CreateSubKey(RegKeyName);

        Debug.WriteLine($"ApplySettingFromRegistry: Saving '{name}' to registry with value: {value}");
        RegKey.SetValue(name, value);

        RegKey.Close();
        RegKey.Dispose();
    }
}
