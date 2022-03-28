using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkMan : MonoBehaviour
{
    HealthComp healthComp;
    [SerializeField] LayerMask ThreatMask;
    // Start is called before the first frame update
    void Start()
    {
        healthComp = GetComponent<HealthComp>();

        healthComp.noHealthLeft += NoHealthLeft;
    }

    private void NoHealthLeft(GameObject killer)
    {
        Debug.Log("I Have No health Left, end my suffering!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*private void OnTriggerEnter(Collider other)
    {
        Car otherAsCar = other.GetComponentInParent<Car>();
        if(otherAsCar)
        {
            Debug.Log("other as car should take away the health now");
            HealthComp walkmanHealthComp = GetComponent<HealthComp>();
            walkmanHealthComp.ChangeHealth(otherAsCar.GetDmgToWalkman());
            otherAsCar.ExplodeOnWalkman();
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"collided with {other.gameObject.name}");
        //the layer is at what digit from 0-32
        int otherLayer = other.gameObject.layer;

        //already the binary number
        int LayerMaskData = ThreatMask.value;
        //converts otherLayer to binary
        int otherLayerAsBinary = 1 << otherLayer;
        int result = otherLayerAsBinary & LayerMaskData;
        if (result != 0)
        {
            Debug.Log("Can damage due to car");
            healthComp.ChangeHealth(-1);
            other.GetComponentInParent<Car>().ExplodeOnWalkman();
        }
    }
}
