using Core.Entities;
using ObjectPooling;
using UnityEngine;

namespace Core.InteractiveObjects
{
    public class Item : InteractiveObject, IPoolable
    { 
        [field: SerializeField] public PoolTypeSO PoolType { get; private set; }
        public GameObject GameObject { get => gameObject; }

        private Pool _pool;
        
        public void SetUpPool(Pool pool)
        {
            _pool = pool;
        }

        public void ResetItem()
        {
            
        }

        public override void PickUpItem(Entity entity)
        {
            _entity = entity;
        }
    }
}
