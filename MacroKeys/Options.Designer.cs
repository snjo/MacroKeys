namespace MacroKeys
{
    partial class Options
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
            label1 = new Label();
            checkBoxAutorun = new CheckBox();
            textBoxMacroFolder = new TextBox();
            buttonSelectFolder = new Button();
            button2 = new Button();
            button3 = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 43);
            label1.Name = "label1";
            label1.Size = new Size(75, 15);
            label1.TabIndex = 0;
            label1.Text = "Macro folder";
            // 
            // checkBoxAutorun
            // 
            checkBoxAutorun.AutoSize = true;
            checkBoxAutorun.Location = new Point(12, 12);
            checkBoxAutorun.Name = "checkBoxAutorun";
            checkBoxAutorun.Size = new Size(242, 19);
            checkBoxAutorun.TabIndex = 1;
            checkBoxAutorun.Text = "Launch application when Windows starts";
            checkBoxAutorun.UseVisualStyleBackColor = true;
            // 
            // textBoxMacroFolder
            // 
            textBoxMacroFolder.Location = new Point(93, 40);
            textBoxMacroFolder.Name = "textBoxMacroFolder";
            textBoxMacroFolder.Size = new Size(277, 23);
            textBoxMacroFolder.TabIndex = 2;
            // 
            // buttonSelectFolder
            // 
            buttonSelectFolder.Location = new Point(376, 40);
            buttonSelectFolder.Name = "buttonSelectFolder";
            buttonSelectFolder.Size = new Size(75, 23);
            buttonSelectFolder.TabIndex = 3;
            buttonSelectFolder.Text = "Select";
            buttonSelectFolder.UseVisualStyleBackColor = true;
            buttonSelectFolder.Click += ButtonSelectFolder_Click;
            // 
            // button2
            // 
            button2.Location = new Point(411, 129);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 4;
            button2.Text = "OK";
            button2.UseVisualStyleBackColor = true;
            button2.Click += ButtonOK_Click;
            // 
            // button3
            // 
            button3.DialogResult = DialogResult.Cancel;
            button3.Location = new Point(330, 129);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 5;
            button3.Text = "Cancel";
            button3.UseVisualStyleBackColor = true;
            // 
            // Options
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(498, 164);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(buttonSelectFolder);
            Controls.Add(textBoxMacroFolder);
            Controls.Add(checkBoxAutorun);
            Controls.Add(label1);
            Name = "Options";
            Text = "Options";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private CheckBox checkBoxAutorun;
        private TextBox textBoxMacroFolder;
        private Button buttonSelectFolder;
        private Button button2;
        private Button button3;
        private FolderBrowserDialog folderBrowserDialog1;
    }
}