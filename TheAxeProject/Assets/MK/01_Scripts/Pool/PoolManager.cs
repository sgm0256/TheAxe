using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Pool/Manager")]
public class PoolManagerSO : ScriptableObject
{
    public List<PoolTypeSO> poolList = new();
    private Dictionary<PoolTypeSO, Pool> _pools;
    [SerializeField] private Transform _rootTrm;

    public event Action<int> LoadCountEvent;
    public event Action<int, string> LoadMessageEvent;

    public void InitializePool(Transform root)
    {
        _rootTrm = root;
        _pools = new Dictionary<PoolTypeSO, Pool>();

        foreach (var poolType in poolList)
        {
            var pool = new Pool(poolType, _rootTrm, poolType.initCount);
            LoadCountEvent?.Invoke(poolType.initCount);
            pool.LoadCompleteEvent += () =>
            {
                LoadMessageEvent?.Invoke(poolType.initCount, $"{poolType.typeName} is loaded");
            };

            _pools.Add(poolType, pool);
        }
    }

    public IPoolable Pop(PoolTypeSO type)
    {
        if (_pools.TryGetValue(type, out Pool pool))
        {
            return pool.Pop();
        }
        return null;
    }

    public void Push(IPoolable item)
    {
        if (_pools.TryGetValue(item.PoolType, out Pool pool))
        {
            pool.Push(item);
        }
    }
}