using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Threat : MonoBehaviour
{
    public abstract void Init(ThreatSpawner spawner);
    // Start is called before the first frame update
    void Start()
    {
        
    }
}
