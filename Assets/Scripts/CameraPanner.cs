using UnityEngine;
using UnityEngine.InputSystem;

public class CameraPanner : MonoBehaviour
{
    public float zoomSpeed = .002f;
    public float maxPanX = 3f;
    public float maxPanY = 2f;

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

    private void OnZoom(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<Vector2>();
        controlledCamera.orthographicSize = Mathf.Clamp(controlledCamera.orthographicSize - value.y * zoomSpeed, 2f, 6f);
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
            
            controlledCamera.transform.position = new Vector3(Mathf.Clamp(newPosition.x, -maxPanX, maxPanX), Mathf.Clamp(newPosition.y, -maxPanY, maxPanY), controlledCamera.transform.position.z);
        }

        lastScreenPosition = screenPosition;
    }
}
