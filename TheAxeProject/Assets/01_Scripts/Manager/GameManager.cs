using System;
using System.Collections;
using MKDir;
using ObjectPooling;
using TMPro;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public event Action OnStatUpEvent;
    
    public Player Player => player;
    [field: SerializeField] public float StatUpValue { get; private set; } = 30f;

    [SerializeField] private Player player;
    [SerializeField] private PoolManagerSO _enemyPoolManager;
    [SerializeField] private float _upgradeTime = 60f;
    [SerializeField] private float _hpIncreaseValue = 3f;
    
    private bool _isGameStart = false;
    private float _time = 0;

    private void Update()
    {
        if (_isGameStart)
        {
            _time += Time.deltaTime;

            if (_time >= _upgradeTime)
            {
                StatUP();
                _time = 0;
            }
        }
    }

    public void StatUP()
    {
        OnStatUpEvent?.Invoke();
    }

    public void StartGameCoroutine()
    {
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        while (_isGameStart)
        {
            // TODO : 1분 마다 스폰 올리기  
            yield return new WaitForSeconds(_upgradeTime);
        }
    }
}
