using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Core.Entities;
using DG.Tweening;
using ObjectPooling;
using UnityEngine;

namespace MK.BT
{
    public class ChargeAttack : Action
    {
        public PoolTypeSO attackLoadType;
        public SharedTransform target;
        public SharedEnemy enemy;
        public float duration = 1f;

        protected EntityMover _mover;
        protected EntityHealth _health;
        protected Vector2 _attackDirection = Vector2.zero;
        protected bool _isCanAttack = false;
        protected AttackLoad _attackLoad;
        protected Transform _attackTrm;
        protected float _angle;
        private Collider2D _collider;

        public override void OnStart()
        {
            _collider = enemy.Value.GetComponent<Collider2D>();
            _health = enemy.Value.GetCompo<EntityHealth>();
            _mover = enemy.Value.GetCompo<EntityMover>();
            _mover.StopImmediately();
            
            SetAttackDirection();
            CreateAttackLoad();
            
            _attackLoad.ChargeAttackLoad();
            
            _attackLoad.transform.position = transform.position;
            _attackLoad.transform.rotation = Quaternion.Euler(0, 0, _angle - 90f);
            
        }

        protected void SetAttackDirection()
        {
            _attackDirection = target.Value.position - transform.position;
            _angle = Mathf.Atan2(_attackDirection.y, _attackDirection.x) * Mathf.Rad2Deg;
        }

        protected void CreateAttackLoad()
        {
            _attackLoad = SingletonPoolManager.Instance.GetPoolManager(PoolEnumType.AttackLoad).Pop(attackLoadType) as AttackLoad;
            _attackTrm = _attackLoad.AttackPoint;
            _health.OnDeadEvent.AddListener(_attackLoad.HandleEntityDead);
            _collider.isTrigger = true;
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
            transform.DOMove(_attackTrm.position, duration).SetEase(Ease.InQuad).OnComplete(() =>
            {
                _collider.isTrigger = false;
                _isCanAttack = true;
            });
        }

        public override void OnEnd()
        {
            _health.OnDeadEvent.RemoveListener(_attackLoad.HandleEntityDead);
            _attackLoad.ReadyToAttackEvent -= HandleReadyToAttack;
            _isCanAttack = false;
            base.OnEnd();
        }
    }
}
