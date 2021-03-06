using UnityEngine;
using UnityEngine.EventSystems;

public class ExclusiveControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CameraPanner panner;

    public void OnPointerEnter(PointerEventData eventData)
    {
        panner.enabled = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        panner.enabled = true;
    }
}
