using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MacroKeys
{
    public partial class Options : Form
    {
        public string MacroFolder = "";
        public bool AutorunEnabled = false;

        public Options(string macrofolder)
        {
            InitializeComponent();
            Debug.WriteLine("Setting macro folder text: " + MacroFolder);
            textBoxMacroFolder.Text = macrofolder;
            
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
            DialogResult = DialogResult.OK;
        }
    }
}
