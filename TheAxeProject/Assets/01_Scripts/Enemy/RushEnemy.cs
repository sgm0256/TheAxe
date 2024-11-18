using ObjectPooling;
using UnityEngine;

namespace MK.Enemy
{
    public class RushEnemy : Enemy, IPoolable
    {
        [field: SerializeField] public PoolTypeSO PoolType { get; set; }
        public GameObject GameObject { get => gameObject; }

        private Pool _myPool;
        
        public void SetUpPool(Pool pool)
        {
            _myPool = pool;
        }

        public void ResetItem()
        {
            
        }
    }
}