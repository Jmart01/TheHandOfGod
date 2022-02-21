using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRHand : MonoBehaviour
{
    Animator XRHandAnimator;
    LineRenderer pointingLine;
    private void Start()
    {
        XRHandAnimator = GetComponent<Animator>();
        pointingLine = GetComponent<LineRenderer>();
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
        if(input < .5f)
        {
            HoldObject();
        }
        else
        {
            //release functionality
        }
    }

    public void HoldObject()
    {
        RaycastHit hit;
        pointingLine.SetPosition(0, transform.position);
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            pointingLine.enabled = true;
            pointingLine.SetPosition(1, hit.transform.position);
            hit.transform.position = transform.position;
            hit.transform.parent = transform;
        }else
        {
            pointingLine.enabled = false;
        }
    }
}
