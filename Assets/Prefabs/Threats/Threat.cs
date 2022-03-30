using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Threat : MonoBehaviour
{
    public Action OnDestroyed { get; internal set; }

    public abstract void Init(ThreatSpawner spawner);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnDestroy()
    {
        OnDestroyed?.Invoke();
        OnDestroyed = null;
    }
}
