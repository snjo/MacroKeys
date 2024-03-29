﻿namespace MacroKeys
{
    partial class MacroTextEntry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MacroTextEntry));
            textBox1 = new TextBox();
            buttonOK = new Button();
            checkBoxCtrl = new CheckBox();
            checkBoxAlt = new CheckBox();
            checkBoxShift = new CheckBox();
            panelAddModifiedKeys = new Panel();
            buttonAdd = new Button();
            label2 = new Label();
            textBoxModifiedKeys = new TextBox();
            label1 = new Label();
            panel1 = new Panel();
            buttonDisable = new Button();
            buttonEnable = new Button();
            textBoxCategory = new TextBox();
            label4 = new Label();
            label3 = new Label();
            label5 = new Label();
            panelAddModifiedKeys.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(10, 10);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(776, 82);
            textBox1.TabIndex = 0;
            // 
            // buttonOK
            // 
            buttonOK.Location = new Point(711, 329);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new Size(75, 23);
            buttonOK.TabIndex = 1;
            buttonOK.Text = "OK";
            buttonOK.UseVisualStyleBackColor = true;
            buttonOK.Click += ButtonOK_Click;
            // 
            // checkBoxCtrl
            // 
            checkBoxCtrl.AutoSize = true;
            checkBoxCtrl.Location = new Point(5, 27);
            checkBoxCtrl.Name = "checkBoxCtrl";
            checkBoxCtrl.Size = new Size(45, 19);
            checkBoxCtrl.TabIndex = 2;
            checkBoxCtrl.Text = "Ctrl";
            checkBoxCtrl.UseVisualStyleBackColor = true;
            // 
            // checkBoxAlt
            // 
            checkBoxAlt.AutoSize = true;
            checkBoxAlt.Location = new Point(55, 27);
            checkBoxAlt.Name = "checkBoxAlt";
            checkBoxAlt.Size = new Size(41, 19);
            checkBoxAlt.TabIndex = 3;
            checkBoxAlt.Text = "Alt";
            checkBoxAlt.UseVisualStyleBackColor = true;
            // 
            // checkBoxShift
            // 
            checkBoxShift.AutoSize = true;
            checkBoxShift.Location = new Point(101, 27);
            checkBoxShift.Name = "checkBoxShift";
            checkBoxShift.Size = new Size(50, 19);
            checkBoxShift.TabIndex = 4;
            checkBoxShift.Text = "Shift";
            checkBoxShift.UseVisualStyleBackColor = true;
            // 
            // panelAddModifiedKeys
            // 
            panelAddModifiedKeys.BorderStyle = BorderStyle.FixedSingle;
            panelAddModifiedKeys.Controls.Add(buttonAdd);
            panelAddModifiedKeys.Controls.Add(label2);
            panelAddModifiedKeys.Controls.Add(textBoxModifiedKeys);
            panelAddModifiedKeys.Controls.Add(label1);
            panelAddModifiedKeys.Controls.Add(checkBoxCtrl);
            panelAddModifiedKeys.Controls.Add(checkBoxShift);
            panelAddModifiedKeys.Controls.Add(checkBoxAlt);
            panelAddModifiedKeys.Location = new Point(182, 236);
            panelAddModifiedKeys.Name = "panelAddModifiedKeys";
            panelAddModifiedKeys.Size = new Size(163, 116);
            panelAddModifiedKeys.TabIndex = 5;
            // 
            // buttonAdd
            // 
            buttonAdd.Location = new Point(81, 82);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(71, 23);
            buttonAdd.TabIndex = 8;
            buttonAdd.Text = "Add";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += ButtonAdd_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 55);
            label2.Name = "label2";
            label2.Size = new Size(31, 15);
            label2.TabIndex = 7;
            label2.Text = "Keys";
            // 
            // textBoxModifiedKeys
            // 
            textBoxModifiedKeys.Location = new Point(54, 52);
            textBoxModifiedKeys.Name = "textBoxModifiedKeys";
            textBoxModifiedKeys.Size = new Size(98, 23);
            textBoxModifiedKeys.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.Location = new Point(3, 5);
            label1.Name = "label1";
            label1.Size = new Size(149, 15);
            label1.TabIndex = 5;
            label1.Text = "Add key(s) with modifiers";
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(buttonDisable);
            panel1.Controls.Add(buttonEnable);
            panel1.Controls.Add(textBoxCategory);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Location = new Point(12, 236);
            panel1.Name = "panel1";
            panel1.Size = new Size(164, 116);
            panel1.TabIndex = 6;
            // 
            // buttonDisable
            // 
            buttonDisable.Location = new Point(84, 82);
            buttonDisable.Name = "buttonDisable";
            buttonDisable.Size = new Size(75, 23);
            buttonDisable.TabIndex = 4;
            buttonDisable.Text = "Disable";
            buttonDisable.UseVisualStyleBackColor = true;
            buttonDisable.Click += buttonDisable_Click;
            // 
            // buttonEnable
            // 
            buttonEnable.Location = new Point(6, 82);
            buttonEnable.Name = "buttonEnable";
            buttonEnable.Size = new Size(75, 23);
            buttonEnable.TabIndex = 3;
            buttonEnable.Text = "Enable";
            buttonEnable.UseVisualStyleBackColor = true;
            buttonEnable.Click += buttonEnable_Click;
            // 
            // textBoxCategory
            // 
            textBoxCategory.Location = new Point(6, 53);
            textBoxCategory.Name = "textBoxCategory";
            textBoxCategory.Size = new Size(153, 23);
            textBoxCategory.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(4, 28);
            label4.Name = "label4";
            label4.Size = new Size(91, 15);
            label4.TabIndex = 1;
            label4.Text = "Category name:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label3.Location = new Point(4, 6);
            label3.Name = "label3";
            label3.Size = new Size(123, 15);
            label3.TabIndex = 0;
            label3.Text = "Turn category on/off";
            // 
            // label5
            // 
            label5.Font = new Font("Courier New", 9F);
            label5.Location = new Point(362, 239);
            label5.Name = "label5";
            label5.Size = new Size(254, 113);
            label5.TabIndex = 7;
            label5.Text = "Modifiers:\r\n+() Shift   ^() Ctrl   %() Alt\r\n\r\nModified key examples:\r\n+o      =   O\r\n+(ok)   =   OK\r\n^o      =   Ctrl+O";
            // 
            // MacroTextEntry
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(794, 362);
            Controls.Add(label5);
            Controls.Add(panel1);
            Controls.Add(panelAddModifiedKeys);
            Controls.Add(buttonOK);
            Controls.Add(textBox1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MacroTextEntry";
            Text = "MacroTextEntry";
            panelAddModifiedKeys.ResumeLayout(false);
            panelAddModifiedKeys.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Button buttonOK;
        private CheckBox checkBoxCtrl;
        private CheckBox checkBoxAlt;
        private CheckBox checkBoxShift;
        private Panel panelAddModifiedKeys;
        private Button buttonAdd;
        private Label label2;
        private TextBox textBoxModifiedKeys;
        private Label label1;
        private Panel panel1;
        private Button buttonDisable;
        private Button buttonEnable;
        private TextBox textBoxCategory;
        private Label label4;
        private Label label3;
        private Label label5;
    }
}