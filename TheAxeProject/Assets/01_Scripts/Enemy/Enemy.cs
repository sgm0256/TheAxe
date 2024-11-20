using Core.Entities;
using ObjectPooling;
using UnityEngine;

namespace MK.Enemy
{
    public abstract class Enemy : Entity, IPoolable
    {
        [field: SerializeField] public PoolTypeSO PoolType { get; set; }
        public GameObject GameObject { get => gameObject; }

        private Pool _myPool;
        private EntityHealth _health;
        
        public void SetUpPool(Pool pool)
        {
            _myPool = pool;
        }

        public void ResetItem()
        {
            
        }

        protected override void Awake()
        {
            base.Awake();
            _health = GetCompo<EntityHealth>();
            _health.OnDeadEvent.AddListener(EnemyPoolPush);
        }

        public void EnemyPoolPush()
        {
            SingletonPoolManager.Instance.Push(PoolEnumType.Enemy, this);
        }
    }
}
