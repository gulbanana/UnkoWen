using System.Linq;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[ExecuteAlways]
public class CircleDrawing : MonoBehaviour
{
    public int resolution = 10;
    public float radius = 2f;

    private void OnValidate()
    {
        var geometry = DrawCircle();

        var renderer = GetComponent<LineRenderer>();
        renderer.positionCount = geometry.Length;
        for (var i = 0; i < geometry.Length; i++)
        {
            renderer.SetPosition(i, geometry[i]);
        }
    }
    
    private Vector2[] DrawCircle()
    {
        // (4/3)*tan(pi/(2n)), where n = the number of control points in an arc - currently 4
        var magicNumber = radius * 0.552284749831f;

        var curveSW = Bezier(resolution,
            new Vector2(-radius, 0),
            new Vector2(-radius, -magicNumber),
            new Vector2(-magicNumber, -radius),
            new Vector2(0, -radius));

        var curveSE = Bezier(resolution,
            new Vector2(0, -radius),
            new Vector2(magicNumber, -radius),
            new Vector2(radius, -magicNumber),
            new Vector2(radius, 0));

        var curveNE = Bezier(resolution,
            new Vector2(radius, 0),
            new Vector2(radius, magicNumber),
            new Vector2(magicNumber, radius),
            new Vector2(0, radius));

        var curveNW = Bezier(resolution,
            new Vector2(0, radius),
            new Vector2(-magicNumber, radius),
            new Vector2(-radius, magicNumber),
            new Vector2(-radius, 0));

        return curveSW.Concat(curveSE).Concat(curveNE).Concat(curveNW).ToArray();
    }

    // de casteljau's algorithm
    private static Vector2[] Bezier(int steps, params Vector2[] points)
    {
        var segments = new LineSegment[points.Length-1];
        for (var i = 0; i < points.Length-1; i++)
        {
            segments[i] = new LineSegment(points[i], points[i + 1]);
        }

        // integration timestep
        var result = new Vector2[steps];
        for (var i = 0; i < steps; i++)
        {
            var t = (1f / steps) * i;

            // nth derivatives
            var lastSegments = segments;
            for (var j = 1; j < segments.Length; j++)
            {
                var nextSegments = new LineSegment[segments.Length - j];
                for (var k = 0; k < nextSegments.Length; k++)
                {
                    nextSegments[k] = new LineSegment(lastSegments[k] * t, lastSegments[k + 1] * t);
                }
                lastSegments = nextSegments;
            }

            result[i] = lastSegments[0] * t;
        }

        return result;
    }
}
