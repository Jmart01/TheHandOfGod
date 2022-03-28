using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreatSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct ThreatSpawnSetting
    {
        public Threat threatToSpawn;
        public float weight;
        public float originalWeight;
        public int currentCount;
        public int maxCount;
    }
    public class ThreatHandler
    {
        Threat _threatToSpawn;
        Vector2 _weightRange;
        int _maxCount;
        int _currentCount;

        public ThreatHandler(Threat threat, Vector2 weightRange, int maxCount)
        {
            _threatToSpawn = threat;
            _weightRange = weightRange;
            _maxCount = maxCount;
            _currentCount = 0;
        }

        public void SpawnThreat()
        {
            Threat newThreat = GameObject.Instantiate(_threatToSpawn);
            newThreat.Init(FindObjectOfType<ThreatSpawner>());
            newThreat.OnDestroyed += ReduceCount;
            _currentCount++;
        }

        private void ReduceCount()
        {
            _currentCount--;
        }

        public bool CanSpawn()
        {
            return _currentCount < _maxCount;
        }

        public bool IsFloatInWeightRange(float val)
        {
            return val > _weightRange.x && val < _weightRange.y;
        }
    }

    [SerializeField] ThreatSpawnSetting[] Threats;
    [SerializeField] float MinSpawnInterval = 1f;
    [SerializeField] float MaxSpawnInterval = 5f;
    [SerializeField] BoxCollider MeteoriteSpawner;
    float maxWeight = 0;
    List<ThreatHandler> threatHandlers;
    public BoxCollider GetMeteoriteSpawnBoxCollider()
    {
        return MeteoriteSpawner;
    }

    public float SetMaxSpawnInterval()
    {
        return MaxSpawnInterval = Mathf.Clamp(MaxSpawnInterval, MinSpawnInterval, MaxSpawnInterval - 1);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartSpawnThreat());
        PopulateThreatHandlers();
    }

    private void PopulateThreatHandlers()
    {
        threatHandlers = new List<ThreatHandler>();
        float weight = 0;
        foreach (ThreatSpawnSetting setting in Threats)
        {
            threatHandlers.Add(new ThreatHandler(setting.threatToSpawn, new Vector2(weight, weight + setting.weight), setting.maxCount));
            weight += setting.weight;
        }
        maxWeight = weight;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartSpawnThreat()
    {
        while(true)
        {
            yield return new WaitForSeconds(Mathf.Lerp(MinSpawnInterval, MaxSpawnInterval, UnityEngine.Random.Range(0f, 1f)));
            SpawnThreat();
        }
    }

    private void SpawnThreat()
    {
        float randomPointer = UnityEngine.Random.Range(0, maxWeight);
        foreach(ThreatHandler handler in threatHandlers)
        {
            if(handler.IsFloatInWeightRange(randomPointer) && handler.CanSpawn())
            {
                handler.SpawnThreat();
                break;
            }
        }
    }
}
