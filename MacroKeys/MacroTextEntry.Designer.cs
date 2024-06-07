namespace MacroKeys
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
            panel2 = new Panel();
            buttonAddDelay = new Button();
            numericDelay = new NumericUpDown();
            label6 = new Label();
            panel3 = new Panel();
            label9 = new Label();
            numericMouseMoveY = new NumericUpDown();
            label8 = new Label();
            buttonAddMouseMove = new Button();
            numericMouseMoveX = new NumericUpDown();
            label7 = new Label();
            panel4 = new Panel();
            label10 = new Label();
            numericMousePositionY = new NumericUpDown();
            label11 = new Label();
            buttonAddMousePosition = new Button();
            numericMousePositionX = new NumericUpDown();
            label12 = new Label();
            panel5 = new Panel();
            buttonMouseClickRight = new Button();
            buttonMouseClickMiddle = new Button();
            buttonMouseClickLeft = new Button();
            label15 = new Label();
            panelAddModifiedKeys.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericDelay).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericMouseMoveY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericMouseMoveX).BeginInit();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericMousePositionY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericMousePositionX).BeginInit();
            panel5.SuspendLayout();
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
            buttonOK.Location = new Point(707, 452);
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
            panelAddModifiedKeys.Location = new Point(354, 236);
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
            label5.Location = new Point(528, 239);
            label5.Name = "label5";
            label5.Size = new Size(254, 113);
            label5.TabIndex = 7;
            label5.Text = "Modifiers:\r\n+() Shift   ^() Ctrl   %() Alt\r\n\r\nModified key examples:\r\n+o      =   O\r\n+(ok)   =   OK\r\n^o      =   Ctrl+O";
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(buttonAddDelay);
            panel2.Controls.Add(numericDelay);
            panel2.Controls.Add(label6);
            panel2.Location = new Point(183, 236);
            panel2.Name = "panel2";
            panel2.Size = new Size(165, 116);
            panel2.TabIndex = 8;
            // 
            // buttonAddDelay
            // 
            buttonAddDelay.Location = new Point(84, 27);
            buttonAddDelay.Name = "buttonAddDelay";
            buttonAddDelay.Size = new Size(70, 23);
            buttonAddDelay.TabIndex = 3;
            buttonAddDelay.Text = "Add";
            buttonAddDelay.UseVisualStyleBackColor = true;
            buttonAddDelay.Click += ButtonAddDelay_Click;
            // 
            // numericDelay
            // 
            numericDelay.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            numericDelay.Location = new Point(3, 26);
            numericDelay.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericDelay.Name = "numericDelay";
            numericDelay.Size = new Size(75, 23);
            numericDelay.TabIndex = 2;
            numericDelay.Value = new decimal(new int[] { 200, 0, 0, 0 });
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label6.Location = new Point(3, 6);
            label6.Name = "label6";
            label6.Size = new Size(139, 15);
            label6.TabIndex = 1;
            label6.Text = "Add delay (milliseconds)";
            // 
            // panel3
            // 
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(label9);
            panel3.Controls.Add(numericMouseMoveY);
            panel3.Controls.Add(label8);
            panel3.Controls.Add(buttonAddMouseMove);
            panel3.Controls.Add(numericMouseMoveX);
            panel3.Controls.Add(label7);
            panel3.Location = new Point(12, 359);
            panel3.Name = "panel3";
            panel3.Size = new Size(164, 116);
            panel3.TabIndex = 9;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(6, 56);
            label9.Name = "label9";
            label9.Size = new Size(14, 15);
            label9.TabIndex = 8;
            label9.Text = "Y";
            // 
            // numericMouseMoveY
            // 
            numericMouseMoveY.Location = new Point(26, 54);
            numericMouseMoveY.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            numericMouseMoveY.Minimum = new decimal(new int[] { 20000, 0, 0, int.MinValue });
            numericMouseMoveY.Name = "numericMouseMoveY";
            numericMouseMoveY.Size = new Size(69, 23);
            numericMouseMoveY.TabIndex = 7;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(6, 27);
            label8.Name = "label8";
            label8.Size = new Size(14, 15);
            label8.TabIndex = 6;
            label8.Text = "X";
            // 
            // buttonAddMouseMove
            // 
            buttonAddMouseMove.Location = new Point(101, 25);
            buttonAddMouseMove.Name = "buttonAddMouseMove";
            buttonAddMouseMove.Size = new Size(58, 52);
            buttonAddMouseMove.TabIndex = 5;
            buttonAddMouseMove.Text = "Add";
            buttonAddMouseMove.UseVisualStyleBackColor = true;
            buttonAddMouseMove.Click += ButtonAddMouseMove_Click;
            // 
            // numericMouseMoveX
            // 
            numericMouseMoveX.Location = new Point(26, 25);
            numericMouseMoveX.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            numericMouseMoveX.Minimum = new decimal(new int[] { 20000, 0, 0, int.MinValue });
            numericMouseMoveX.Name = "numericMouseMoveX";
            numericMouseMoveX.Size = new Size(69, 23);
            numericMouseMoveX.TabIndex = 4;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label7.Location = new Point(4, 7);
            label7.Name = "label7";
            label7.Size = new Size(79, 15);
            label7.TabIndex = 1;
            label7.Text = "Move mouse";
            // 
            // panel4
            // 
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Controls.Add(label10);
            panel4.Controls.Add(numericMousePositionY);
            panel4.Controls.Add(label11);
            panel4.Controls.Add(buttonAddMousePosition);
            panel4.Controls.Add(numericMousePositionX);
            panel4.Controls.Add(label12);
            panel4.Location = new Point(183, 359);
            panel4.Name = "panel4";
            panel4.Size = new Size(164, 116);
            panel4.TabIndex = 10;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(6, 56);
            label10.Name = "label10";
            label10.Size = new Size(14, 15);
            label10.TabIndex = 8;
            label10.Text = "Y";
            // 
            // numericMousePositionY
            // 
            numericMousePositionY.Location = new Point(26, 54);
            numericMousePositionY.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            numericMousePositionY.Name = "numericMousePositionY";
            numericMousePositionY.Size = new Size(70, 23);
            numericMousePositionY.TabIndex = 7;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(6, 27);
            label11.Name = "label11";
            label11.Size = new Size(14, 15);
            label11.TabIndex = 6;
            label11.Text = "X";
            // 
            // buttonAddMousePosition
            // 
            buttonAddMousePosition.Location = new Point(102, 25);
            buttonAddMousePosition.Name = "buttonAddMousePosition";
            buttonAddMousePosition.Size = new Size(57, 52);
            buttonAddMousePosition.TabIndex = 5;
            buttonAddMousePosition.Text = "Add";
            buttonAddMousePosition.UseVisualStyleBackColor = true;
            buttonAddMousePosition.Click += ButtonAddMousePosition_Click;
            // 
            // numericMousePositionX
            // 
            numericMousePositionX.Location = new Point(26, 25);
            numericMousePositionX.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            numericMousePositionX.Name = "numericMousePositionX";
            numericMousePositionX.Size = new Size(70, 23);
            numericMousePositionX.TabIndex = 4;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label12.Location = new Point(4, 7);
            label12.Name = "label12";
            label12.Size = new Size(113, 15);
            label12.TabIndex = 1;
            label12.Text = "Set mouse position";
            // 
            // panel5
            // 
            panel5.BorderStyle = BorderStyle.FixedSingle;
            panel5.Controls.Add(buttonMouseClickRight);
            panel5.Controls.Add(buttonMouseClickMiddle);
            panel5.Controls.Add(buttonMouseClickLeft);
            panel5.Controls.Add(label15);
            panel5.Location = new Point(354, 359);
            panel5.Name = "panel5";
            panel5.Size = new Size(164, 116);
            panel5.TabIndex = 11;
            // 
            // buttonMouseClickRight
            // 
            buttonMouseClickRight.Location = new Point(6, 83);
            buttonMouseClickRight.Name = "buttonMouseClickRight";
            buttonMouseClickRight.Size = new Size(75, 23);
            buttonMouseClickRight.TabIndex = 4;
            buttonMouseClickRight.Tag = "3";
            buttonMouseClickRight.Text = "Right";
            buttonMouseClickRight.UseVisualStyleBackColor = true;
            buttonMouseClickRight.Click += ButtonAddMouseClick_Click;
            // 
            // buttonMouseClickMiddle
            // 
            buttonMouseClickMiddle.Location = new Point(6, 54);
            buttonMouseClickMiddle.Name = "buttonMouseClickMiddle";
            buttonMouseClickMiddle.Size = new Size(75, 23);
            buttonMouseClickMiddle.TabIndex = 3;
            buttonMouseClickMiddle.Tag = "2";
            buttonMouseClickMiddle.Text = "Middle";
            buttonMouseClickMiddle.UseVisualStyleBackColor = true;
            buttonMouseClickMiddle.Click += ButtonAddMouseClick_Click;
            // 
            // buttonMouseClickLeft
            // 
            buttonMouseClickLeft.Location = new Point(6, 27);
            buttonMouseClickLeft.Name = "buttonMouseClickLeft";
            buttonMouseClickLeft.Size = new Size(75, 23);
            buttonMouseClickLeft.TabIndex = 2;
            buttonMouseClickLeft.Tag = "1";
            buttonMouseClickLeft.Text = "Left";
            buttonMouseClickLeft.UseVisualStyleBackColor = true;
            buttonMouseClickLeft.Click += ButtonAddMouseClick_Click;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label15.Location = new Point(4, 7);
            label15.Name = "label15";
            label15.Size = new Size(77, 15);
            label15.TabIndex = 1;
            label15.Text = "Mouse clicks";
            // 
            // MacroTextEntry
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(794, 484);
            Controls.Add(panel5);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
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
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericDelay).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericMouseMoveY).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericMouseMoveX).EndInit();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericMousePositionY).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericMousePositionX).EndInit();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
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
        private Panel panel2;
        private Label label6;
        private Button buttonAddDelay;
        private NumericUpDown numericDelay;
        private Panel panel3;
        private Label label8;
        private Button buttonAddMouseMove;
        private NumericUpDown numericMouseMoveX;
        private Label label7;
        private Label label9;
        private NumericUpDown numericMouseMoveY;
        private Panel panel4;
        private Label label10;
        private NumericUpDown numericMousePositionY;
        private Label label11;
        private Button buttonAddMousePosition;
        private NumericUpDown numericMousePositionX;
        private Label label12;
        private Panel panel5;
        private Button buttonMouseClickRight;
        private Button buttonMouseClickMiddle;
        private Button buttonMouseClickLeft;
        private Label label15;
    }
}