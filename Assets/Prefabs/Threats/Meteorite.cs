using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : Threat, IDragable
{
    Vector3 playerTrans;
    float randMoveSpeed;
    float randRotation;


    private void Start()
    {
        setRandomMoveSpeed();
        playerTrans = FindObjectOfType<WalkMan>().gameObject.transform.position;
    }

    private float setRandomMoveSpeed()
    {
        randMoveSpeed = Random.Range(0.1f,2f);
        return randMoveSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        if(playerTrans != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTrans, randMoveSpeed);
        }
    }

    public override void Init()
    {
        OrbitMovementComp orbitMovementComp = GetComponent<OrbitMovementComp>();
    }

    public void Grabbed(GameObject grabber, Vector3 grabPoint)
    {
        throw new System.NotImplementedException();
    }

    public void Released(Vector3 ThrowVelocity)
    {
        
    }
}
