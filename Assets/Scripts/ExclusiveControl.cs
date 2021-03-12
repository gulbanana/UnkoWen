using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExclusiveControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CameraController disabledController;
    public Image hiddenScrollbar;
    private bool disablePanningLater;

    public void Start()
    {
        hiddenScrollbar.enabled = false;
    }

    public void Update()
    {
        if (disablePanningLater && !disabledController.panning)
        {
            disablePanningLater = false;
            disabledController.enabled = false;
            hiddenScrollbar.enabled = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (disabledController.panning)
        {
            disablePanningLater = true;
        }
        else
        {
            disabledController.enabled = false;
            hiddenScrollbar.enabled = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        disablePanningLater = false;
        disabledController.enabled = true;
        hiddenScrollbar.enabled = false;
    }
}
