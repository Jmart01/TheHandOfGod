using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRHand : MonoBehaviour
{
    Animator XRHandAnimator;

    private void Start()
    {
        XRHandAnimator = GetComponent<Animator>();
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
        Debug.Log("input");
    }

    public void UpdateTriggerAnimation(float input)
    {
        XRHandAnimator.SetFloat("Trigger", input);
    }
}
