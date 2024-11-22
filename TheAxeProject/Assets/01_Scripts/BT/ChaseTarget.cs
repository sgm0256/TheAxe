using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Core.Entities;
using UnityEngine;

namespace MK.BT
{
    public class ChaseTarget : Action
    {
        public SharedTransform target;
        public SharedEnemy enemy;

        private EntityMover _mover;
        
        public float rotateSpeed = 3f;

        public override void OnStart()
        {
            _mover = enemy.Value.GetCompo<EntityMover>();
        }

        public override TaskStatus OnUpdate()
        {
            if (Vector2.Distance(transform.position, target.Value.position) < Mathf.Epsilon)
            {
                return TaskStatus.Success;
            }
            
            Vector2 direction = target.Value.position - transform.position;

            _mover.SetMovement(direction);
            
            //transform.rotation = rotation;

            return TaskStatus.Running;
        }
    }
}
