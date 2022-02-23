//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.2.0
//     from Assets/Prefabs/Player/PlayerInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""XRRightController"",
            ""id"": ""890493ec-57aa-44be-821a-748591a4eb12"",
            ""actions"": [
                {
                    ""name"": ""position"",
                    ""type"": ""Value"",
                    ""id"": ""aae2267e-c743-46f1-921d-61468737d2ff"",
                    ""expectedControlType"": ""Vector3"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Rotation"",
                    ""type"": ""Value"",
                    ""id"": ""65ec354a-53be-4ae3-8bfa-d4d21b67996b"",
                    ""expectedControlType"": ""Quaternion"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""TriggerAxis"",
                    ""type"": ""Value"",
                    ""id"": ""22a211f0-52f5-4789-a609-c13b4370fbec"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""GripAxis"",
                    ""type"": ""Value"",
                    ""id"": ""b1d8e1dc-ed4e-45af-b1d0-65c046ad68da"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""TriggerPress"",
                    ""type"": ""Button"",
                    ""id"": ""825d7b2b-0f97-457b-90ce-7bbd8f3116b8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e0411468-4497-43ce-b6cd-ba460fdd7377"",
                    ""path"": ""<XRController>{RightHand}/pointerPosition"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""position"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a94feda1-dbfc-430d-b33c-61f3a8532e28"",
                    ""path"": ""<XRController>{RightHand}/pointerRotation"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b02aff16-551f-4524-a272-1157f7979500"",
                    ""path"": ""<XRController>{RightHand}/trigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TriggerAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""11e75099-28b9-4319-8db4-cfcf13502c5c"",
                    ""path"": ""<XRController>{RightHand}/grip"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GripAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e356f31e-1bd7-4f84-862c-d1b31f533162"",
                    ""path"": ""<XRController>{RightHand}/triggerPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TriggerPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // XRRightController
        m_XRRightController = asset.FindActionMap("XRRightController", throwIfNotFound: true);
        m_XRRightController_position = m_XRRightController.FindAction("position", throwIfNotFound: true);
        m_XRRightController_Rotation = m_XRRightController.FindAction("Rotation", throwIfNotFound: true);
        m_XRRightController_TriggerAxis = m_XRRightController.FindAction("TriggerAxis", throwIfNotFound: true);
        m_XRRightController_GripAxis = m_XRRightController.FindAction("GripAxis", throwIfNotFound: true);
        m_XRRightController_TriggerPress = m_XRRightController.FindAction("TriggerPress", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // XRRightController
    private readonly InputActionMap m_XRRightController;
    private IXRRightControllerActions m_XRRightControllerActionsCallbackInterface;
    private readonly InputAction m_XRRightController_position;
    private readonly InputAction m_XRRightController_Rotation;
    private readonly InputAction m_XRRightController_TriggerAxis;
    private readonly InputAction m_XRRightController_GripAxis;
    private readonly InputAction m_XRRightController_TriggerPress;
    public struct XRRightControllerActions
    {
        private @PlayerInput m_Wrapper;
        public XRRightControllerActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @position => m_Wrapper.m_XRRightController_position;
        public InputAction @Rotation => m_Wrapper.m_XRRightController_Rotation;
        public InputAction @TriggerAxis => m_Wrapper.m_XRRightController_TriggerAxis;
        public InputAction @GripAxis => m_Wrapper.m_XRRightController_GripAxis;
        public InputAction @TriggerPress => m_Wrapper.m_XRRightController_TriggerPress;
        public InputActionMap Get() { return m_Wrapper.m_XRRightController; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(XRRightControllerActions set) { return set.Get(); }
        public void SetCallbacks(IXRRightControllerActions instance)
        {
            if (m_Wrapper.m_XRRightControllerActionsCallbackInterface != null)
            {
                @position.started -= m_Wrapper.m_XRRightControllerActionsCallbackInterface.OnPosition;
                @position.performed -= m_Wrapper.m_XRRightControllerActionsCallbackInterface.OnPosition;
                @position.canceled -= m_Wrapper.m_XRRightControllerActionsCallbackInterface.OnPosition;
                @Rotation.started -= m_Wrapper.m_XRRightControllerActionsCallbackInterface.OnRotation;
                @Rotation.performed -= m_Wrapper.m_XRRightControllerActionsCallbackInterface.OnRotation;
                @Rotation.canceled -= m_Wrapper.m_XRRightControllerActionsCallbackInterface.OnRotation;
                @TriggerAxis.started -= m_Wrapper.m_XRRightControllerActionsCallbackInterface.OnTriggerAxis;
                @TriggerAxis.performed -= m_Wrapper.m_XRRightControllerActionsCallbackInterface.OnTriggerAxis;
                @TriggerAxis.canceled -= m_Wrapper.m_XRRightControllerActionsCallbackInterface.OnTriggerAxis;
                @GripAxis.started -= m_Wrapper.m_XRRightControllerActionsCallbackInterface.OnGripAxis;
                @GripAxis.performed -= m_Wrapper.m_XRRightControllerActionsCallbackInterface.OnGripAxis;
                @GripAxis.canceled -= m_Wrapper.m_XRRightControllerActionsCallbackInterface.OnGripAxis;
                @TriggerPress.started -= m_Wrapper.m_XRRightControllerActionsCallbackInterface.OnTriggerPress;
                @TriggerPress.performed -= m_Wrapper.m_XRRightControllerActionsCallbackInterface.OnTriggerPress;
                @TriggerPress.canceled -= m_Wrapper.m_XRRightControllerActionsCallbackInterface.OnTriggerPress;
            }
            m_Wrapper.m_XRRightControllerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @position.started += instance.OnPosition;
                @position.performed += instance.OnPosition;
                @position.canceled += instance.OnPosition;
                @Rotation.started += instance.OnRotation;
                @Rotation.performed += instance.OnRotation;
                @Rotation.canceled += instance.OnRotation;
                @TriggerAxis.started += instance.OnTriggerAxis;
                @TriggerAxis.performed += instance.OnTriggerAxis;
                @TriggerAxis.canceled += instance.OnTriggerAxis;
                @GripAxis.started += instance.OnGripAxis;
                @GripAxis.performed += instance.OnGripAxis;
                @GripAxis.canceled += instance.OnGripAxis;
                @TriggerPress.started += instance.OnTriggerPress;
                @TriggerPress.performed += instance.OnTriggerPress;
                @TriggerPress.canceled += instance.OnTriggerPress;
            }
        }
    }
    public XRRightControllerActions @XRRightController => new XRRightControllerActions(this);
    public interface IXRRightControllerActions
    {
        void OnPosition(InputAction.CallbackContext context);
        void OnRotation(InputAction.CallbackContext context);
        void OnTriggerAxis(InputAction.CallbackContext context);
        void OnGripAxis(InputAction.CallbackContext context);
        void OnTriggerPress(InputAction.CallbackContext context);
    }
}
