using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExclusiveControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CameraPanner disabledPanner;
    public Image hiddenScrollbar;

    public void Start()
    {
        hiddenScrollbar.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        disabledPanner.enabled = false;
        hiddenScrollbar.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        disabledPanner.enabled = true;
        hiddenScrollbar.enabled = false;
    }
}
