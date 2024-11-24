using System.Collections.Generic;
using MKDir;

namespace ObjectPooling
{
    public enum PoolEnumType
    {
        AttackLoad, InteractiveObject, Axe, RanageAttack, Enemy, Environment
    }
    
    public class SingletonPoolManager : MonoSingleton<SingletonPoolManager>
    {
        public List<PoolManagerSO> poolManagerList = new();

        private Dictionary<PoolEnumType, PoolManagerSO> _poolManagers = new ();

        protected override void Awake()
        {
            base.Awake();
            foreach (PoolManagerSO poolManager in poolManagerList)
            {
                poolManager.InitializePool(this.transform);
                _poolManagers.Add(poolManager.PoolEnumType, poolManager);
            }
        }

        public IPoolable Pop(PoolEnumType poolManagerType, PoolTypeSO type)
        {
            return GetPoolManager(poolManagerType).Pop(type);
        }
        
        public void Push(PoolEnumType poolManagerType, IPoolable item)
        {
            GetPoolManager(poolManagerType).Push(item);
        }

        public PoolManagerSO GetPoolManager(PoolEnumType type)
        {
            return _poolManagers.GetValueOrDefault(type);
        }
    }
}
