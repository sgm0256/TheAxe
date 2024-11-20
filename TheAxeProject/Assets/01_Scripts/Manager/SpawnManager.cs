using System.Collections;
using System.Collections.Generic;
using MK.Enemy;
using MKDir;
using ObjectPooling;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    // TODO : 15분 게임으로 만들기
    // TODO : UI 작업
    // TODO : 적 비주얼 찾기
    // TODO : 시간 지날 때 마다 적 스탯 강화
    public List<PoolTypeSO> enemyPool = new List<PoolTypeSO>();
    public List<Transform> SpawnPoint = new List<Transform>();

    [SerializeField] private float _spawnTime = 2f;
    [SerializeField] private float _spawnDecreaseTime = 0.3f;
    [SerializeField] private float _waveCooldown = 15f;
    [SerializeField] private float _waveIncreaseTime = 3f;
    
    private PoolManagerSO _poolManager;
    private bool _isStopSpawn = false;
    private bool _isStartSpawn = false;

    private float _time;

    private void Start()
    {
        _time = _waveCooldown;
        _poolManager = SingletonPoolManager.Instance.GetPoolManager(PoolEnumType.Enemy);
        SpawnStart();
    }

    public void SpawnStart()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private void Update()
    {
        if (_isStartSpawn)
        {
            _time -= Time.deltaTime;

            if (_time <= 0)
            {
                _spawnTime -= _spawnDecreaseTime;
                _time = _waveCooldown + _waveIncreaseTime;
            }
        }
        
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
        _isStartSpawn = true;
        while (_isStopSpawn == false)
        {
            Enemy enemy = _poolManager.Pop(enemyPool[Random.Range(0, enemyPool.Count)]) as Enemy;
            enemy.transform.position = SpawnPoint[Random.Range(0, SpawnPoint.Count)].position;
            yield return new WaitForSeconds(_spawnTime);
        }
        
        _isStartSpawn = false;
    }
}
