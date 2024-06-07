using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroKeys;

internal struct MacroChunk
{
    public string Text;
    public int Delay;

    public MacroChunk(string text, int delay)
    {
        Text = text;
        Delay = delay;
    }
}
