using UnityEngine;

namespace MK.Enemy
{
    public class RushEnemy : Enemy
    {
        [field: SerializeField] public AttackLoadGenerator AttackLoadGenerator { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            
            if(AttackLoadGenerator != null)
                AttackLoadGenerator.PoolManager.LoadSuccessEvent += HandleSuccessPool;
        }

        private void OnDestroy()
        {
            if(AttackLoadGenerator != null) 
                AttackLoadGenerator.PoolManager.LoadSuccessEvent -= HandleSuccessPool;
        }
        
        private void HandleSuccessPool()
        {
            Debug.Log("로딩 완료");
        }
    }
}

