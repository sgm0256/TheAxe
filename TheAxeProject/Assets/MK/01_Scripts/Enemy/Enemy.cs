using System;
using UnityEngine;

namespace MK.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private PoolManagerSO _enemyPoolManager;
        public PoolManagerSO EnemyPoolManager => _enemyPoolManager;
        
        // TODO : Add StatSystem
        
        private void Initialize()
        {
            _enemyPoolManager.InitializePool(transform);
        }
        
        private void Awake()
        {
            Initialize();
        }
    }
}
