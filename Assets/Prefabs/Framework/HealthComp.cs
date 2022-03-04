using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnHealthChanged(float newValue, float oldValue, float maxValue, GameObject Causer);
public delegate void OnNoHealthLeft(GameObject killer);

public class HealthComp : MonoBehaviour
{
    [SerializeField] float HitPoints = 10;
    [SerializeField] float MaxHitPoints = 10;

    public OnHealthChanged onHealthChanged;
    public OnNoHealthLeft noHealthLeft;

    public void ChangeHealth(float changeAmount, GameObject Causer = null)
    {
        if(changeAmount < 0 && HitPoints == 0)
        {
            return;
        }

        float oldValue = HitPoints;
        HitPoints += changeAmount;
        HitPoints = Mathf.Clamp(HitPoints, 0f, MaxHitPoints);
        if(HitPoints == 0)
        {
            noHealthLeft.Invoke(Causer);
        }
        if(onHealthChanged != null)
        {
            onHealthChanged.Invoke(HitPoints, oldValue, MaxHitPoints, Causer);
        }
    }

    public void BroadCastCurrentHealth()
    {
        onHealthChanged.Invoke(HitPoints, HitPoints, MaxHitPoints, gameObject);
    }
    
}
