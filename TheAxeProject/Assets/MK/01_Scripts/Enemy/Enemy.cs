using System;
using UnityEngine;

namespace MK.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private PoolManagerSO _poolManager;
        public PoolManagerSO PoolManager => _poolManager;

        private void Awake()
        {
            _poolManager.InitializePool(transform);
        }
    }
}
