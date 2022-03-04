using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] Image ProgressBar;
    // Start is called before the first frame update
    void Start()
    {
        HealthComp WalkmanHealthComp = FindObjectOfType<WalkMan>().GetComponent<HealthComp>();
        WalkmanHealthComp.onHealthChanged += WalkmanHealthChanged;
        WalkmanHealthComp.BroadCastCurrentHealth();

    }

    private void WalkmanHealthChanged(float newValue, float oldValue, float maxValue, GameObject Causer)
    {
        SetWalkmanHealth(newValue / maxValue);
    }

    private void SetWalkmanHealth(float percent)
    {
        ProgressBar.material.SetFloat("_Progress", percent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
