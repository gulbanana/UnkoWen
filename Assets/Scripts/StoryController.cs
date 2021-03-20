using Ink.Runtime;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class StoryController : MonoBehaviour, IPointerClickHandler
{
    public EntityController entities;
    public TextAsset json;
    public TextMeshProUGUI mainText;
    public TextMeshProUGUI choiceText;
    private Story story;
    private bool hideNextChoices;

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
                Debug.Log($"paragraph: {paragraph}");
                Debug.Log($"tags: {story.currentTags.Count}");
                foreach (var tag in story.currentTags)
                {
                    ProcessTag(tag);
                }
                entities.DisableInteraction();

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
                if (hideNextChoices)
                {
                    entities.EnableInteraction();
                    hideNextChoices = false;
                }
                else
                {
                    choiceText.text = string.Join(string.Empty, story.currentChoices.Select((c, idx) => $"<link=\"{idx}\">{c.text}</link>\n"));
                    choiceText.enabled = true;
                }
            }
        }
    }

    void ProcessTag(string tag)
    {
        var result = tag.Split(' ') switch
        {
            ("disable", var name) => entities.Deactivate(name),
            ("enable", var name) => entities.Activate(name),
            ("hide-choices", _) => hideNextChoices = true,
            _ => false
        };

        if (!result)
        {
            Debug.LogError($"Failed to process tag {tag}");
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

    public void Choose(string text)
    {
        var c = story.currentChoices.SingleOrDefault(c => c.text == text);
        if (c == null)
        {
            Debug.LogError($"failed to find choice {text}");
        }
        else
        {
            story.ChooseChoiceIndex(c.index);
        }
    }
}
