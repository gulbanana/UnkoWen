using Ink.Runtime;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class StoryController : MonoBehaviour, IPointerClickHandler
{
    public TextAsset json;
    public TextMeshProUGUI mainText;
    public TextMeshProUGUI choiceText;
    private Story story;

    void Start()
    {
        mainText.text = "";
        story = new Story(json.text);
    }

    void Update()
    {
        if (story.canContinue)
        {
            while (story.canContinue)
            {
                var paragraph = story.Continue();
                mainText.text += $"{paragraph}\n";

                foreach (var warning in story.currentWarnings)
                {
                    Debug.LogWarning(warning);
                }

                foreach (var error in story.currentErrors)
                {
                    Debug.LogError(error);
                }
            }
            
            if (story.currentChoices.Any())
            {
                choiceText.text = string.Join(string.Empty, story.currentChoices.Select((c, idx) => $"<link=\"{idx}\">{c.text}</link>\n"));
                choiceText.enabled = true;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var linkIndex = TMP_TextUtilities.FindIntersectingLink(choiceText, eventData.position, eventData.pressEventCamera);
        if (linkIndex != -1)
        {
            var link = choiceText.textInfo.linkInfo[linkIndex];
            if (int.TryParse(link.GetLinkID(), out var linkID))
            {
                story.ChooseChoiceIndex(linkID);
                choiceText.enabled = false;
            }
            else
            {
                Debug.LogError($"unknown link id {link.GetLinkID()}");
            }
        }
    }
}
