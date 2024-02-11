namespace MacroKeys
{
    partial class HotkeyPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            checkBoxEnabled = new CheckBox();
            comboBoxKey = new ComboBox();
            checkBoxCtrl = new CheckBox();
            checkBoxAlt = new CheckBox();
            checkBoxShift = new CheckBox();
            checkBoxWin = new CheckBox();
            textBoxName = new TextBox();
            label1 = new Label();
            textBoxActions = new TextBox();
            checkBoxWait = new CheckBox();
            buttonSave = new Button();
            buttonDelete = new Button();
            buttonEditAction = new Button();
            SuspendLayout();
            // 
            // checkBoxEnabled
            // 
            checkBoxEnabled.Location = new Point(3, 7);
            checkBoxEnabled.Name = "checkBoxEnabled";
            checkBoxEnabled.Size = new Size(84, 19);
            checkBoxEnabled.TabIndex = 0;
            checkBoxEnabled.Text = "Enabled";
            checkBoxEnabled.UseVisualStyleBackColor = true;
            // 
            // comboBoxKey
            // 
            comboBoxKey.FormattingEnabled = true;
            comboBoxKey.Items.AddRange(new object[] { "Back", "Tab", "Return", "Enter", "Pause", "CapsLock", "Escape", "Space", "PageUp", "PageDown", "End", "Home", "Left", "Up", "Right", "Down", "PrintScreen", "Insert", "Delete", "LWin", "RWin", "NumPad0", "NumPad1", "NumPad2", "NumPad3", "NumPad4", "NumPad5", "NumPad6", "NumPad7", "NumPad8", "NumPad9", "Multiply", "Add", "Separator", "Subtract", "Decimal", "Divide", "NumLock", "Scroll", "OemBackslash", "OemCloseBrackets", "Oemcomma", "OemMinus", "OemOpenBrackets", "OemPeriod", "OemPipe", "Oemplus", "OemQuestion", "OemQuotes", "OemSemicolon", "Oemtilde", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12" });
            comboBoxKey.Location = new Point(74, 34);
            comboBoxKey.Name = "comboBoxKey";
            comboBoxKey.Size = new Size(121, 23);
            comboBoxKey.TabIndex = 1;
            // 
            // checkBoxCtrl
            // 
            checkBoxCtrl.AutoSize = true;
            checkBoxCtrl.Location = new Point(201, 36);
            checkBoxCtrl.Name = "checkBoxCtrl";
            checkBoxCtrl.Size = new Size(45, 19);
            checkBoxCtrl.TabIndex = 2;
            checkBoxCtrl.Text = "Ctrl";
            checkBoxCtrl.UseVisualStyleBackColor = true;
            // 
            // checkBoxAlt
            // 
            checkBoxAlt.AutoSize = true;
            checkBoxAlt.Location = new Point(252, 36);
            checkBoxAlt.Name = "checkBoxAlt";
            checkBoxAlt.Size = new Size(41, 19);
            checkBoxAlt.TabIndex = 3;
            checkBoxAlt.Text = "Alt";
            checkBoxAlt.UseVisualStyleBackColor = true;
            // 
            // checkBoxShift
            // 
            checkBoxShift.AutoSize = true;
            checkBoxShift.Location = new Point(299, 36);
            checkBoxShift.Name = "checkBoxShift";
            checkBoxShift.Size = new Size(50, 19);
            checkBoxShift.TabIndex = 4;
            checkBoxShift.Text = "Shift";
            checkBoxShift.UseVisualStyleBackColor = true;
            // 
            // checkBoxWin
            // 
            checkBoxWin.AutoSize = true;
            checkBoxWin.Location = new Point(355, 36);
            checkBoxWin.Name = "checkBoxWin";
            checkBoxWin.Size = new Size(47, 19);
            checkBoxWin.TabIndex = 5;
            checkBoxWin.Text = "Win";
            checkBoxWin.UseVisualStyleBackColor = true;
            // 
            // textBoxName
            // 
            textBoxName.Location = new Point(74, 5);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(172, 23);
            textBoxName.TabIndex = 6;
            textBoxName.Text = "Name";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(23, 63);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 7;
            label1.Text = "Action";
            // 
            // textBoxActions
            // 
            textBoxActions.Location = new Point(74, 60);
            textBoxActions.Name = "textBoxActions";
            textBoxActions.Size = new Size(333, 23);
            textBoxActions.TabIndex = 8;
            // 
            // checkBoxWait
            // 
            checkBoxWait.AutoSize = true;
            checkBoxWait.Location = new Point(252, 9);
            checkBoxWait.Name = "checkBoxWait";
            checkBoxWait.Size = new Size(155, 19);
            checkBoxWait.TabIndex = 9;
            checkBoxWait.Text = "Wait for modifier release";
            checkBoxWait.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            buttonSave.Location = new Point(421, 6);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(31, 23);
            buttonSave.TabIndex = 10;
            buttonSave.Text = "💾";
            buttonSave.UseVisualStyleBackColor = true;
            // 
            // buttonDelete
            // 
            buttonDelete.Location = new Point(421, 33);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(31, 23);
            buttonDelete.TabIndex = 11;
            buttonDelete.Text = "🗑";
            buttonDelete.UseVisualStyleBackColor = true;
            // 
            // buttonEditAction
            // 
            buttonEditAction.Location = new Point(421, 59);
            buttonEditAction.Name = "buttonEditAction";
            buttonEditAction.Size = new Size(31, 23);
            buttonEditAction.TabIndex = 12;
            buttonEditAction.Text = "...";
            buttonEditAction.UseVisualStyleBackColor = true;
            // 
            // HotkeyPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(buttonEditAction);
            Controls.Add(buttonDelete);
            Controls.Add(buttonSave);
            Controls.Add(checkBoxWait);
            Controls.Add(textBoxActions);
            Controls.Add(label1);
            Controls.Add(textBoxName);
            Controls.Add(checkBoxWin);
            Controls.Add(checkBoxShift);
            Controls.Add(checkBoxAlt);
            Controls.Add(checkBoxCtrl);
            Controls.Add(comboBoxKey);
            Controls.Add(checkBoxEnabled);
            Name = "HotkeyPanel";
            Size = new Size(455, 87);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        public CheckBox checkBoxEnabled;
        public ComboBox comboBoxKey;
        public CheckBox checkBoxCtrl;
        public CheckBox checkBoxAlt;
        public CheckBox checkBoxShift;
        public CheckBox checkBoxWin;
        public TextBox textBoxName;
        public TextBox textBoxActions;
        public CheckBox checkBoxWait;
        public Button buttonSave;
        public Button buttonDelete;
        public Button buttonEditAction;
    }
}
