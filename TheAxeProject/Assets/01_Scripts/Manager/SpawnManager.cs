using System.Collections;
using System.Collections.Generic;
using MK.Enemy;
using MKDir;
using ObjectPooling;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    public List<PoolTypeSO> enemyPool = new List<PoolTypeSO>();
    public List<Transform> SpawnPoint = new List<Transform>();
    public bool IsWave { get; set; } = false;
    
    [SerializeField] private float _spawnTime = 2.5f;
    [SerializeField] private float _spawnDecreaseTime = 0.1f;
    [SerializeField] private float _spawnDecreaseCooldown = 30f;
    [SerializeField] private float _waveCooldown = 60f;
    [SerializeField] private int _waveCount = 50;
    
    private PoolManagerSO _poolManager;
    private bool _isStopSpawn = false;
    private bool _isStartSpawn = false;

    private void Start()
    {
        _poolManager = SingletonPoolManager.Instance.GetPoolManager(PoolEnumType.Enemy);
        SpawnStart();
    }

    public void SpawnStart()
    {
        StartSpawn();
        StartCoroutine(SpawnCoroutine());
        StartCoroutine(SpawnDecreaseTime());
        StartCoroutine(WaveTime());
    }

    private void Update()
    {
        // TODO : IsStopSpawn 바꾸기 
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (_isStopSpawn)
                _isStopSpawn = false;
            else
                _isStopSpawn = true;
        }
    }

    private PoolTypeSO RandomEnemyType()
    {
        // 1. 근거리
        // 2. 돌진
        // 3. 원거리

        int rand = Random.Range(0, 101);

        // 근거리 50%
        if (rand < 50)
        {
            return enemyPool[0];
        }
        // 돌진 30%
        else if (rand < 80)
        {
            return enemyPool[1];
        }
        // 원거리 20%
        else
        {
            return enemyPool[2];
        }
    }

    private void StartSpawn()
    {
        for (int i = 0; i < 10; ++i)
        {
            Enemy enemy = _poolManager.Pop(RandomEnemyType()) as Enemy;
            enemy.transform.position = SpawnPoint[Random.Range(0, SpawnPoint.Count)].position;
        }
    }

    private void EnemyWave()
    {
        //Vector2 spawnPoints = SpawnPoint[Random.Range(0, SpawnPoint.Count)].position;
        for (int i = 0; i < _waveCount; ++i)
        {
            Enemy enemy = _poolManager.Pop(RandomEnemyType()) as Enemy;
            enemy.transform.position = SpawnPoint[Random.Range(0, SpawnPoint.Count)].position;
        }

        IsWave = false;
    }

    private IEnumerator WaveTime()
    {
        if (_isStopSpawn) yield return null;
        
        yield return new WaitForSeconds(_waveCooldown);
        EnemyWave();
    }

    private IEnumerator SpawnDecreaseTime()
    {
        if (_isStopSpawn) yield return null;
        
        yield return new WaitForSeconds(_spawnDecreaseCooldown);
        _spawnTime -= _spawnDecreaseTime;
    }
    
    private IEnumerator SpawnCoroutine()
    {
        _isStartSpawn = true;
        while (_isStopSpawn == false)
        {
            Enemy enemy = _poolManager.Pop(RandomEnemyType()) as Enemy;
            enemy.transform.position = SpawnPoint[Random.Range(0, SpawnPoint.Count)].position;
            
            yield return new WaitForSeconds(_spawnTime);
        }
        
        _isStartSpawn = false;
    }
}
