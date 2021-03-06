using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExclusiveControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CameraPanner disabledPanner;
    public Image hiddenScrollbar;
    private bool disablePanningLater;

    public void Start()
    {
        hiddenScrollbar.enabled = false;
    }

    public void Update()
    {
        if (disablePanningLater && !disabledPanner.panning)
        {
            disablePanningLater = false;
            disabledPanner.enabled = false;
            hiddenScrollbar.enabled = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (disabledPanner.panning)
        {
            disablePanningLater = true;
        }
        else
        {
            disabledPanner.enabled = false;
            hiddenScrollbar.enabled = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        disablePanningLater = false;
        disabledPanner.enabled = true;
        hiddenScrollbar.enabled = false;
    }
}
