using Core.Entities;
using Core.StatSystem;
using ObjectPooling;
using UnityEngine;

namespace MK.Enemy
{
    public abstract class Enemy : Entity, IPoolable
    {
        [field: SerializeField] public PoolTypeSO PoolType { get; set; }
        public GameObject GameObject { get => gameObject; }

        protected Pool _myPool;
        protected EntityHealth _health;
        protected EntityStat _stat;
        
        public void SetUpPool(Pool pool)
        {
            _myPool = pool;
        }

        public void ResetItem()
        {
            _health.SetHealth();
        }

        protected override void Awake()
        {
            base.Awake();
            _health = GetCompo<EntityHealth>();
            _stat = GetCompo<EntityStat>();
            _health.OnDeadEvent.AddListener(EnemyPoolPush);
            GameManager.Instance.OnStatUpEvent += HandleStatUp;
            SingletonPoolManager.Instance.OnAllPushEvent += HandleAllPush;
        }

        private void HandleAllPush()
        {
            SingletonPoolManager.Instance.Push(PoolEnumType.Enemy, this);
        }

        protected virtual void HandleStatUp()
        {
            _stat.IncreaseBaseValue(_stat.HpStat, GameManager.Instance.StatUpValue);
        }

        protected void OnDestroy()
        {
            GameManager.Instance.OnStatUpEvent -= HandleStatUp;
            SingletonPoolManager.Instance.OnAllPushEvent -= HandleAllPush;
        }

        public void EnemyPoolPush()
        {
            SingletonPoolManager.Instance.Push(PoolEnumType.Enemy, this);
        }
    }
}
