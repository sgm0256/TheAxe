using System;
using System.Collections;
using System.Collections.Generic;
using MK.Enemy;
using MKDir;
using ObjectPooling;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    [field: SerializeField] public PoolManagerSO PoolManager;
    public List<PoolTypeSO> enemyPool = new List<PoolTypeSO>();
    public List<Transform> SpawnPoint = new List<Transform>();

    private bool _isStopSpawn = false;

    [SerializeField] private float _spawnTime = 2.5f;
    
    // TODO : 일정 시간마다 풀을 출력해야 함
    // TODO : 랜덤한 위치로 출력
    // TODO : 쿨타임 추가
    
    protected override void Awake()
    {
        base.Awake();
        PoolManager.InitializePool(transform);
    }

    private void Start()
    {
        SpawnStart();
    }

    public void SpawnStart()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (_isStopSpawn)
                _isStopSpawn = false;
            else
                _isStopSpawn = true;
        }
    }

    private IEnumerator SpawnCoroutine()
    {
        // TODO : Spawn 내용 추가 

        while (_isStopSpawn == false)
        {
            Enemy enemy =  PoolManager.Pop(enemyPool[Random.Range(0, enemyPool.Count)]) as Enemy;
            enemy.transform.position = SpawnPoint[Random.Range(0, SpawnPoint.Count)].position;
            yield return new WaitForSeconds(_spawnTime);
        }
        
        /*if (_isStopSpawn)
            yield return null;
        
        yield return null;*/
    }
}
