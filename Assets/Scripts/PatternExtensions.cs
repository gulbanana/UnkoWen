public static class PatternExtensions
{
    public static void Deconstruct(this string[] array, out string e0, out string e1)
    {
        e0 = array.Length > 0 ? array[0] : null;
        e1 = array.Length > 1 ? array[1] : null;
    }
}