using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Sprite)), RequireComponent(typeof(Collider2D))]
public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    bool draggingByEvent;
    Camera draggingCamera;
    Vector2 dragOffset;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!draggingByEvent)
        {
            if (draggingCamera == null)
            {
                GrabPointer(eventData.pressEventCamera, eventData.position);
            }
            else
            {
                ReleasePointer();
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GrabPointer(eventData.pressEventCamera, eventData.position);
        draggingCamera = null;
        draggingByEvent = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        FollowPointer(eventData.pressEventCamera, eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        ReleasePointer();
    }

    public void Update()
    {
        if (draggingCamera != null)
        {
            FollowPointer(draggingCamera, Pointer.current.position.ReadValue());
        }
    }

    private void GrabPointer(Camera camera, Vector3 position)
    {
        var worldPosition = camera.ScreenToWorldPoint(position);
        draggingCamera = camera;
        dragOffset = transform.position - worldPosition;
    }

    private void FollowPointer(Camera camera, Vector3 position)
    {
        var plane = new Plane(Vector3.forward, transform.position);
        var ray = camera.ScreenPointToRay(position + (Vector3)dragOffset);

        if (plane.Raycast(ray, out var distance))
        {
            transform.position = ray.origin + ray.direction * distance + (Vector3)dragOffset;
        }
    }

    private void ReleasePointer()
    {
        draggingCamera = null;
        draggingByEvent = false;
    }
}
