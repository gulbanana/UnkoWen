using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// born 1995-2009
public class Zoomer : MonoBehaviour
{
    public float speed = 500f;
    Camera camera;
    Controls controls;    

    public void Awake()
    {
        camera = GetComponent<Camera>();
        controls = new Controls();
        controls.UI.Zoom.performed += OnZoom;
    }

    public void OnEnable()
    {
        controls.UI.Enable();
    }

    public void OnDisable()
    {
        controls.UI.Disable();
    }

    private void OnZoom(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<Vector2>();
        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize - value.y / speed, 2f, 6f);
    }
}
