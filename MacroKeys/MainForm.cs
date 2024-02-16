using Hotkeys;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Versioning;

[assembly: AssemblyVersion("1.1.*")]

namespace MacroKeys;
[SupportedOSPlatform("windows")]

public partial class MainForm : Form
{
    public static readonly string ApplicationName = "MacroKeys";
    DateTime lastMacro = DateTime.MinValue;
    readonly List<Macro> Macros = [];
    public string MacroFolder = @".\macros\";
    readonly int NameLine = 2;
    readonly int CategoryLine = 5;
    readonly int DescriptionLine = 8;
    readonly int EnabledLine = 11;
    readonly int KeyLine = 14;
    readonly int ModifiersLine = 17;
    readonly int WaitForModifierReleaseLine = 20;
    readonly int ActionLine = 23;
    public bool StartHidden = false;

    public MainForm()
    {
        InitializeComponent();
        string? folderName = RegistrySetting.LoadStringFromRegistry("Macrofolder");
        bool? startHidden = RegistrySetting.LoadBoolFromRegistry("StartHidden");
        Debug.WriteLine("Hidden status from registry:" + startHidden);
        if (startHidden == true)
        {
            StartHidden = true;
            HideApplication();
        }
        else
        {
            ShowApplication();
        }
        if (folderName != null)
        {
            MacroFolder = folderName;
        }

        if (Directory.Exists(MacroFolder) == false)
        {
            DialogResult result = MessageBox.Show("No macro folder found. Do you want to open Options to configure it now?", "Set up macro folder", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Options_Click(this, new EventArgs());
            }
        }
        else
        {
            LoadMacros();
        }

        Autorun.Autorun.UpdatePathIfEnabled(ApplicationName);
    }

    private void HideApplication()
    {
        //Don't use this.ShowInTaskbar = true/false, it breaks hotkeys
        Debug.WriteLine("Hiding application");
        this.WindowState = FormWindowState.Minimized;
        Hide();
    }

    private void ShowApplication()
    {
        Debug.WriteLine("Showing Application");
        Show();
        WindowState = FormWindowState.Normal; // setting Normal here makes it actually show up in front of other windows
    }

    private void LoadMacros()
    {
        UpdateMacroFileList(MacroFolder);
        LoadMacros(MacroFiles);
        AddHotkeyPanels(Macros);
        foreach (Macro macro in Macros)
        {
            HotkeyTools.AssignGlobalHotkeyToMacro(macro, this);
            macro.UpdateHotkey();
        }
    }

    public Dictionary<string, Hotkey> HotkeyList = [];

    readonly List<string> MacroFiles = [];

    private void UpdateMacroFileList(string folder)
    {
        MacroFiles.Clear();
        Debug.WriteLine($"Updating macros from folder: {folder}");
        if (Directory.Exists(folder) == false)
        {
            Debug.WriteLine($"Can't find macro folder {folder}");
            return;
        }

        string[] files = Directory.GetFiles(folder);

        foreach (string file in files)
        {
            if (Path.GetExtension(file) == ".txt")
            {
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

            Macro newMacro = new(this)
            {
                FileName = filename,
                Name = LineWithoutComment(lines, NameLine),
                Category = LineWithoutComment(lines, CategoryLine),
                Description = LineWithoutComment(lines, DescriptionLine),
                HotkeyEnabled = bool.Parse(LineWithoutComment(lines, EnabledLine)),
                HotkeyKey = LineWithoutComment(lines, KeyLine)
            };

            (newMacro.HotkeyCtrl, newMacro.HotkeyAlt, newMacro.HotkeyShift, newMacro.HotkeyWin) = ParseModifiers(LineWithoutComment(lines, ModifiersLine));
            newMacro.WaitForModifierRelease = LineWithoutComment(lines, WaitForModifierReleaseLine).ToLower() == "true";

            newMacro.Action = ReadLines(lines, ActionLine);

            return newMacro;
        }
        return null;
    }

    private static string ReadLines(string[] lines, int startFromLine)
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

    private static string LineWithoutComment(string[] lines, int lineNumber)
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

    private static (bool Ctrl, bool Alt, bool Shift, bool Win) ParseModifiers(string modifierText)
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
            //Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);                  // The key of the hotkey that was pressed.
            //KeyModifier modifier = (KeyModifier)((int)m.LParam & 0xFFFF);       // The modifier of the hotkey that was pressed.
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
    }

    private string specialTagStart = "{[";
    private string specialTagEnd = "]}";
    private string ParseSpecialCommand(string actionText)
    {
        if (actionText.Contains(specialTagStart)) // && commandText.Contains(specialTagEnd))
        {
            //Debug.WriteLine($"Parsing special commands in {commandText}");
            string remainingText = "";
            int loopCount = 0;
            for (int i = 0; i < actionText.Length && loopCount < 100; loopCount++) // iterate if tag end found
            {
                //Debug.WriteLine($"Looking for tags at index {i}");
                int tagStart = actionText.IndexOf(specialTagStart, i);
                if (tagStart < 0)
                {
                    Debug.WriteLine($"No more start tags from index {i}");
                    remainingText += actionText[i..];
                    break;
                }
                else
                {
                    if (tagStart > i)
                    {
                        string addText = actionText[i..tagStart];
                        remainingText += addText;
                        //Debug.WriteLine($"Tag start:{tagStart} > i:{i}, adding text between to remaining: {addText}");
                    }
                    int tagEnd = actionText.IndexOf(specialTagEnd, tagStart);
                    if (tagEnd < 0)
                    {
                        string addText = actionText[(tagStart + specialTagStart.Length)..];
                        //Debug.WriteLine($"No end tag after index {tagStart}, adding text {addText}");
                        remainingText += addText;
                        break;
                    }
                    else
                    {
                        string specialCommand = actionText.Substring(tagStart + specialTagStart.Length, tagEnd - tagStart - specialTagStart.Length);
                        //i = tagEnd + specialTagEnd.Length - 1; // subtract 1 because i++ happens right after this in for loop
                        i = tagEnd + specialTagEnd.Length; // subtract 1 because i++ happens right after this in for loop
                        Debug.WriteLine($"Special command from {tagStart} to {tagEnd}: {specialCommand}, setting i to {i}");
                        RunSpecialCommand(specialCommand);
                    }
                }
            }
            return remainingText;
        }
        else
        {
            return actionText;
        }
    }

    private void RunSpecialCommand(string specialCommand)
    {
        Debug.WriteLine($"Running special command {specialCommand}");
        if (specialCommand.Length == 0)
        {
            Debug.WriteLine("Special comman length is 0");
            return;
        }
        string commandType = "";
        string commandParameter = "";
        string[] commandSplit = specialCommand.Split(':');
        if (commandSplit.Length > 0 ) commandType = commandSplit[0];
        if (commandSplit.Length > 1 ) commandParameter = commandSplit[1];
        Debug.WriteLine($"special command: {commandType}, parameter {commandParameter}");
        if (commandType.ToLower() == "enable")
        {
            Debug.WriteLine("Enable");
            SetCategoryOnOff(commandParameter, true);
        }
        else if (commandType.ToLower() == "disable")
        {
            Debug.WriteLine("Disable");
            SetCategoryOnOff(commandParameter, false);
        }

    }

    private void SetCategoryOnOff(string category, bool enabled)
    {
        foreach (Macro macro in Macros)
        {
            Debug.WriteLine($"check {macro.Name}");
            if (macro.Category.Equals(category, StringComparison.CurrentCultureIgnoreCase) || category.Equals("all", StringComparison.CurrentCultureIgnoreCase))
            {
                if (enabled)
                {
                    macro.Enable();
                }
                else
                {
                    macro.Disable();
                }
            }
        }
    }

    private void SendMacro(Macro macro)
    {
        TimeSpan timeSinceLastMacro = DateTime.Now - lastMacro;
        if (macro.Action.Length == 0) return;
        string actionText = ParseSpecialCommand(macro.Action);
        Debug.WriteLine("Action text after removing special: " + actionText);
        if (timeSinceLastMacro.TotalMilliseconds > 100)// || lastMacroUsed != id)
        {
            try
            {
                SendKeys.Send(actionText);
                macro.MacroError = false;
                macro.hotkeyPanel.textBoxActions.BackColor = Color.White;
                //Debug.WriteLine($"macro {macro.Name} sent, action: {macro.Action}, time since last macro ended: " + timeSinceLastMacro.TotalMilliseconds);
            }
            catch
            {
                macro.MacroError = true;
                macro.hotkeyPanel.textBoxActions.BackColor = Color.Orange;
                Debug.WriteLine($"Macro {macro.Name} Error");
                notifyIconSysTray.ShowBalloonTip(2000, "Macro error", $"Error in macro {macro.Name}, check that all special characters are enclosed in " + "{}, and button names are valid", ToolTipIcon.Error);
            }
            lastMacro = DateTime.Now;
        }
        else
        {
            Debug.WriteLine($"key held? skipping macro {macro.Name}, time since last macro: {timeSinceLastMacro.TotalMilliseconds}");
        }
    }


    bool delayedActionActive = false;
    Macro? delayedMacro = null;
    private void TimerDelayAction_Tick(object sender, EventArgs e)
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
        Macro newMacro = new(parent: this, waitForModifierRelease: true);
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

    public static string BrowseFolderInExplorer(string folder)
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
        BrowseFolderInExplorer(MacroFolder);
    }

    private void ExitClick(object sender, EventArgs e)
    {
        Close();
    }

    private void Options_Click(object sender, EventArgs e)
    {
        string oldMacroFolder = MacroFolder;
        Options options = new(MacroFolder, StartHidden);
        DialogResult result = options.ShowDialog();
        if (result == DialogResult.OK)
        {
            MacroFolder = options.MacroFolder;
            RegistrySetting.SaveSettingToRegistry("Macrofolder", MacroFolder);
            if (options.AutorunEnabled)
            {
                Autorun.Autorun.Enable(ApplicationName);
            }
            else
            {
                Autorun.Autorun.Disable(ApplicationName);
            }
            StartHidden = options.StartHidden;
            RegistrySetting.SaveSettingToRegistry("StartHidden", StartHidden.ToString());
        }
        if (oldMacroFolder != MacroFolder)
        {
            HotkeyTools.ReleaseHotkeys(Macros);
            panelMacros.Controls.Clear();
            Macros.Clear();
            LoadMacros();
        }
    }

    public static void OpenLink(string url)
    {
        Process.Start(new ProcessStartInfo() { FileName = url, UseShellExecute = true });
    }

    private void Documentation_Click(object sender, EventArgs e)
    {
        OpenLink("https://github.com/snjo/MacroKeys/blob/master/README.md");
    }

    private void Website_Click(object sender, EventArgs e)
    {
        OpenLink("https://github.com/snjo/MacroKeys");
    }

    private void About_Click(object sender, EventArgs e)
    {
        using About about = new();
        about.ShowDialog();
    }

    private void NotifyIcon_Click(object sender, EventArgs e)
    {
        this.WindowState = FormWindowState.Normal;
        Show();
        WindowState = FormWindowState.Normal;
    }

    private void timerHide_Tick(object sender, EventArgs e)
    {
        if (StartHidden)
        {
            Hide();
        }
        timerHide.Stop();
    }
}
