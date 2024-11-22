using System;
using System.Collections;
using MKDir;
using ObjectPooling;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoSingleton<GameManager>
{
    public event Action OnStatUpEvent;
    
    public Player Player => player;
    [field: SerializeField] public float StatUpValue { get; private set; } = 30f;

    [SerializeField] private Player player;
    [SerializeField] private PoolManagerSO _enemyPoolManager;
    [SerializeField] private float _upgradeTime = 60f;
    
    public bool IsGameStart { get; set; } = false;
    public float CurrentGameTime { get => _gameTime; }
    public int CurrentGameMinute { get; private set; }

    private float _gameTime;
    private float _durationTime = 0;

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
            CurrentGameMinute++;
            _gameTime = 0f;
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
