namespace MacroKeys
{
    public partial class MacroTextEntry : Form
    {
        public string TextResult = "";
        public MacroTextEntry(string text)
        {
            InitializeComponent();
            textBox1.Text = text;
            TextResult = text;
            AddButtons();
        }

        string[] KeysFunction = {
            "ESC", "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12"};

        string[] KeysCluster = { "BREAK", "SCROLLLOCK", "PRTSCR", "INSERT", "DELETE", "HOME", "END", "PGUP", "PGDN" };

        string[] KeysNumpad = { "NUMLOCK", "MULTIPLY", "DIVIDE", "SUBTRACT", "ADD" };

        string[] KeysNav = { "ENTER", "TAB", "BACKSPACE", "CAPSLOCK", "UP", "DOWN", "LEFT", "RIGHT" };

        string[] KeysSymbols = { "{", "}", "(", ")", "^", "+", "%", "~" };

        private void AddButtons()
        {
            int shortWidth = 50;
            int longWidth = 75;
            int top = textBox1.Bottom + 10;
            int buttonHeight = 25;
            //int left = 10;

            AddButtonRow(KeysFunction, shortWidth, top);
            top += buttonHeight;
            AddButtonRow(KeysNav, longWidth, top);
            top += buttonHeight;
            AddButtonRow(KeysNumpad, longWidth, top);
            top += buttonHeight;
            AddButtonRow(KeysCluster, longWidth, top);
            top += buttonHeight;
            AddButtonRow(KeysSymbols, shortWidth, top);
        }

        private void AddButtonRow(string[] buttons, int buttonWidth, int top)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                Button newButton = new Button();
                newButton.Text = buttons[i];
                newButton.Tag = buttons[i];
                newButton.Location = new Point(10 + i * (buttonWidth + 5), top);
                newButton.Width = buttonWidth;
                newButton.Height = 25;
                newButton.Click += AddKeyString;
                this.Controls.Add(newButton);
                //Debug.WriteLine($"Add button: {buttons[i]}, at {newButton.Location}, size {newButton.Size}");
            }
        }

        private void AddKeyString(object? sender, EventArgs e)
        {
            if (sender is Button button)
            {
                if (button.Tag is string key)
                {
                    string enclosedText = "{" + key + "}";
                    int caret = textBox1.SelectionStart;
                    textBox1.Text = textBox1.Text.Insert(caret, enclosedText);
                    textBox1.SelectionStart = caret + enclosedText.Length;
                    //textBox1.Text += "{" + key + "}";
                }
            }
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            TextResult = textBox1.Text;
            DialogResult = DialogResult.OK;
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            int textLength = textBoxModifiedKeys.Text.Length;
            if (textLength == 0) return;
            string modifiedKeys = "";
            if (checkBoxCtrl.Checked) modifiedKeys += "^";
            if (checkBoxAlt.Checked) modifiedKeys += "%";
            if (checkBoxShift.Checked) modifiedKeys += "+";

            if (textLength > 1)
            {
                modifiedKeys += "(";
            }

            modifiedKeys += textBoxModifiedKeys.Text;

            if (textLength > 1)
            {
                modifiedKeys += ")";
            }
            int caret = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(caret, modifiedKeys);
            textBox1.SelectionStart = caret + modifiedKeys.Length;
        }
    }
}
