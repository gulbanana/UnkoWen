using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChoiceLinks : MonoBehaviour, IPointerClickHandler
{    
    public TextMeshProUGUI mainText;
    public TextMeshProUGUI choiceText;

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
            var link = choiceText.textInfo.linkInfo[linkIndex];
            mainText.text += $"{texts[linkIndex]}\n\n";
        }
    }
}
