using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using MK.Enemy;
using UnityEngine;

public class FindTarget : Conditional
{
    public SharedEnemy enemy;
    public SharedTransform target;
    public SharedLayerMask whatIsTarget;

    public override void OnAwake()
    {
        // TODO : Enemy 변수 세팅 방식 변경
        enemy.Value = transform.GetComponent<Enemy>();
        
        var result = Physics2D.OverlapCircle(transform.position, float.MaxValue, whatIsTarget.Value);

        if (result != null)
        {
            target.Value = result.transform;
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (target.Value != null)
            return TaskStatus.Success;
        
        return TaskStatus.Failure;
    }
}
