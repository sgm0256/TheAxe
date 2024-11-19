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

    private PoolManagerSO _poolManager;
    private bool _isStopSpawn = false;

    [SerializeField] private float _spawnTime = 2.5f;

    private void Start()
    {
        _poolManager = SingletonPoolManager.Instance.GetPoolManager(PoolEnumType.Enemy);
        SpawnStart();
    }

    public void SpawnStart()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (_isStopSpawn)
                _isStopSpawn = false;
            else
                _isStopSpawn = true;
        }
    }

    private IEnumerator SpawnCoroutine()
    {
        while (_isStopSpawn == false)
        {
            Enemy enemy = _poolManager.Pop(enemyPool[Random.Range(0, enemyPool.Count)]) as Enemy;
            enemy.transform.position = SpawnPoint[Random.Range(0, SpawnPoint.Count)].position;
            yield return new WaitForSeconds(_spawnTime);
        }
    }
}
