using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ChoiceLinks : MonoBehaviour, IPointerClickHandler
{    
    public TextMeshProUGUI mainText;
    public TextMeshProUGUI choiceText;
    public Camera hoverCam;
    public Color32 baseColor;
    public Color32 hoverColor;
    private int hoverLink = -1;

    private readonly string[] texts =
    {
        "\"Click here,\" I said, willing the player's cursor to my location.",
        "\"...or here. That would also be an OK place to click.\" I knew the player wouldn't perceive my contempt."
    };

    public void OnPointerClick(PointerEventData eventData)
    {
        var linkIndex = TMP_TextUtilities.FindIntersectingLink(choiceText, eventData.position, eventData.pressEventCamera);
        if (linkIndex != -1)
        {
            //var link = choiceText.textInfo.linkInfo[linkIndex];
            mainText.text += $"{texts[linkIndex]}\n\n";
        }
    }

    public void LateUpdate()
    {
        var position = Mouse.current.position.ReadValue();
        var hovering = TMP_TextUtilities.IsIntersectingRectTransform(choiceText.rectTransform, position, hoverCam);

        if (hovering)
        {
            var linkIndex = TMP_TextUtilities.FindIntersectingLink(choiceText, position, hoverCam);
            if (linkIndex != -1)
            {
                if (hoverLink != -1)
                {
                    HighlightLink(hoverLink, baseColor);
                }

                hoverLink = linkIndex;
                HighlightLink(hoverLink, hoverColor);
            }
        }
        else if (hoverLink != -1)
        {
            HighlightLink(hoverLink, baseColor);
            hoverLink = -1;
        }
    }

    private void HighlightLink(int linkIndex, Color32 color)
    {
        var linkInfo = choiceText.textInfo.linkInfo[linkIndex];

        for (var i = 0; i < linkInfo.linkTextLength; i++)
        {
            var charIndex = linkInfo.linkTextfirstCharacterIndex + i;
            var charInfo = choiceText.textInfo.characterInfo[charIndex];
            var meshIndex = charInfo.materialReferenceIndex;
            var vertexIndex = charInfo.vertexIndex;
            var vertexColors = choiceText.textInfo.meshInfo[meshIndex].colors32;

            vertexColors[vertexIndex + 0] = color;
            vertexColors[vertexIndex + 1] = color;
            vertexColors[vertexIndex + 2] = color;
            vertexColors[vertexIndex + 3] = color;
        }

        choiceText.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
    }
}
