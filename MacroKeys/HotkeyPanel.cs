namespace MacroKeys
{
    public partial class HotkeyPanel : UserControl
    {
        public Macro Owner;
        public HotkeyPanel(Macro owner)
        {
            InitializeComponent();
            Owner = owner;
        }
    }
}
