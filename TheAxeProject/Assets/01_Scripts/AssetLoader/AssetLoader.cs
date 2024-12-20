//using System;
//using MKDir;
//using ObjectPooling;
//using TMPro;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class AssetLoader : MonoSingleton<AssetLoader>
//{
//    public event Action AssetLoadedEvent;
    
//    public string _nextScene = "GameScene";
    
//    [SerializeField] private TextMeshProUGUI _subTitleText;
//    [SerializeField] private TextMeshProUGUI _loadingText;
//    [SerializeField] private SingletonPoolManager _poolManager;
//    [SerializeField] private AssetDBSO _assetDB;
//    [SerializeField] private TextMeshProUGUI _loadingCountText;

//    public bool IsLoadComplete => _isLoadComplete;
    
//    private bool _isLoadComplete;
//    private int _toLoadCount;
//    private int _currentLoadedCount;
    
//    protected override void Awake()
//    {
//        if (_isLoadComplete)
//        {
//            SceneManager.LoadScene(_nextScene);
//        }
//        else
//        {
//            base.Awake();

//            _currentLoadedCount = 0;
//            _assetDB.LoadCountEvent += AddLoadCount;
//            _assetDB.LoadMessageEvent += LoadComplete;
//            _assetDB.Initialize();

//            foreach (PoolManagerSO poolManager in _poolManager.poolManagerList)
//            {
//                poolManager.LoadCountEvent   += AddLoadCount;
//                poolManager.LoadMessageEvent += LoadComplete;
//            }
        
//            AssetLoadedEvent += () =>
//            {
//                _loadingText.gameObject.SetActive(false);
//                _subTitleText.gameObject.SetActive(true);
//                _isLoadComplete = true;
//            };
//        }
//    }

//    private void Start()
//    {
//        if (_isLoadComplete)
//        {
//            SceneManager.LoadScene(_nextScene);
//        }
//    }

//    private void Update()
//    {
//        if (_isLoadComplete && Input.anyKey)
//        {
//            GameManager.Instance.StartGameCoroutine();
//            SceneManager.LoadScene(_nextScene);
//            GameManager.Instance.IsGameStart = true;
//            _isLoadComplete = false;
//        }
//    }

//    private void AddLoadCount(int count)
//    {
//        _toLoadCount += count;
//        UpdateLoadText("Ready to load");
//    }

//    private void LoadComplete(int count, string message)
//    {
//        _currentLoadedCount += count;
//        UpdateLoadText(message);

//        if(_currentLoadedCount >= _toLoadCount)
//        {
//            AssetLoadedEvent?.Invoke();
//            Debug.Log("Complete");
//        }
//    }

//    private void UpdateLoadText(string text)
//    {
//        _loadingCountText.text = $"Loading : {text} - {_currentLoadedCount} / {_toLoadCount}";
//    }
//}