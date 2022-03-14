using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkMan : MonoBehaviour
{
    HealthComp healthComp;
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
}
