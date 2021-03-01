using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Sprite)), RequireComponent(typeof(Collider2D))]
public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        var plane = new Plane(Vector3.forward, transform.position);
        var ray = eventData.pressEventCamera.ScreenPointToRay(eventData.position);
        
        if (plane.Raycast(ray, out var distance))
        {
            transform.position = ray.origin + ray.direction * distance;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }
}
