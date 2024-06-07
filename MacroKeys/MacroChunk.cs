namespace MacroKeys;

internal struct MacroChunk(string text, int delay)
{
    public string Text = text;
    public int Delay = delay;
}
