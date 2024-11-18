using System;
using ObjectPooling;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AssetLoader : MonoBehaviour
{
    public event Action AssetLoadedEvent;
    
    [SerializeField] private string _nextScene = "GameScene";
    [SerializeField] private TextMeshProUGUI _subTitleText;
    [SerializeField] private SingletonPoolManager _poolManager;
    [SerializeField] private AssetDBSO _assetDB;
    [SerializeField] private TextMeshProUGUI _loadingText;

    private bool _isLoadComplete;
    private int _toLoadCount;
    private int _currentLoadedCount;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _subTitleText.gameObject.SetActive(false);
        _isLoadComplete = false;

        _currentLoadedCount = 0;
        _assetDB.LoadCountEvent += AddLoadCount;
        _assetDB.LoadMessageEvent += LoadComplete;
        _assetDB.Initialize();

        foreach (PoolManagerSO poolManager in _poolManager.poolManagerList)
        {
            poolManager.LoadCountEvent   += AddLoadCount;
            poolManager.LoadMessageEvent += LoadComplete;
        }
        
        AssetLoadedEvent += () =>
        {
            _subTitleText.gameObject.SetActive(true);
            _isLoadComplete = true;
        };
    }

    private void OnDestroy()
    {
        foreach (PoolManagerSO poolManager in _poolManager.poolManagerList)
        {
            poolManager.LoadCountEvent   -= AddLoadCount;
            poolManager.LoadMessageEvent -= LoadComplete;
        }
    }

    private void Update()
    {
        if (_isLoadComplete && Input.anyKey)
        {
            SceneManager.LoadScene(_nextScene);
            _isLoadComplete = false;
        }
    }

    private void AddLoadCount(int count)
    {
        _toLoadCount += count;
        UpdateLoadText("Ready to load");
    }

    private void LoadComplete(int count, string message)
    {
        _currentLoadedCount += count;
        UpdateLoadText(message);

        if(_currentLoadedCount >= _toLoadCount)
        {
            AssetLoadedEvent?.Invoke();
            Debug.Log("Complete");
        }
    }

    private void UpdateLoadText(string text)
    {
        _loadingText.text = $"Loading : {text} - {_currentLoadedCount} / {_toLoadCount}";
    }
}