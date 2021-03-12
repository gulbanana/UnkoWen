using System.Text;

static class Helpers
{
    public static string PrintArray<T>(T[] array)
    {
        var builder = new StringBuilder();

        for (var column = 0; column < array.Length; column++)
        {
            builder.Append(array[column].ToString());
            builder.Append(" ");
        }

        return builder.ToString();
    }

    // row-major order
    public static string Print2DArray<T>(T[,] array, int n)
    {
        var builder = new StringBuilder();

        for (var row = 0; row < n; row++)
        {
            for (var column = 0; column < n; column++)
            {
                builder.Append(array[row, column].ToString());
                builder.Append(" ");
            }
            builder.AppendLine();
        }

        return builder.ToString();
    }
}
