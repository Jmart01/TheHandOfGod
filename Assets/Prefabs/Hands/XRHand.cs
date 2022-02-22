using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRHand : MonoBehaviour
{
    Animator XRHandAnimator;
    LineRenderer pointingLine;
    [SerializeField] Transform holdObjectTrans;
    RaycastHit hit;
    private void Start()
    {
        XRHandAnimator = GetComponent<Animator>();
        pointingLine = GetComponent<LineRenderer>();
        pointingLine.SetPosition(0, transform.localPosition);
        pointingLine.enabled = false;
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
        XRHandAnimator.SetFloat("Grip", input);
        
    }

    public void UpdateTriggerAnimation(float input)
    {
        XRHandAnimator.SetFloat("Trigger", input);
        if(input > .5f)
        {
            if(holdObjectTrans.childCount < 1)
            {
                HoldObject();
            }
            else
            {
                Debug.Log("Cant hold more than 1 item");
            }   
        }
        else
        {
            if(holdObjectTrans.childCount == 1)
            {
                DropObject();
            }
        }
    }

    public void HoldObject()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            pointingLine.enabled = true;
            pointingLine.SetPosition(1, hit.transform.position);
            hit.transform.position = holdObjectTrans.position;
            hit.transform.parent = holdObjectTrans;
        }else
        {
            pointingLine.enabled = false;
        }
    }
    public void DropObject()
    {
        hit.transform.parent = null;
    }
}
