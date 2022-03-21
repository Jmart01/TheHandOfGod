using System.Collections;
using UnityEngine;


public class XRHand : MonoBehaviour, IXRControllerInterface
{
    Animator XRHandAnimator;
    [SerializeField] LaserPointer laserPointer;
    [SerializeField] GameObject GrabbingPoint;
    [SerializeField] Transform ThrowVelocityRefPoint;
    IDragable objectInHand;

    Vector3 Velocity;
    Vector3 OldPos;
    Vector3 PositionOneSecondBefore;
    private float _gripInput;

    IEnumerator CalculateAverageSpeed()
    {
        while(true)
        {
            Velocity = (ThrowVelocityRefPoint.position - OldPos) / 0.1f;
            OldPos = ThrowVelocityRefPoint.position;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void Start()
    {
        XRHandAnimator = GetComponentInChildren<Animator>();
        if(laserPointer == null)
        {
            laserPointer = GetComponentInChildren<LaserPointer>();
        }
        StartCoroutine(CalculateAverageSpeed());
    }
    public void UpdateLocalPosition(Vector3 location)
    {
        transform.localPosition = location;
    }

    internal void UpdateLocalRotation(Quaternion rotation)
    {
        transform.localRotation = rotation;
    }

    public void UpdateGripAnimation(float input)
    {
        XRHandAnimator.SetFloat("Grip", _gripInput);
        if (laserPointer != null && laserPointer.GetFocusedObject(out GameObject objectInFocus, out Vector3 ContactPoint))
        {
            IDragable objectAsDragable = objectInFocus.GetComponent<IDragable>();
            if (objectAsDragable == null)
            {
                objectAsDragable = objectInFocus.GetComponentInParent<IDragable>();
            }

            if (objectAsDragable != null)
            {
                objectAsDragable.Grabbed(GrabbingPoint, ContactPoint);
                objectInHand = objectAsDragable;
            }
        }
    }

    internal void StickUpdated(Vector2 stickInput)
    {
       
    }

    public void UpdateTriggerAnimation(float input)
    {
        XRHandAnimator.SetFloat("Trigger", input);
    }

    InventorySlot GetCurrentOverSlot()
    {
        if(laserPointer == null)
        {
            return null;
        }
        GameObject currentOverUI = laserPointer.GetCurrentPointingUI();
        if(currentOverUI != null)
        {
            return currentOverUI.GetComponent<InventorySlot>();
        }
        return null;
    }

    internal void TriggerReleased()
    {
        //drop down to inventory slot if hovering over slot and slot is empty and if the hand is holding an item
        InventoryComponent item = null;
        if (objectInHand as UnityEngine.Object)
        {
            item = objectInHand.GetGameObject().GetComponent<InventoryComponent>();
        }
        InventorySlot slot = GetCurrentOverSlot();
        if(slot != null && slot.IsSlotEmpty() && item != null)
        {
            slot.StoreItem(item);
            objectInHand = null;
        }
        if(objectInHand as UnityEngine.Object)
        {
            objectInHand.Released(Velocity);
        }
    }

    internal void TriggerPressed()
    {
        //Debug.Log("trigger is pressed");
        if (laserPointer != null && laserPointer.GetFocusedObject(out GameObject objectInFocus, out Vector3 ContactPoint))
        {
            IDragable objectAsDragable = objectInFocus.GetComponent<IDragable>();
            if(objectAsDragable == null)
            {
                objectAsDragable = objectInFocus.GetComponentInParent<IDragable>();
            }

            if(objectAsDragable != null)
            {
                objectAsDragable.Grabbed(GrabbingPoint, ContactPoint);
                objectInHand = objectAsDragable;
            }
        }
        InventorySlot slot = GetCurrentOverSlot();
        if(slot != null && !slot.IsSlotEmpty())
        {
            InventoryComponent item = slot.TakeOutItem();
            IDragable objectAsDragable = item.GetComponent<IDragable>();
            if(objectAsDragable as UnityEngine.Object)
            {
                objectInHand = objectAsDragable;
            }

        }
    }

    private void Update()
    {
        
    }

    public Vector2 GetPointerScreenPosition()
    {
        if(laserPointer != null)
        {
            return laserPointer.GetPointerScreenPosition();
        }
        return Vector2.zero;
    }
}
