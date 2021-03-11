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

                foreach (var tag in story.currentTags)
                {
                    ProcessTag(tag);
                }

                foreach (var warning in story.currentWarnings ?? Enumerable.Empty<string>())
                {
                    Debug.LogWarning(warning);
                }

                foreach (var error in story.currentErrors ?? Enumerable.Empty<string>())
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

    void ProcessTag(string tag)
    {
        switch (tag.Split(' '))
        {
            case ("disable", var name):
                GameObject.Find(name)?.SetActive(false);
                break;

            case ("enable", var name):
                Resources.FindObjectsOfTypeAll<GameObject>().SingleOrDefault(go => go.name == name)?.SetActive(true);
                break;
        };        
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
