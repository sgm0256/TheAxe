using UnityEngine;

namespace MK.Enemy
{
    public abstract class Enemy : MonoBehaviour
    {
        [SerializeField] protected PoolManagerSO _enemyPoolManager;
        public PoolManagerSO EnemyPoolManager => _enemyPoolManager;
        
        // TODO : Add StatSystem

        protected Rigidbody2D _myRigid;
        public Rigidbody2D MyRigid => _myRigid;
        
        protected virtual void Initialize()
        {
            if (_enemyPoolManager != null)
            {
                _enemyPoolManager.InitializePool(transform);
            }
            _myRigid = GetComponent<Rigidbody2D>();
        }
        
        protected virtual void Awake()
        {
            Initialize();
        }
    }
}
