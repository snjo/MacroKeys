using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
