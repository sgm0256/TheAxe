using System;
using System.Collections;
using MKDir;
using ObjectPooling;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public event Action OnStatUpEvent;
    public event Action OnGameClearEvent;
    
    public Player Player => player;
    [field: SerializeField] public float StatUpValue { get; private set; } = 30f;

    [SerializeField] private Player player;
    [SerializeField] private PoolManagerSO _enemyPoolManager;
    [SerializeField] private float _upgradeTime = 60f;
    
    public bool IsGameStart { get; set; } = false;
    public bool IsGameClear { get; set; } = false;
    public float CurrentGameTime => _gameTime;
    public int CurrentGameMinute => _gameMinute;

    public int CurrentEnemyKillCount
    {
        get => _enemyKillCount;
        set => _enemyKillCount = value;
    }

    private float _gameTime = 0f;
    private float _durationTime = 0f;
    private int _gameMinute = 0;
    private int _enemyKillCount = 0;

    public void SetGame()
    {
        _gameTime = 0f;
        _durationTime = 0f;
        _gameMinute = 0;
        _enemyKillCount = 0;
    }

    private void Update()
    {
        if (IsGameStart)
        {
            _durationTime += Time.deltaTime;

            if (_durationTime >= _upgradeTime)
            {
                StatUP();
                _durationTime = 0;
            }
        }

        _gameTime += Time.deltaTime;
        if (_gameTime >= 60f)
        {
            _gameMinute++;
            _gameTime = 0f;
        }

        if (_gameMinute >= 10 && IsGameClear == false)
        {
            IsGameClear = true;
            OnGameClearEvent?.Invoke();
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
        while (IsGameStart)
        {
            // 스폰 업그레이드 쿨타임
            yield return new WaitForSeconds(_upgradeTime);
            SpawnManager.Instance.IsWave = true;
        }
    }
}
