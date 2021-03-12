using UnityEngine;

struct LineSegment
{
    public Vector2 start { get; set; }
    public Vector2 end { get; set; }

    public LineSegment(Vector2 start, Vector2 end) => (this.start, this.end) = (start, end);

    public static Vector2 operator*(LineSegment line, float x)
    {
        var diff = line.end - line.start;
        return line.start + diff * x;
    }
}
