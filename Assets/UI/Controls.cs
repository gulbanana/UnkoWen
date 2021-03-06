// GENERATED AUTOMATICALLY FROM 'Assets/UI/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Interface"",
            ""id"": ""dc251e8d-509d-4982-bd53-2fb0e2250542"",
            ""actions"": [
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Value"",
                    ""id"": ""69c4f813-5704-416e-9916-daeb022cff03"",
                    ""expectedControlType"": ""Analog"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pan"",
                    ""type"": ""Value"",
                    ""id"": ""7f0dc556-28a5-4bb8-b326-1c5ede8d47e7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Trigger Pan"",
                    ""type"": ""Button"",
                    ""id"": ""ddab4864-0607-472b-b105-6c7acc470f9c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""288e592c-9394-43cf-9245-35eee5d051f6"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""39ac0e42-ba17-409d-a142-ee9c9831bc12"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pan"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c80ddf78-c3b3-419c-aed2-5566cfee1e2d"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Trigger Pan"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Interface
        m_Interface = asset.FindActionMap("Interface", throwIfNotFound: true);
        m_Interface_Zoom = m_Interface.FindAction("Zoom", throwIfNotFound: true);
        m_Interface_Pan = m_Interface.FindAction("Pan", throwIfNotFound: true);
        m_Interface_TriggerPan = m_Interface.FindAction("Trigger Pan", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Interface
    private readonly InputActionMap m_Interface;
    private IInterfaceActions m_InterfaceActionsCallbackInterface;
    private readonly InputAction m_Interface_Zoom;
    private readonly InputAction m_Interface_Pan;
    private readonly InputAction m_Interface_TriggerPan;
    public struct InterfaceActions
    {
        private @Controls m_Wrapper;
        public InterfaceActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Zoom => m_Wrapper.m_Interface_Zoom;
        public InputAction @Pan => m_Wrapper.m_Interface_Pan;
        public InputAction @TriggerPan => m_Wrapper.m_Interface_TriggerPan;
        public InputActionMap Get() { return m_Wrapper.m_Interface; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InterfaceActions set) { return set.Get(); }
        public void SetCallbacks(IInterfaceActions instance)
        {
            if (m_Wrapper.m_InterfaceActionsCallbackInterface != null)
            {
                @Zoom.started -= m_Wrapper.m_InterfaceActionsCallbackInterface.OnZoom;
                @Zoom.performed -= m_Wrapper.m_InterfaceActionsCallbackInterface.OnZoom;
                @Zoom.canceled -= m_Wrapper.m_InterfaceActionsCallbackInterface.OnZoom;
                @Pan.started -= m_Wrapper.m_InterfaceActionsCallbackInterface.OnPan;
                @Pan.performed -= m_Wrapper.m_InterfaceActionsCallbackInterface.OnPan;
                @Pan.canceled -= m_Wrapper.m_InterfaceActionsCallbackInterface.OnPan;
                @TriggerPan.started -= m_Wrapper.m_InterfaceActionsCallbackInterface.OnTriggerPan;
                @TriggerPan.performed -= m_Wrapper.m_InterfaceActionsCallbackInterface.OnTriggerPan;
                @TriggerPan.canceled -= m_Wrapper.m_InterfaceActionsCallbackInterface.OnTriggerPan;
            }
            m_Wrapper.m_InterfaceActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Zoom.started += instance.OnZoom;
                @Zoom.performed += instance.OnZoom;
                @Zoom.canceled += instance.OnZoom;
                @Pan.started += instance.OnPan;
                @Pan.performed += instance.OnPan;
                @Pan.canceled += instance.OnPan;
                @TriggerPan.started += instance.OnTriggerPan;
                @TriggerPan.performed += instance.OnTriggerPan;
                @TriggerPan.canceled += instance.OnTriggerPan;
            }
        }
    }
    public InterfaceActions @Interface => new InterfaceActions(this);
    public interface IInterfaceActions
    {
        void OnZoom(InputAction.CallbackContext context);
        void OnPan(InputAction.CallbackContext context);
        void OnTriggerPan(InputAction.CallbackContext context);
    }
}
