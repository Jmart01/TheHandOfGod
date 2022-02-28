using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitMovementComp : MonoBehaviour
{
    [SerializeField] float OrbitAngularSpeed;
    [SerializeField] Transform OrbitAround;
    [SerializeField] Vector3 OrbitAxis;
    // Start is called before the first frame update
    void Start()
    {
        if(OrbitAround == null)
        {
            OrbitAround = FindObjectOfType<Earth>()?.transform;
        }
        if(OrbitAround)
        {
            transform.parent = OrbitAround;
            transform.localPosition = Vector3.zero;
        }
    }

    internal void SetRotation(Quaternion spawnRot)
    {
        transform.rotation = spawnRot;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(
            OrbitAxis.x * OrbitAngularSpeed * Time.deltaTime,
            OrbitAxis.y * OrbitAngularSpeed * Time.deltaTime,
            OrbitAxis.z * OrbitAngularSpeed * Time.deltaTime);
    }
}
