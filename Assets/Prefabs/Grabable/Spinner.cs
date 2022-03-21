using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour,IDragable
{
    GameObject LookRef;
    [SerializeField]Transform ObjectToSpin;
    [SerializeField]Transform SpinOffset;
    [SerializeField] [Range(0, 1)] float Damping = 0.5f;
    [SerializeField] float SpinSpeed = 20f;

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void Grabbed(GameObject grabber, Vector3 grabPoint)
    {
        Debug.Log(grabber.name);
        LookRef.transform.position = grabPoint;
        LookRef.transform.parent = grabber.transform;
        SpinOffset.LookAt(LookRef.transform,Vector3.up);
        ObjectToSpin.transform.parent = SpinOffset;
    }

    public void Released(Vector3 ThrowVelocity)
    {
        ObjectToSpin.parent = transform;
        LookRef.transform.parent = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        LookRef = new GameObject($"{gameObject.name} look ref");
    }

    // Update is called once per frame
    void Update()
    {
        if(LookRef.transform.parent)
        {
            Quaternion goalRotation = Quaternion.LookRotation((LookRef.transform.position - SpinOffset.position).normalized, Vector3.up);
            float lerpAlpha = Mathf.Clamp(((1 - Damping) * SpinSpeed * Time.deltaTime), 0f ,1f);
            SpinOffset.rotation = Quaternion.Slerp(SpinOffset.rotation, goalRotation, lerpAlpha);
            //SpinOffset.LookAt(LookRef.transform, Vector3.up);
        }
    }
}
