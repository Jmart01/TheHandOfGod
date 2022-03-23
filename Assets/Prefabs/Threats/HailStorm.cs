using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HailStorm : Threat, IDragable
{
    [SerializeField] GameObject explosionEffect;
    [SerializeField] Transform explosionTrans;
    Coroutine damageOvertimeCoroutine;
    OrbitMovementComp orbitMovementComp;
    //Rigidbody _rb;
    public void Grabbed(GameObject grabber, Vector3 grabPoint)
    {
        orbitMovementComp.enabled = false;
        //_rb.isKinematic = true;
        transform.parent = grabber.transform;
        transform.position = grabPoint;
    }

    public override void Init(ThreatSpawner spawner)
    {
        orbitMovementComp = GetComponent<OrbitMovementComp>();
        //_rb = GetComponent<Rigidbody>();
        Transform walkmanTrans = GameplayStatic.GetWalkmanTransform();
        Vector3 spawnRotUp = new Vector3(Random.Range(0, 360), 0, 0);
        Vector3 spawnRotForward = transform.forward * Random.Range(0, 50);
        Quaternion spawnRot = Quaternion.LookRotation(spawnRotForward, spawnRotUp);

        orbitMovementComp.SetRotation(spawnRot);

    }

    public void Released(Vector3 ThrowVelocity)
    {
        orbitMovementComp.enabled = true;
        transform.parent = null;
        //_rb.isKinematic = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<WalkMan>())
        {
            damageOvertimeCoroutine = StartCoroutine(DamageOverTime(other.gameObject,-1));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.GetComponent<WalkMan>())
        {
            StopCoroutine(damageOvertimeCoroutine);
        }
    }

    IEnumerator DamageOverTime(GameObject other,float damageToDo)
    {
        while(other.GetComponent<HealthComp>().GetHitpoints() > 0)
        {
            other.GetComponent<HealthComp>().ChangeHealth(damageToDo * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Threat>())
        {
            BlowUp();
        }
    }

    private void BlowUp()
    {
        GameObject clonedEffect = Instantiate(explosionEffect, explosionTrans.position, transform.rotation);
        StartCoroutine(DestroyEffectAndGameObject(clonedEffect));
    }

    IEnumerator DestroyEffectAndGameObject(GameObject effect)
    {
        Destroy(gameObject);
        yield return new WaitForSeconds(0.5f);
        Destroy(effect);
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
