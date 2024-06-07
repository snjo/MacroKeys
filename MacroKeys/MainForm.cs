using Hotkeys;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

[assembly: AssemblyVersion("1.1.*")]

namespace MacroKeys;
[SupportedOSPlatform("windows")]

public partial class MainForm : Form
{
    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
    private const int MOUSEEVENTF_LEFTDOWN = 0x02;
    private const int MOUSEEVENTF_LEFTUP = 0x04;
    private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
    private const int MOUSEEVENTF_RIGHTUP = 0x10;
    private const int MOUSEEVENTF_MIDDLEDOWN = 0x20;
    private const int MOUSEEVENTF_MIDDLEUP = 0x40;
    private const int MOUSEEVENTF_ABSOLUTE = 0x8000;


    public static readonly string ApplicationName = "MacroKeys";
    DateTime lastMacro = DateTime.MinValue;
    readonly List<Macro> Macros = [];
    private List<MacroChunk> MacroChunks = [];
    public string MacroFolder = @".\macros\";
    readonly int NameLine = 2;
    readonly int CategoryLine = 5;
    readonly int DescriptionLine = 8;
    readonly int EnabledLine = 11;
    readonly int KeyLine = 14;
    readonly int ModifiersLine = 17;
    readonly int WaitForModifierReleaseLine = 20;
    readonly int ActionLine = 23;
    public bool StartMinimized = false;
    public bool HideWhenMinimized = false;

    public MainForm()
    {
        InitializeComponent();
        string? folderName = RegistrySetting.LoadStringFromRegistry("Macrofolder");
        StartMinimized = RegistrySetting.LoadBoolFromRegistry("StartMinimized");
        HideWhenMinimized = RegistrySetting.LoadBoolFromRegistry("HideWhenMinimized");
        Debug.WriteLine("Hidden status from registry:" + StartMinimized);
        if (StartMinimized)
        {
            this.WindowState = FormWindowState.Minimized;
            if (HideWhenMinimized)
            {
                HideApplication();
            }
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
                Debug.WriteLine($"hotkey pressed, id {id}, macro {macro.Name} id {macro.ghk.id}");
                if (macro.WaitForModifierRelease && ModifierKeys != Keys.None)
                {
                    delayedActionActive = true;
                    delayedMacro = macro;
                    timerDelayAction.Start();
                }
                else
                {
                    PrepareMacroChunks(macro);
                }
            }
        }
    }

    private void PrepareMacroChunks(Macro macro)
    {
        string delayTag = "{[delay";
        MacroChunks.Clear();
        timerMacroChunkDelay.Stop();

        if (macro.Action.Length == 0)
        {
            StopMacroChunks();
        }

        int index = 0;
        while (index < macro.Action.Length)
        {
            string chunk = macro.Action[index..];
            int foundStart = chunk.IndexOf(delayTag);
            Debug.WriteLine($"index {index}: found start {foundStart}");
            if (index == 0 && foundStart > 0)
            {
                string firstText = chunk[..foundStart];
                Debug.WriteLine($"Adding stuff before first delay: '{firstText}', adding {foundStart} to index {index}");
                // add anything before the first delay
                MacroChunks.Add(new MacroChunk(firstText, 0));
                index+=foundStart;
            }
            else if (foundStart == -1)
            {
                // add whatever's left
                Debug.WriteLine($"Adding what's left: '{chunk}'");
                MacroChunks.Add(new MacroChunk(chunk, 0));
                break;
            }
            else
            {
                int foundEnd = chunk.IndexOf("]}");
                Debug.WriteLine($"Found tag end in chunk at {foundEnd}, text: '{chunk}'");
                if (foundEnd == -1)
                {
                    Debug.WriteLine("Error in delay tag");
                    MacroError(macro);
                    return;
                }

                string timeText = chunk[delayTag.Length..foundEnd];
                int nextDelay = chunk.IndexOf(delayTag,1);
                string chunkAction;
                if (int.TryParse(timeText, out int time))
                {
                    Debug.WriteLine($"nextDelay {nextDelay}");
                    
                    if (nextDelay > 0)
                    {
                        
                        chunkAction = chunk[(foundEnd + 2)..nextDelay];
                    }
                    else
                    {
                        chunkAction = chunk[(foundEnd + 2)..];
                    }
                    Debug.WriteLine($"Adding chunk {MacroChunks.Count}: delay:{time}, action:'{chunkAction}'");
                    MacroChunks.Add(new MacroChunk(chunkAction, time));
                }
                else
                {
                    Debug.WriteLine($"Delay time is invalid: '{timeText}', aborting macro");
                    MacroError(macro);
                    return;
                }
                Debug.WriteLine($"Adding {foundEnd} and chunk lenght {chunkAction.Length} + 2 to index {index}");
                index += foundEnd+chunkAction.Length+2;
                Debug.WriteLine($"new index {index}");
            }
        }

        if (MacroChunks.Count > 0)
        {
            CurrentMacroChunk = 0;
            MacroChunksRunning = true;
            CurrentMacro = macro;
            NextMacroChunk(macro);
        }
    }

    private bool MacroChunksRunning = false;
    int CurrentMacroChunk = 0;
    Macro? CurrentMacro = null;
    private void NextMacroChunk(Macro macro)
    {
        Debug.WriteLine("\n---CHUNK---");
        timerMacroChunkDelay.Stop();
        if (CurrentMacroChunk < MacroChunks.Count)
        {
            Debug.WriteLine($"Running macro {macro.Name} chunk {CurrentMacroChunk}: {MacroChunks[CurrentMacroChunk].Text}");
            
            timerMacroChunkDelay.Interval = Math.Max(MacroChunks[CurrentMacroChunk].Delay, 10);
            timerMacroChunkDelay.Start();
        }
        else
        {
            StopMacroChunks();
        }
        //CurrentMacroChunk++;
    }

    private void TimerMacroChunkTick(object sender, EventArgs e)
    {
        if (CurrentMacro != null)
        {
            SendMacro(CurrentMacro, MacroChunks[CurrentMacroChunk].Text, preventRepeat: false);
            CurrentMacroChunk++;
            NextMacroChunk(CurrentMacro);
        }
    }

    private void StopMacroChunks()
    {
        timerMacroChunkDelay.Stop();
        MacroChunks.Clear();
        MacroChunksRunning = false;
        Debug.WriteLine("Stopping macro chunk output");
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
        if (commandSplit.Length > 0) commandType = commandSplit[0];
        if (commandSplit.Length > 1) commandParameter = commandSplit[1];
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
        else if (commandType.ToLower().Contains("mxy"))
        {
            bool absolute = true;
            if (commandType.ToLower().Contains("mxyr"))
            {
                absolute = false;
            }
            string[] msplit = commandType.Split(',');
            if (msplit.Length >= 3)
            {
                bool mposValid = true;
                if (int.TryParse(msplit[1], out int mx) == false) mposValid = false;
                if (int.TryParse(msplit[2], out int my) == false) mposValid = false;
                if (mposValid)
                {
                    Debug.WriteLine($"Move mouse to {mx}, {my}");
                    if (absolute)
                    {
                        Cursor.Position = new Point(mx, my);
                    }
                    else
                    {
                        Cursor.Position = new Point(mx + Cursor.Position.X, my + Cursor.Position.Y);
                    }
                }
            }
        }
        else if (commandType.ToLower() == "m1")
        {
            MouseClickEvent(1, true, true);
        }
        else if (commandType.ToLower() == "m2")
        {
            MouseClickEvent(2, true, true);
        }
    }

    private void MouseClickEvent(int buttonNumber, bool down, bool up)
    {
        uint buttonDown = 0;
        uint buttonUp = 0;

        if (buttonNumber == 1)
        {
            buttonDown = MOUSEEVENTF_LEFTDOWN;
            buttonUp = MOUSEEVENTF_LEFTUP;
            Debug.WriteLine("Left click");
        }
        else if (buttonNumber == 2)
        {
            buttonDown = MOUSEEVENTF_RIGHTDOWN;
            buttonUp = MOUSEEVENTF_RIGHTUP;
            Debug.WriteLine("Right click");
        }
        else if (buttonNumber == 3)
        {
            buttonDown = MOUSEEVENTF_MIDDLEDOWN;
            buttonUp = MOUSEEVENTF_MIDDLEUP;
            Debug.WriteLine("Middle click");
        }
        uint presses = 0;
        if (down) presses = buttonDown;
        if (up) presses = presses | buttonUp;

        if (presses > 0)
        {
            mouse_event(presses, 0, 0, 0, 0);
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

    private void SendMacro(Macro macro, string action, bool preventRepeat)
    {
        string actionText;
        TimeSpan timeSinceLastMacro = DateTime.Now - lastMacro;
        if (macro == null)
        {
            Debug.WriteLine("Macro is null");
            return;
        }
        if (macro.Action.Length == 0)
        {
            StopMacroChunks();
            return;
        }
        actionText = ParseSpecialCommand(action);
        //if (action.Length > 0)
        //{
        //    actionText = ParseSpecialCommand(action);
        //}
        //else
        //{
        //    actionText = ParseSpecialCommand(macro.Action);
        //}
        Debug.WriteLine("Action text after removing special: " + actionText);
        if (timeSinceLastMacro.TotalMilliseconds > 100 || preventRepeat == false)// || lastMacroUsed != id)
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
                MacroError(macro);
            }
            lastMacro = DateTime.Now;
        }
        else
        {
            Debug.WriteLine($"key held? skipping macro {macro.Name}, time since last macro: {timeSinceLastMacro.TotalMilliseconds}");
        }
    }

    private void MacroError(Macro macro)
    {
        macro.MacroError = true;
        macro.hotkeyPanel.textBoxActions.BackColor = Color.Orange;
        Debug.WriteLine($"Macro {macro.Name} Error");
        notifyIconSysTray.ShowBalloonTip(2000, "Macro error", $"Error in macro {macro.Name}, check that all special characters are enclosed in " + "{}, and button names are valid", ToolTipIcon.Error);
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
                PrepareMacroChunks(delayedMacro);
                //SendMacro(delayedMacro);
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
        Options options = new(MacroFolder, StartMinimized, HideWhenMinimized);
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
            StartMinimized = options.StartMinimized;
            HideWhenMinimized = options.HideWhenMinimized;

            RegistrySetting.SaveSettingToRegistry("StartMinimized", StartMinimized.ToString());
            RegistrySetting.SaveSettingToRegistry("HideWhenMinimized", HideWhenMinimized.ToString());
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
        if (StartMinimized)
        {
            this.WindowState = FormWindowState.Minimized;
            if (HideWhenMinimized)
            {
                Hide();
            }
        }
        timerHide.Stop();
    }

    private void MainForm_SizeChanged(object sender, EventArgs e)
    {
        if (HideWhenMinimized && WindowState == FormWindowState.Minimized)
        {
            HideApplication();
        }
    }
}
