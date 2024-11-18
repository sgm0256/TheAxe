using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Core.StatSystem;
using UnityEngine;

namespace MK.BT
{
    public class RangeDected : Conditional
    {
        public LayerMask whatIsTarget;
        public SharedEnemy enemy;
        public SharedTransform target;
        public StatSO _rangeStat;
        
        private Collider2D[] _colliders;
        private EntityStat _stat;
        private float _radiusRange;
        
        public override void OnStart()
        {
            _stat = enemy.Value.GetCompo<EntityStat>();
            _radiusRange = _stat.GetStat(_rangeStat).Value;
            _stat.GetStat(_rangeStat).OnValueChange += HandleValueChange;
        }

        private void HandleValueChange(StatSO stat, float current, float previous)
        {
            _radiusRange = current;
        }

        public override TaskStatus OnUpdate()
        {
            _colliders = Physics2D.OverlapCircleAll(transform.position, _radiusRange, whatIsTarget);
            if (_colliders.Length > 0)
            {
                target.Value = _colliders[0].transform;
                return TaskStatus.Success;
            }
            
            return TaskStatus.Failure;
        }

        public override void OnEnd()
        {
            _stat.GetStat(_rangeStat).OnValueChange -= HandleValueChange;
        }

        public override void OnDrawGizmos()
        {
            base.OnDrawGizmos();

            Gizmos.color = Color.red;
            if (transform != null)
            {
                Gizmos.DrawWireSphere(transform.position, _radiusRange);
            }
        }
    }
}
