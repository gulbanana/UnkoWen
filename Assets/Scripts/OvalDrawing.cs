using System.Linq;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[ExecuteAlways]
public class OvalDrawing : MonoBehaviour
{
    public int samplesPerArc = 10;
    public float width = 2f;
    public float height = 2f;

    private void OnValidate()
    {
        var geometry = DrawOval();

        var renderer = GetComponent<LineRenderer>();
        renderer.positionCount = geometry.Length;
        for (var i = 0; i < geometry.Length; i++)
        {
            renderer.SetPosition(i, geometry[i]);
        }
    }
    
    private Vector2[] DrawOval()
    {
        // (4/3)*tan(pi/(2n)), where n = the number of control points in an arc - currently 4
        var magicNumber = 0.552284749831f;
        var rx = width / 2;
        var cx = rx * magicNumber;
        var ry = height / 2;
        var cy = ry * magicNumber;

        var curveSW = Bezier(samplesPerArc,
            new Vector2(-rx, 0),
            new Vector2(-rx, -cy),
            new Vector2(-cx, -ry),
            new Vector2(0, -ry));

        var curveSE = Bezier(samplesPerArc,
            new Vector2(0, -ry),
            new Vector2(cx, -ry),
            new Vector2(rx, -cy),
            new Vector2(rx, 0));

        var curveNE = Bezier(samplesPerArc,
            new Vector2(rx, 0),
            new Vector2(rx, cy),
            new Vector2(cx, ry),
            new Vector2(0, ry));

        var curveNW = Bezier(samplesPerArc,
            new Vector2(0, ry),
            new Vector2(-cx, ry),
            new Vector2(-rx, cy),
            new Vector2(-rx, 0));

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
