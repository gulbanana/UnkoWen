using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public float zoomSpeed = .002f;
    public float minZoom = 2f;
    public float maxZoom = 6f;
    private Rect maxPan;

    public Camera controlledCamera;
    private Controls controls;
    internal bool panning;
    Vector2 lastScreenPosition;

    public void OnEnable()
    {
        if (controls == null)
        {
            controls = new Controls();
            controls.Interface.Zoom.performed += OnZoom;
            controls.Interface.Pan.performed += OnPan;
            controls.Interface.TriggerPan.started += OnBeginPan;
            controls.Interface.TriggerPan.canceled += OnEndPan;
        }

        controls.Enable();
    }

    public void OnDisable()
    {
        controls.Disable();
    }

    public void Start()
    {
        controlledCamera.orthographicSize = maxZoom;
        var origin = controlledCamera.ViewportToWorldPoint(Vector2.zero);
        var extent = controlledCamera.ViewportToWorldPoint(Vector2.one);
        maxPan = new Rect(origin, extent);
    }

    private void OnZoom(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<Vector2>();
        controlledCamera.orthographicSize = Mathf.Clamp(controlledCamera.orthographicSize - value.y * zoomSpeed, minZoom, maxZoom);

        if (value.magnitude > 0)
        {
            ClampCamera(controlledCamera.transform.position);
        }
    }

    private void OnBeginPan(InputAction.CallbackContext context)
    {
        panning = true;
    }

    private void OnEndPan(InputAction.CallbackContext context)
    {
        panning = false;
    }

    private void OnPan(InputAction.CallbackContext context)
    {
        var screenPosition = context.ReadValue<Vector2>();
        
        if (panning)
        {
            var worldPosition = controlledCamera.ScreenToWorldPoint(screenPosition);
            var lastWorldPosition = controlledCamera.ScreenToWorldPoint(lastScreenPosition);

            var worldDelta = lastWorldPosition - worldPosition;

            var newPosition = controlledCamera.transform.position + worldDelta;

            ClampCamera(newPosition);
        }

        lastScreenPosition = screenPosition;
    }

    private void ClampCamera(Vector3 desiredPosition)
    {
        var zoomFactor = maxZoom / controlledCamera.orthographicSize;
        var halfWidth = (maxPan.width - (maxPan.width / zoomFactor));
        var halfHeight = (maxPan.height - (maxPan.height / zoomFactor));

        controlledCamera.transform.position = new Vector3(Mathf.Clamp(desiredPosition.x, -halfWidth, halfWidth), Mathf.Clamp(desiredPosition.y, -halfHeight, halfHeight), controlledCamera.transform.position.z);
    }
}
