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
        playerInput.XRRightController.position.performed += ctx => rightHand.UpdateLocalPosition(ctx.ReadValue<Vector3>());
        playerInput.XRRightController.Rotation.performed += ctx => rightHand.UpdateLocalRotation(ctx.ReadValue<Quaternion>());
        playerInput.XRRightController.GripAxis.performed += ctx => rightHand.UpdateGripAnimation(ctx.ReadValue<float>());
        playerInput.XRRightController.TriggerAxis.performed += ctx => rightHand.UpdateTriggerAnimation(ctx.ReadValue<float>());
        playerInput.XRRightController.TriggerPress.performed += ctx => rightHand.TriggerPressed();
        playerInput.XRRightController.TriggerPress.canceled += ctx => rightHand.TriggerReleased();
    }
}
