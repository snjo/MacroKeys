namespace MacroKeys
{
    partial class About
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            labelName = new Label();
            label2 = new Label();
            labelVersion = new Label();
            label4 = new Label();
            linkLabel1 = new LinkLabel();
            SuspendLayout();
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelName.Location = new Point(29, 20);
            labelName.Name = "labelName";
            labelName.Size = new Size(93, 21);
            labelName.TabIndex = 0;
            labelName.Text = "MacroKeys";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(30, 44);
            label2.Name = "label2";
            label2.Size = new Size(186, 15);
            label2.TabIndex = 1;
            label2.Text = "By Andreas Aakvik Gogstad (2024)";
            // 
            // labelVersion
            // 
            labelVersion.AutoSize = true;
            labelVersion.Location = new Point(30, 85);
            labelVersion.Name = "labelVersion";
            labelVersion.Size = new Size(45, 15);
            labelVersion.TabIndex = 2;
            labelVersion.Text = "Version";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(30, 122);
            label4.Name = "label4";
            label4.Size = new Size(142, 15);
            label4.TabIndex = 3;
            label4.Text = "Website and source code:";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(30, 147);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(199, 15);
            linkLabel1.TabIndex = 4;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "https://github.com/snjo/MacroKeys";
            linkLabel1.LinkClicked += Link_Clicked;
            // 
            // About
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(279, 198);
            Controls.Add(linkLabel1);
            Controls.Add(label4);
            Controls.Add(labelVersion);
            Controls.Add(label2);
            Controls.Add(labelName);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "About";
            Text = "About";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelName;
        private Label label2;
        private Label labelVersion;
        private Label label4;
        private LinkLabel linkLabel1;
    }
}