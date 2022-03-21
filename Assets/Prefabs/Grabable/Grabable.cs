using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabable : MonoBehaviour, IDragable
{
    [SerializeField] float transitionTime = 1f;
    [SerializeField]float throwForceMultiplier = 2;
    Transform holder;
    Coroutine grabbingCoroutine;
    Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    public void Grabbed(GameObject grabber, Vector3 grabPoint)
    {
        Debug.Log($"{grabber.name} grabbed me");
        transform.parent = grabber.transform;
        holder = grabber.transform;
        grabbingCoroutine = StartCoroutine(MoveToGrabberLoc());
        if(rigidbody)
        {
            rigidbody.isKinematic = true;
            rigidbody.velocity = Vector3.zero;
        }
        
    }

    public void Released(Vector3 ThrowVelocity)
    {
        Debug.Log("I AM FREE");
        if(grabbingCoroutine != null)
        {
            StopCoroutine(grabbingCoroutine);
            transform.parent = null;

            if(rigidbody)
            {
                rigidbody.isKinematic = false;
                rigidbody.velocity = ThrowVelocity * throwForceMultiplier;
            }
        }
    }


    IEnumerator MoveToGrabberLoc()
    {
        float timer = 0f;
        Vector3 startingPos = transform.position;
        while(timer < transitionTime)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
            transform.position = Vector3.Lerp(startingPos, holder.position, timer / transitionTime);
            yield return new WaitForEndOfFrame();
        }

        transform.position = holder.position;
        transform.parent = holder;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
