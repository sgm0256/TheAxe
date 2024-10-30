using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace MK.BT
{
    public class ChargeAttack : Action
    {
        public PoolTypeSO _attackLoadPool;
        public SharedTransform target;
        public SharedEnemy enemy;
        private Vector2 _attackDirection;

        public override void OnStart()
        {
            _attackDirection = target.Value.position - transform.position;
        }
    }
}
