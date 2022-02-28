using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WhichHand
{
    LeftHand,
    RightHand
}

public class XRHand : MonoBehaviour
{
    Animator XRHandAnimator;
    [SerializeField] LaserPointer laserPointer;
    [SerializeField] GameObject GrabbingPoint;
    [SerializeField] Transform ThrowVelocityRefPoint;
    [SerializeField] WhichHand whichSide;
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
        _gripInput = input;
        XRHandAnimator.SetFloat("Grip", _gripInput);
        if(whichSide.Equals(WhichHand.LeftHand))
        {
            if(_gripInput > 0.9f)
            {
                //FindObjectOfType<Earth>().RotateUsingVelocity(Velocity.x);
                //return;
            }
        }
        
    }

    internal void StickUpdated(Vector2 stickInput)
    {
        if(whichSide.Equals(WhichHand.LeftHand) && _gripInput < 0.1f)
        {
            if(stickInput.x >0.5)
            {
                //FindObjectOfType<Earth>().
            }
        }
    }

    public void UpdateTriggerAnimation(float input)
    {
        XRHandAnimator.SetFloat("Trigger", input);
    }

    internal void TriggerReleased()
    {
        if(whichSide.Equals(WhichHand.RightHand))
        {
            if(objectInHand != null)
            {
                objectInHand.Released(Velocity);
            }
        }
    }

    internal void TriggerPressed()
    {
        if(whichSide.Equals(WhichHand.RightHand))
        {
            Debug.Log("trigger is pressed");
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
        }
    }

    private void Update()
    {
        
    }
}
