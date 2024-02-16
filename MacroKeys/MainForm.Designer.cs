namespace MacroKeys
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            timerDelayAction = new System.Windows.Forms.Timer(components);
            panelMacros = new Panel();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            newMacroToolStripMenuItem = new ToolStripMenuItem();
            openMacroFolderToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            documentationToolStripMenuItem = new ToolStripMenuItem();
            websiteToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            notifyIconSysTray = new NotifyIcon(components);
            timerHide = new System.Windows.Forms.Timer(components);
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // timerDelayAction
            // 
            timerDelayAction.Interval = 10;
            timerDelayAction.Tick += TimerDelayAction_Tick;
            // 
            // panelMacros
            // 
            panelMacros.AutoScroll = true;
            panelMacros.Dock = DockStyle.Fill;
            panelMacros.Location = new Point(0, 24);
            panelMacros.Name = "panelMacros";
            panelMacros.Size = new Size(560, 483);
            panelMacros.TabIndex = 2;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, optionsToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(560, 24);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newMacroToolStripMenuItem, openMacroFolderToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // newMacroToolStripMenuItem
            // 
            newMacroToolStripMenuItem.Name = "newMacroToolStripMenuItem";
            newMacroToolStripMenuItem.Size = new Size(174, 22);
            newMacroToolStripMenuItem.Text = "New Macro";
            newMacroToolStripMenuItem.Click += NewMacroClick;
            // 
            // openMacroFolderToolStripMenuItem
            // 
            openMacroFolderToolStripMenuItem.Name = "openMacroFolderToolStripMenuItem";
            openMacroFolderToolStripMenuItem.Size = new Size(174, 22);
            openMacroFolderToolStripMenuItem.Text = "Open Macro folder";
            openMacroFolderToolStripMenuItem.Click += OpenFolderClick;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(174, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += ExitClick;
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(61, 20);
            optionsToolStripMenuItem.Text = "Options";
            optionsToolStripMenuItem.Click += Options_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { documentationToolStripMenuItem, websiteToolStripMenuItem, aboutToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "Help";
            // 
            // documentationToolStripMenuItem
            // 
            documentationToolStripMenuItem.Name = "documentationToolStripMenuItem";
            documentationToolStripMenuItem.Size = new Size(206, 22);
            documentationToolStripMenuItem.Text = "Documentation";
            documentationToolStripMenuItem.Click += Documentation_Click;
            // 
            // websiteToolStripMenuItem
            // 
            websiteToolStripMenuItem.Name = "websiteToolStripMenuItem";
            websiteToolStripMenuItem.Size = new Size(206, 22);
            websiteToolStripMenuItem.Text = "Website and source code";
            websiteToolStripMenuItem.Click += Website_Click;
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(206, 22);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += About_Click;
            // 
            // notifyIconSysTray
            // 
            notifyIconSysTray.Icon = (Icon)resources.GetObject("notifyIconSysTray.Icon");
            notifyIconSysTray.Text = "MacroKeys";
            notifyIconSysTray.Visible = true;
            notifyIconSysTray.Click += NotifyIcon_Click;
            notifyIconSysTray.DoubleClick += NotifyIcon_Click;
            // 
            // timerHide
            // 
            timerHide.Enabled = true;
            timerHide.Tick += timerHide_Tick;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(560, 507);
            Controls.Add(panelMacros);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "Macro Keys";
            FormClosing += Form1_FormClosing;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Timer timerDelayAction;
        private Panel panelMacros;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newMacroToolStripMenuItem;
        private ToolStripMenuItem openMacroFolderToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem documentationToolStripMenuItem;
        private ToolStripMenuItem websiteToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private NotifyIcon notifyIconSysTray;
        private System.Windows.Forms.Timer timerHide;
    }
}
