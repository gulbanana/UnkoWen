using Ink.Runtime;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class StoryController : MonoBehaviour, IPointerClickHandler
{
    public PlotController entities;
    public TextAsset json;
    public TMP_Text mainText;
    public TMP_Text choiceText;
    public TMP_FontAsset mainFont;
    public TMP_FontAsset interviewFont;
    public float titleSize;
    private Story story;
    private bool hideNextChoices;
    private bool inTitle;
    private bool inInterview;
    private bool endingInterview;

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
                entities.DisableInteraction();

                var paragraph = story.Continue();                
                
                foreach (var tag in story.currentTags)
                {
                    ProcessTag(tag);
                }

                mainText.text += Format(paragraph);

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
            ("disable", string name) => entities.Deactivate(name),
            ("enable", string name) => entities.Activate(name),
            ("label", string[](string name, string[] label)) => entities.SetName(name, string.Join(" ", label)),
            ("art", string[](string name, string asset)) => entities.SetImage(name, asset),
            ("plot-choices", _) => hideNextChoices = true,
            ("format", "title") => inTitle = true,
            ("format", "begin-interview") => BeginInterview(),
            ("format", "end-interview") => EndInterview(),
            _ => false
        };

        if (!result)
        {
            Debug.LogError($"Failed to process tag {tag}");
        }
    }

    bool BeginInterview()
    {
        inInterview = true;
        choiceText.font = interviewFont;
        return true;
    }

    bool EndInterview()
    {
        inInterview = false;
        endingInterview = true;
        choiceText.font = mainFont;
        return true;
    }

    string Format(string paragraph)
    {
        var builder = new StringBuilder();

        if (endingInterview)
        {
            builder.AppendLine();
            endingInterview = false;
        }

        if (inInterview)
        {
            builder.Append($"<font={interviewFont.name}>");
        }

        if (inTitle)
        {
            builder.Append("<align=center>");
            if (!inInterview)
            {
                builder.Append($"<size={titleSize}>");
            }
        }

        builder.Append(paragraph);
        
        if (inTitle)
        {
            if (!inInterview)
            {
                builder.Append("</size>");
            }
            builder.Append("</align>");
            inTitle = false;
        }

        if (inInterview)
        {
            builder.Append("</font>");
        }
        else
        {
            builder.AppendLine();
        }

        return builder.ToString();
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
