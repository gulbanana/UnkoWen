using UnityEngine;
using UnityEngine.UI;

public class AutoScroll : MonoBehaviour
{
    public float scrollSpeed = 1000f; // pixels per second
    private ScrollRect scrollRect;
    private float contentHeight;
    private bool beginScrolling;
    private bool scrolling;

    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    void Update()
    {
        var lastContentHeight = contentHeight;
        contentHeight = scrollRect.content.rect.height;
        if (contentHeight > lastContentHeight)
        {
            beginScrolling = true;
        }

        if (beginScrolling)
        {
            if (scrollRect.verticalScrollbar.value > 0)
            {
                beginScrolling = false;
                scrolling = true;
            }
        }

        if (scrolling)
        {
            if (scrollRect.verticalScrollbar.value <= 0)
            {
                scrolling = false;
            }
            else
            {
                var scrollableHeight = contentHeight - scrollRect.viewport.rect.height;
                var delta = Time.deltaTime * scrollSpeed / scrollableHeight;
                scrollRect.verticalScrollbar.value -= delta;
            }
        }
    }
}
