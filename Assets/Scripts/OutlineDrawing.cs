using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;

public class OutlineDrawing : MonoBehaviour
{
    public TextMeshPro textMesh;
    public SpriteShapeController spriteShape;
    [TextArea(3, 10)] public string text;

    private void OnValidate()
    {
        ChangeText(text).Forget();
    }

    public async UniTask ChangeText(string text)
    {
        await UniTask.Yield();

        if (textMesh)
        {
            textMesh.text = text;
            textMesh.rectTransform.sizeDelta = new Vector2(textMesh.preferredWidth, textMesh.preferredHeight);

            if (spriteShape && spriteShape.spline.GetPointCount() == 8)
            {
                BuildExemplar(textMesh.rectTransform.sizeDelta, spriteShape.spline);
            }
        }
    }

    private static void BuildExemplar(Vector2 size, Spline spline)
    {
        for (var i = 0; i < exemplar3.Length/3; i++)
        {
            spline.SetPosition(i, exemplar3[i, 0] * size);
            spline.SetLeftTangent(i, exemplar3[i, 1] * size);
            spline.SetRightTangent(i, exemplar3[i, 2] * size);

            if (i == 0 || i == exemplar3.Length / 3 - 1)
            {
                spline.SetHeight(i, .01f);
            }
            else
            {
                spline.SetHeight(i, .05f);
            }
        }
    }

    // w = 1 h = 1
    private static readonly Vector3[,] exemplar3 = new Vector3[8, 3]
    {
        { new Vector3(-0.6093444f, 0.239446f), new Vector3(0f, 0f), new Vector3(-0.1212946f, -0.2108566f) },
        { new Vector3(-0.5797728f, -0.5066376f), new Vector3(-0.1660769f, 0.2132384f), new Vector3(0.1535294f, -0.197127f) },
        { new Vector3(0.0458793f, -0.7446248f), new Vector3(-0.1283025f, 0.006789542f), new Vector3(0.1034789f, -0.005475712f) },
        { new Vector3(0.5298351f, -0.409226f), new Vector3(-0.1027065f, -0.268957f), new Vector3(0.0755535f, 0.1978514f) },
        { new Vector3(0.5386792f, 0.3898384f), new Vector3(0.1197482f, -0.3131032f), new Vector3(-0.08645081f, 0.2260412f) },
        { new Vector3(0.003101764f, 0.6794524f), new Vector3(0.133619f, 0.008687114f), new Vector3(-0.1591447f, -0.01034727f) },
        { new Vector3(-0.3808933f, 0.5537258f), new Vector3(0.2046482f, 0.1060986f), new Vector3(-0.2224941f, -0.1153505f) },
        { new Vector3(-0.6275525f, 0.0348959f), new Vector3(0.03817097f, -0.1056986f), new Vector3(-0.07015402f, 0.1942639f) },
    };
}
