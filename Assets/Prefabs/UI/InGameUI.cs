using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameUI : MonoBehaviour
{
    [SerializeField] Image ProgressBar;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] Canvas DeadMenu;
    float timeElapsed = 0;
    [SerializeField] ThreatSpawner threatSpawner;
    // Start is called before the first frame update
    void Start()
    {
        HealthComp WalkmanHealthComp = FindObjectOfType<WalkMan>().GetComponent<HealthComp>();
        WalkmanHealthComp.onHealthChanged += WalkmanHealthChanged;
        WalkmanHealthComp.noHealthLeft += NoHealthLeft;
        WalkmanHealthComp.BroadCastCurrentHealth();
        DeadMenu.enabled = false;
    }

    private void NoHealthLeft(GameObject killer)
    {
        DeadMenu.enabled = true;
        Time.timeScale = 0;
    }

    private void WalkmanHealthChanged(float newValue, float oldValue, float maxValue, GameObject Causer)
    {
        SetWalkmanHealth(newValue / maxValue);
    }

    private void SetWalkmanHealth(float percent)
    {
        if(ProgressBar != null)
        {
            ProgressBar.material.SetFloat("_Progress", percent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = Mathf.Round(timeElapsed).ToString();
        timeElapsed += Time.deltaTime;
        if(timeElapsed % 5 == 0)
        {
            threatSpawner.SetMaxSpawnInterval();
        }
    }
}
