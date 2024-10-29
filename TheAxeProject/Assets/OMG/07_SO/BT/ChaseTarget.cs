using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace MK.BT
{
    public class ChaseTarget : Action
    {
        public SharedTransform targetTrm;
        public SharedEnemy enemy;

        public SharedFloat speed;

        public override TaskStatus OnUpdate()
        {
            if (Vector2.Distance(transform.position, targetTrm.Value.position) < Mathf.Epsilon)
            {
                Debug.Log("추적 성공");
                return TaskStatus.Success;
            }

            transform.position =
                Vector2.MoveTowards(transform.position, targetTrm.Value.position, speed.Value * Time.deltaTime);

            return TaskStatus.Running;
        }
    }
}
