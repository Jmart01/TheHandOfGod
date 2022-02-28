using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputComp : MonoBehaviour
{
    PlayerInput playerInput;
    [SerializeField] XRHand rightHand;
    [SerializeField] XRHand leftHand;

    private void Awake()
    {
        if (playerInput == null)
        {
            playerInput = new PlayerInput();
        }
    }

    private void OnEnable()
    {
        playerInput?.Enable();
    }

    private void OnDisable()
    {
        playerInput?.Disable();
    }

    private void Start()
    {
        //RightHand Controls
        playerInput.XRRightController.position.performed += ctx => rightHand.UpdateLocalPosition(ctx.ReadValue<Vector3>());
        playerInput.XRRightController.Rotation.performed += ctx => rightHand.UpdateLocalRotation(ctx.ReadValue<Quaternion>());
        playerInput.XRRightController.GripAxis.performed += ctx => rightHand.UpdateGripAnimation(ctx.ReadValue<float>());
        playerInput.XRRightController.TriggerAxis.performed += ctx => rightHand.UpdateTriggerAnimation(ctx.ReadValue<float>());
        playerInput.XRRightController.TriggerPress.performed += ctx => rightHand.TriggerPressed();
        playerInput.XRRightController.TriggerPress.canceled += ctx => rightHand.TriggerReleased();
        //LeftHand controls
        playerInput.XRLeftController.GripAxis.performed += ctx => leftHand.UpdateGripAnimation(ctx.ReadValue<float>());
        playerInput.XRLeftController.Position.performed += ctx => leftHand.UpdateLocalPosition(ctx.ReadValue<Vector3>());
        playerInput.XRLeftController.Rotation.performed += ctx => leftHand.UpdateLocalRotation(ctx.ReadValue<Quaternion>());
        playerInput.XRLeftController.TriggerPressed.performed += ctx => leftHand.TriggerPressed();
        playerInput.XRLeftController.TriggerPressed.canceled += ctx => leftHand.TriggerReleased();
        playerInput.XRLeftController.TriggerAxis.performed += ctx => leftHand.UpdateTriggerAnimation(ctx.ReadValue<float>());

    }
}
