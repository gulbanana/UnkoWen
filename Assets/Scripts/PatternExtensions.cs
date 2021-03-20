using System.Linq;

public static class PatternExtensions
{
    public static void Deconstruct(this string[] array, out string head, out object tail)
    {
        head = array.Length > 0 ? array[0] : null;
        if (array.Length < 2)
        {
            tail = null;
        } 
        else if (array.Length == 2)
        {
            tail = array[1];
        }
        else
        {
            tail = array.Skip(1).ToArray();
        }
    }
}