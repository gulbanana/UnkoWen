using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExclusiveControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CameraPanner disabledPanner;
    public Image hiddenScrollbar;
    private bool pointerWithin;

    public void Start()
    {
        hiddenScrollbar.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        pointerWithin = true;
        if (disabledPanner.panning)
        {
            StartCoroutine(DisablePanningLater());
        }
        else
        {
            disabledPanner.enabled = false;
            hiddenScrollbar.enabled = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pointerWithin = false;
        disabledPanner.enabled = true;
        hiddenScrollbar.enabled = false;
    }

    private IEnumerator DisablePanningLater()
    {
        while (disabledPanner.panning)
        {
            yield return null;
        }

        if (pointerWithin)
        {
            disabledPanner.enabled = false;
            hiddenScrollbar.enabled = true;
        }
    }
}
