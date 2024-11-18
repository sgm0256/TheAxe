using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Core.Entities;
using DG.Tweening;
using ObjectPooling;
using UnityEngine;

namespace MK.BT
{
    public abstract class ChargeAttack : Action
    {
        public PoolTypeSO attackLoadType;
        public SharedTransform target;
        public SharedEnemy enemy;
        public float duration = 1f;

        protected EntityMover _mover;
        protected Vector2 _attackDirection = Vector2.zero;
        protected bool _isCanAttack = false;
        protected AttackLoad _attackLoad;
        protected Transform _attackTrm;

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
        
        protected virtual void HandleReadyToAttack()
        {
            transform.DOMove(_attackTrm.position, duration).SetEase(Ease.InQuad).OnComplete(() => _isCanAttack = true );
        }

        public override void OnEnd()
        {
            _attackLoad.ReadyToAttackEvent -= HandleReadyToAttack;
            _isCanAttack = false;
            base.OnEnd();
        }
    }
}
