    using BehaviorDesigner.Runtime;
    using BehaviorDesigner.Runtime.Tasks;
    using Core.Entities;
    using Core.StatSystem;
    using UnityEngine;

    namespace MK.BT
{
    public class MeleeAttack : Action
    {
        public SharedEnemy enemy;
        public SharedLayerMask targetLayer;
        public SharedFloat radius;
        public StatSO damageStat;
        public SharedFloat distacne;

        private EntityStat _stat;
        private float _currentDamage;

        // TODO : Add Attack

        public override void OnStart()
        {
            base.OnStart();

            _stat = enemy.Value.GetCompo<EntityStat>();
            _currentDamage = _stat.GetStat(damageStat).Value;
            
            var result = Physics2D.CircleCast(transform.position, radius.Value, transform.forward, distacne.Value, targetLayer.Value);

            if (result)
            {
                if (result.transform.TryGetComponent(out Player player))
                {
                    EntityHealth health = player.GetCompo<EntityHealth>();
                    health.ApplyDamage(_currentDamage, enemy.Value);
                    Debug.Log("공격 함");
                }
            }
        }

        public override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            Gizmos.color = Color.cyan;
            //Gizmos.DrawWireSphere();
        }
    }
}

