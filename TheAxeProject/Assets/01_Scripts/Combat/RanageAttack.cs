using ObjectPooling;
using UnityEngine;

namespace MK.Enemy
{
    public class RanageAttack : MonoBehaviour, IPoolable
    {
        [field: SerializeField] public PoolTypeSO PoolType { get; set; }
        [SerializeField] private PoolEnumType type;
        
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