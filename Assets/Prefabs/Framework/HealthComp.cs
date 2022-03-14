using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnHealthChanged(float newValue, float oldValue, float maxValue, GameObject Causer);
public delegate void OnNoHealthLeft(GameObject killer);

public class HealthComp : MonoBehaviour
{
    [SerializeField] float HitPoints = 10;
    [SerializeField] float MaxHitPoints = 10;
    [SerializeField] LayerMask ThreatMask;


    public OnHealthChanged onHealthChanged;
    public OnNoHealthLeft noHealthLeft;

    Coroutine HealthRegenCoroutine;

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
            HealthRegenCoroutine = null;
        }
        if(onHealthChanged != null)
        {
            onHealthChanged.Invoke(HitPoints, oldValue, MaxHitPoints, Causer);
            if(HealthRegenCoroutine != null)
            {
                StopCoroutine(HealthRegenCoroutine);
            }
            HealthRegenCoroutine = StartCoroutine(RegenHealth());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"collided with {other.gameObject.name}");
        //the layer is at what digit from 0-32
        int otherLayer = other.gameObject.layer;

        //already the binary number
        int LayerMaskData = ThreatMask.value;
        //converts otherLayer to binary
        int otherLayerAsBinary = 1 << otherLayer;
        int result = otherLayerAsBinary & LayerMaskData;
        if(result != 0)
        {
            Debug.Log("Can damage");
            //ChangeHealth(-1);
            //other.GetComponent<Car>().ExplodeOnWalkman();
        }
    }


    IEnumerator RegenHealth()
    {
        yield return new WaitForSeconds(5f);
        while(HitPoints < MaxHitPoints)
        {
            yield return new WaitForSeconds(.1f);
            ChangeHealth(2f);
        }
        StopCoroutine(HealthRegenCoroutine);
    }

    public void BroadCastCurrentHealth()
    {
        onHealthChanged.Invoke(HitPoints, HitPoints, MaxHitPoints, gameObject);
    }
    
}
