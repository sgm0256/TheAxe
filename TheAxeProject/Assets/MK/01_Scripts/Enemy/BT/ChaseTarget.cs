using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Unity.Mathematics;
using UnityEngine;

namespace MK.BT
{
    public class ChaseTarget : Action
    {
        public SharedTransform target;
        public SharedEnemy enemy;

        public SharedFloat speed;
        public float rotateSpeed = 3f;

        public override TaskStatus OnUpdate()
        {
            if (Vector2.Distance(transform.position, target.Value.position) < Mathf.Epsilon)
            {
                Debug.Log("추적 성공");
                return TaskStatus.Success;
            }

            transform.position =
                Vector2.MoveTowards(transform.position, target.Value.position, speed.Value * Time.deltaTime);

            Vector2 direction = target.Value.position - transform.position;
            
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion angleAxis = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, rotateSpeed * Time.deltaTime);
            
            transform.rotation = rotation;

            return TaskStatus.Running;
        }
    }
}
