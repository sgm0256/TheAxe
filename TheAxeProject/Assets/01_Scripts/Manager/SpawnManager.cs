using System.Collections;
using System.Collections.Generic;
using MKDir;
using ObjectPooling;
using UnityEngine;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    [field: SerializeField] public PoolManagerSO PoolManager;
    public List<Transform> SpawnPoint = new List<Transform>();

    private bool _isStopSpawn = false;
    
    // TODO : 일정 시간마다 풀을 출력해야 함
    // TODO : 랜덤한 위치로 출력
    // TODO : 쿨타임 추가
    
    protected override void Awake()
    {
        base.Awake();
        PoolManager.InitializePool(transform);
    }

    public void SpawnStart()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        // TODO : Spawn 내용 추가 

        if (_isStopSpawn)
            yield return null;
        
        yield return null;
    }
}
