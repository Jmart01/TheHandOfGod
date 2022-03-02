using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Threat, IDragable
{
    [SerializeField] Transform[] laneTrans;
    [SerializeField] Transform CarPivot;
    [SerializeField] float LaneSpeed = 5f;
    [SerializeField]
    LayerMask LaneDetectionLayerMask;
    //[SerializeField] Transform CarTrans;

    Transform DestinationLane;
    GameObject DragRef;

    private void Start()
    {
        if(!HasAvailableLane())
        {
            Destroy(gameObject);
            return;
        }
        DragRef = new GameObject($"{gameObject.name} drag ref");
        PickRandomLane();
    }
    public void Grabbed(GameObject grabber, Vector3 grabPoint)
    {
        DragRef.transform.position = grabPoint;
        DragRef.transform.parent = grabber.transform;
        //Debug.Log($"{DragRef.gameObject.transform.parent.name}");
    }


    private void PickRandomLane()
    {
        if(laneTrans.Length == 0)
        {
            return;
        }
        int randomIndex = Random.Range(0, laneTrans.Length);

        if(CanMoveToLane(laneTrans[randomIndex]))
        {
            DestinationLane = laneTrans[randomIndex];
            return;
        }
        PickRandomLane();
    }

    bool HasAvailableLane()
    {
        bool HasAvailableLane = false;
        foreach(var lane in laneTrans)
        {
            if(CanMoveToLane(lane))
            {
                return true;
            }
        }
        return false;
    }


    private void Update()
    {
        if(DragRef.transform.parent != null && laneTrans.Length != 0)
        {
            Debug.Log("Dragging");
            Transform ClosestLane = laneTrans[0];
            float closestDistance = Vector3.Distance(DragRef.transform.position, ClosestLane.position);
            foreach(var lane in laneTrans)
            {
                float distance = Vector3.Distance(DragRef.transform.position, lane.position);
                if(distance < closestDistance)
                {
                    closestDistance = distance;
                    ClosestLane = lane;
                }
            }
            if (CanMoveToLane(ClosestLane))
            {
                DestinationLane = ClosestLane;
            }
        }
        
        float LerpAlpha = Mathf.Clamp(Time.deltaTime * LaneSpeed,0f,1f);
        CarPivot.rotation = Quaternion.Slerp(CarPivot.rotation, DestinationLane.parent.rotation, LerpAlpha);
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
        DragRef.transform.parent = null;
    }

    bool CanMoveToLane(Transform lane)
    {
        BoxCollider CarCollider = GetComponentInChildren<BoxCollider>();
        Collider[] colliders = Physics.OverlapBox(lane.position, CarCollider.size / 2, lane.rotation, LaneDetectionLayerMask);
        foreach (Collider collider in colliders)
        {
            if(collider.gameObject != CarCollider.gameObject)
            {
                return false;
            }
        }
        return true;
    }


    private void OnDrawGizmos()
    {
        foreach(var lane in laneTrans)
        {
            BoxCollider CarCollider = GetComponentInChildren<BoxCollider>();
            if (!CanMoveToLane(lane))
            {
                Gizmos.DrawCube(lane.position, CarCollider.size);
            }
        }
    }
}
