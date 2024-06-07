using System.Runtime.Versioning;

namespace MacroKeys;
[SupportedOSPlatform("windows")]

public partial class About : Form
{

    public About()
    {
        InitializeComponent();
        labelVersion.Text = "Version " + ProductVersion;
    }

    private void Link_Clicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        MainForm.OpenLink("https://github.com/snjo/MacroKeys");
    }
}
