using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Threat, IDragable
{
    [SerializeField] Transform[] laneTrans;
    Vector3 pointGrab;

    public void Grabbed(GameObject grabber, Vector3 grabPoint)
    {
        //Vector3 placeToMoveCar

    }

    private Vector3 ClosestLane(Vector3 point)
    {
        Transform closestLane = null;
        float closestDist = float.MaxValue;

        foreach(Transform lane in laneTrans)
        {
            float distance = Vector3.Distance(lane.localPosition, point);
            if(distance < closestDist)
            {
                closestLane = lane;
                closestDist = distance;
            }
        }
        return closestLane.localPosition;
    }

    public override void Init()
    {
        OrbitMovementComp orbitMovementComp = GetComponent<OrbitMovementComp>();
        Transform walkManTrans = GameplayStatic.GetWalkmanTransform();
        Vector3 SpawnRotUp = -walkManTrans.up;
        Vector3 SpawnRotForward = walkManTrans.forward;

        Quaternion SpawnRot = Quaternion.LookRotation(SpawnRotForward, SpawnRotUp);
        orbitMovementComp.SetRotation(SpawnRot);
    }

    public void Released(Vector3 ThrowVelocity)
    {
        
    }

}
