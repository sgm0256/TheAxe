using System.Collections.Generic;
using MKDir;

namespace ObjectPooling
{
    public enum PoolEnumType
    {
        AttackLoad, InteractiveObject, Axe, RanageAttack
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

        public PoolManagerSO GetPoolManager(PoolEnumType type)
        {
            return _poolManagers.GetValueOrDefault(type);
        }
    }
}
