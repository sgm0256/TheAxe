using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace MK.BT
{
    public class ChargeAttack : Conditional
    {
        public PoolTypeSO attackLoadPool;
        public SharedTransform target;
        public SharedEnemy enemy;
        
        private Vector2 _attackDirection = Vector2.zero;
        private bool _isCanAttack = false;
        private AttackLoad _attackLoad;

        public override void OnStart()
        {
            _attackDirection = target.Value.position - transform.position;
            float angle = Mathf.Atan2(_attackDirection.y, _attackDirection.x) * Mathf.Rad2Deg;
            
            _attackLoad = enemy.Value.EnemyPoolManager.Pop(attackLoadPool) as AttackLoad;
            _attackLoad.transform.position = transform.position;
            _attackLoad.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
            _attackLoad.ChargeAttackLoad();
            
            _attackLoad.ReadyToAttackEvent += HandleReadyToAttack;
        }

        public override TaskStatus OnUpdate()
        {
            if (_isCanAttack)
                return TaskStatus.Success;

            return TaskStatus.Running;
        }

        private void HandleReadyToAttack()
        {
            Debug.Log("AttackSuccess");
            _isCanAttack = true;
        }

        public override void OnEnd()
        {
            _attackLoad.ReadyToAttackEvent -= HandleReadyToAttack;
            _isCanAttack = false;
            base.OnEnd();
        }
    }
}
