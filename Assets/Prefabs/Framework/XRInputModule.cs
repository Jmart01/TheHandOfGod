using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public interface IXRControllerInterface
{
    public Vector2 GetPointerScreenPosition();
}


public class XRInputModule : PointerInputModule
{
    PlayerInput playerInput;
    PointerEventData LeftControllerData;
    PointerEventData RightControllerData;

    [SerializeField] GameObject LeftController;
    [SerializeField] GameObject RightController;

    IXRControllerInterface LeftControllerInterface;
    IXRControllerInterface RightControllerInterface;

    public override void Process()
    {
        
    }

    protected override void Awake()
    {
        base.Awake();
        if(playerInput == null)
        {
            playerInput = new PlayerInput();
        }
    }

    protected override void Start()
    {
        base.Start();
        LeftControllerInterface = LeftController.GetComponent<IXRControllerInterface>();
        RightControllerInterface = RightController.GetComponent<IXRControllerInterface>();
        RightControllerData = new PointerEventData(eventSystem);
        LeftControllerData = new PointerEventData(eventSystem);
        playerInput.XRRightController.TriggerPress.performed += RightTriggerPressed;
        playerInput.XRLeftController.TriggerPressed.performed += LeftTriggerPressed;
        playerInput.XRRightController.TriggerPress.canceled += OnRightTriggerReleased;
        playerInput.XRLeftController.TriggerPressed.canceled += OnLeftTriggerReleased;

        playerInput.XRRightController.position.performed += OnRightTriggerMoved;
        playerInput.XRLeftController.Position.performed += OnLeftTriggerMoved;

    }

    private void OnLeftTriggerMoved(InputAction.CallbackContext obj)
    {
        OnTriggerMoved(LeftControllerInterface, LeftControllerData);
    }

    void OnTriggerMoved(IXRControllerInterface controller, PointerEventData eventData)
    {
        if(controller == null || eventData == null)
        {
            return;
        }
        eventData.position = controller.GetPointerScreenPosition();
        List<RaycastResult> raycastResult = new List<RaycastResult>();
        eventSystem.RaycastAll(eventData, raycastResult);
        eventData.pointerCurrentRaycast = FindFirstRaycast(raycastResult);
        ProcessMove(eventData);
        if (eventData.dragging)
        {
            ExecuteEvents.Execute(eventData.pointerDrag, eventData, ExecuteEvents.dragHandler);
        }
    }

    private void OnRightTriggerMoved(InputAction.CallbackContext obj)
    {
        OnTriggerMoved(RightControllerInterface, RightControllerData);
    }

    private void OnLeftTriggerReleased(InputAction.CallbackContext obj)
    {
        OnTriggerReleased(LeftControllerInterface, LeftControllerData);
    }

    private void OnRightTriggerReleased(InputAction.CallbackContext obj)
    {
        OnTriggerReleased(RightControllerInterface, RightControllerData);
    }

    private void LeftTriggerPressed(InputAction.CallbackContext obj)
    {
        OnTriggerPressed(LeftControllerInterface, LeftControllerData);
    }

    private void RightTriggerPressed(InputAction.CallbackContext obj)
    {
        OnTriggerPressed(RightControllerInterface, RightControllerData);
    }

    private void OnTriggerReleased(IXRControllerInterface controller, PointerEventData eventData)
    {
        if(controller == null || eventData == null)
        {
            return;
        }

        eventData.position = controller.GetPointerScreenPosition();
        List<RaycastResult> raycastResult = new List<RaycastResult>();
        eventSystem.RaycastAll(eventData, raycastResult);
        eventData.pointerCurrentRaycast = FindFirstRaycast(raycastResult);

        ExecuteEvents.Execute(eventData.pointerPress, eventData, ExecuteEvents.pointerUpHandler);
        GameObject currentPointerDown = ExecuteEvents.GetEventHandler<IPointerDownHandler>(eventData.pointerCurrentRaycast.gameObject);
        if (eventData.pointerPress == currentPointerDown)
        {
            ExecuteEvents.Execute(eventData.pointerPress, eventData, ExecuteEvents.pointerClickHandler);
        }
        eventData.pointerPress = null;
        if(eventData.dragging)
        {
            ExecuteEvents.Execute(eventData.pointerDrag, eventData, ExecuteEvents.endDragHandler);
            eventData.pointerDrag = null;
            eventData.dragging = false;
        }
    }

    private void OnTriggerPressed(IXRControllerInterface Controller, PointerEventData eventData)
    {
        if (Controller == null || eventData == null)
        {
            return;
        }

        eventData.position = Controller.GetPointerScreenPosition();
        List<RaycastResult> raycastResult = new List<RaycastResult>();
        eventSystem.RaycastAll(eventData, raycastResult);
        eventData.pointerCurrentRaycast = FindFirstRaycast(raycastResult);

        GameObject pointerDownObject = ExecuteEvents.GetEventHandler<IPointerDownHandler>(eventData.pointerCurrentRaycast.gameObject);

        if (pointerDownObject != null)
        {
            ExecuteEvents.Execute(pointerDownObject, RightControllerData, ExecuteEvents.pointerDownHandler);
            RightControllerData.pointerPressRaycast = RightControllerData.pointerCurrentRaycast;
            RightControllerData.eligibleForClick = true;
            eventData.pointerPress = pointerDownObject;
        }

        GameObject pointerDragObj = ExecuteEvents.GetEventHandler<IDragHandler>(eventData.pointerCurrentRaycast.gameObject);
        if(pointerDragObj != null)
        {
            ExecuteEvents.Execute(pointerDragObj, eventData, ExecuteEvents.initializePotentialDrag);
            eventData.pointerPressRaycast = eventData.pointerCurrentRaycast;
            eventData.dragging = true;
            eventData.pointerDrag = pointerDragObj;
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (playerInput != null)
        {
            playerInput.Enable();
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        if(playerInput != null)
        {
            playerInput.Disable();
        }
    }


}
