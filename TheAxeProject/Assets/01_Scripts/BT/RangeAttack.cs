using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Core.Entities;
using ObjectPooling;
using UnityEngine;

namespace MK.BT
{
    public class RangeAttack : Action
    {
        public PoolTypeSO attackLoadType;
        public SharedTransform target;
        public SharedEnemy enemy;
        public float duration = 1f;
        public float size = 3f;

        private EntityMover _mover;
        private Vector2 _attackDirection = Vector2.zero;
        private bool _isCanAttack = false;
        private AttackLoad _attackLoad;
        private Transform _attackTrm;

        public override void OnAwake()
        {
            _mover = enemy.Value.GetCompo<EntityMover>();
        }

        public override void OnStart()
        {
            _mover.StopImmediately();
            
            _attackDirection = target.Value.position - transform.position;
            float angle = Mathf.Atan2(_attackDirection.y, _attackDirection.x) * Mathf.Rad2Deg;
            Quaternion angleAxis = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            transform.rotation = angleAxis;
            
            _attackLoad = SingletonPoolManager.Instance.GetPoolManager(PoolEnumType.AttackLoad).Pop(attackLoadType) as AttackLoad;
            _attackTrm = _attackLoad.AttackPoint;
            
            _attackLoad.transform.position = target.Value.position;
            _attackLoad.transform.localScale = new Vector3(size, size, 1);
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
            _isCanAttack = true;
            // TODO : 여기서 원거리 공격 그리고 이거 다 바꿔야 함
        }

        public override void OnEnd()
        {
            _attackLoad.ReadyToAttackEvent -= HandleReadyToAttack;
            _isCanAttack = false;
            base.OnEnd();
        }
    }
}
