using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : Threat, IDragable
{
    [SerializeField] float minSpeed;
    [SerializeField] float maxSpeed;

    [SerializeField] float minRotSpeed;
    [SerializeField] float maxRotSpeed;
    [SerializeField] GameObject explosionEffect;

    private BoxCollider _spawnBoxBoundary;
    private Rigidbody _rigidbody;

    public override void Init(ThreatSpawner spawner)
    {
        _spawnBoxBoundary = spawner.GetMeteoriteSpawnBoxCollider();
        _rigidbody = GetComponent<Rigidbody>();
        Transform walkmanTrans = GameplayStatic.GetWalkmanTransform();

        Vector3 origin = _spawnBoxBoundary.transform.position;
        Vector3 range = _spawnBoxBoundary.size;
        Vector3 randomRange = new Vector3(Random.Range(-range.x, range.x), Random.Range(-range.y, range.y), Random.Range(-range.z, range.z));
        Vector3 randomSpawnCoordInBox = origin + randomRange;
        transform.position = randomSpawnCoordInBox;
        transform.LookAt(walkmanTrans, Vector3.up);

        float randomSpeed = Random.Range(minSpeed, maxSpeed);
        float randomRotSpeed = Random.Range(minRotSpeed, maxRotSpeed);

        _rigidbody.AddTorque(Vector3.right * randomRotSpeed, ForceMode.Force);
        _rigidbody.AddForce((walkmanTrans.position - transform.position) * randomSpeed, ForceMode.Force);
        
    }

    public void Grabbed(GameObject grabber, Vector3 grabPoint)
    {
        _rigidbody.isKinematic = true;
        transform.parent = grabber.transform;
        transform.position = grabPoint;
    }

    public void Released(Vector3 ThrowVelocity)
    {
        transform.parent = null;
        _rigidbody.isKinematic = false;
        _rigidbody.velocity = ThrowVelocity * 10;
        StartCoroutine(BlowUpCountDown());
    }

    IEnumerator BlowUpCountDown()
    {
        yield return new WaitForSeconds(3f);
        BlowUp();
    }

    private void BlowUp()
    {
        GameObject clonedEffect = Instantiate(explosionEffect,transform.position, transform.rotation);
        StartCoroutine(DestroyEffectAndGameObject(clonedEffect));
    }

    IEnumerator DestroyEffectAndGameObject(GameObject effect)
    {
        Destroy(gameObject);
        yield return new WaitForSeconds(.5f);
        Destroy(effect);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<WalkMan>())
        {
            collision.gameObject.GetComponent<HealthComp>().ChangeHealth(-5f);
        }
        else
        {
            BlowUp();
        }
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
