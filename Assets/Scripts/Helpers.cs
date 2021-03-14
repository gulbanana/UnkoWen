using System.Text;
using UnityEngine;
using UnityEngine.U2D;

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

    public static string ExtractSplineDefinition(Spline spline)
    {
        var builder = new StringBuilder();

        builder.AppendLine($"new Vector3[{spline.GetPointCount()}, 3]");
        builder.AppendLine("{");
        for (var i = 0; i < spline.GetPointCount(); i++)
        {
            builder.AppendLine($"{{ new Vector3({spline.GetPosition(i).x}f, {spline.GetPosition(i).y}f), new Vector3({spline.GetLeftTangent(i).x}f, {spline.GetLeftTangent(i).y}f), new Vector3({spline.GetRightTangent(i).x}f, {spline.GetRightTangent(i).y}f) }},");
        }
        builder.AppendLine("}");

        return builder.ToString();        
    }
}
