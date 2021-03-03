using UnityEngine;
using UnityEngine.InputSystem;

// born 1995-2009
public class Zoomer : MonoBehaviour
{
    public float zoomSpeed = 10f;

    private new Camera camera;
    private Controls controls;
    bool panning;
    Vector2 lastScreenPosition;

    public void OnEnable()
    {
        if (camera == null)
        {
            camera = GetComponent<Camera>();
        }

        if (controls == null)
        {
            controls = new Controls();
            controls.UI.Zoom.performed += OnZoom;
            controls.UI.Pan.performed += OnPan;
            controls.UI.TriggerPan.started += OnBeginPan;
            controls.UI.TriggerPan.canceled += OnEndPan;
        }

        controls.UI.Enable();
    }

    public void OnDisable()
    {
        controls.UI.Disable();
    }

    private void OnZoom(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<Vector2>();
        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize - value.y * zoomSpeed, 2f, 6f);
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
            var worldPosition = camera.ScreenToWorldPoint(screenPosition);
            var lastWorldPosition = camera.ScreenToWorldPoint(lastScreenPosition);

            var worldDelta = lastWorldPosition - worldPosition;

            camera.transform.position = camera.transform.position + worldDelta;
        }

        lastScreenPosition = screenPosition;
    }
}
