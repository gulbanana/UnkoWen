using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChoiceLinks : MonoBehaviour, IPointerClickHandler
{    
    public TextMeshProUGUI mainText;
    public TextMeshProUGUI choiceText;
    public ScrollRect scrollView;
    public float scrollSpeed = 1000f;
    private float totalHeight;

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
            totalHeight = mainText.GetComponentInParent<RectTransform>().rect.height;

            SmoothScroll().Forget();
        }
    }

    private async UniTask SmoothScroll()
    {
        scrollView.verticalNormalizedPosition = Mathf.Clamp(scrollView.verticalNormalizedPosition, 0.0001f, 1);
        while (scrollView.verticalNormalizedPosition > 0)
        {
            scrollView.verticalNormalizedPosition -= Time.deltaTime * scrollSpeed / totalHeight;
            await UniTask.NextFrame();
        }
    }
}
