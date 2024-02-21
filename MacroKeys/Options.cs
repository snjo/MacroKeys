using System.Diagnostics;
using System.Runtime.Versioning;

namespace MacroKeys;
[SupportedOSPlatform("windows")]

public partial class Options : Form
{
    public string MacroFolder = "";
    public bool AutorunEnabled = false;
    public bool StartMinimized = false;
    public bool HideWhenMinimized = false;

    public Options(string macrofolder, bool startHidden, bool hideWhenMinimized)
    {
        InitializeComponent();
        Debug.WriteLine("Setting macro folder text: " + MacroFolder);
        textBoxMacroFolder.Text = macrofolder;
        checkBoxStartMinimized.Checked = startHidden;
        checkBoxHideWhenMinimized.Checked = hideWhenMinimized;

        if (Autorun.Autorun.IsEnabled(MainForm.ApplicationName))
        {
            checkBoxAutorun.Checked = true;
        }
    }

    private void ButtonSelectFolder_Click(object sender, EventArgs e)
    {
        DialogResult result = folderBrowserDialog1.ShowDialog();
        if (result == DialogResult.OK)
        {
            textBoxMacroFolder.Text = folderBrowserDialog1.SelectedPath;
        }
    }

    private void ButtonOK_Click(object sender, EventArgs e)
    {
        MacroFolder = textBoxMacroFolder.Text;
        AutorunEnabled = checkBoxAutorun.Checked;
        StartMinimized = checkBoxStartMinimized.Checked;
        HideWhenMinimized= checkBoxHideWhenMinimized.Checked;
        DialogResult = DialogResult.OK;
    }
}
