using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using MK.Enemy;
using UnityEngine;

public class FindTarget : Conditional
{
    public SharedEnemy enemy;
    public SharedTransform target;
    public SharedLayerMask whatIsTarget;
    public SharedBool isFindTarget;
    public SharedInt colliderCount;

    private Collider2D[] col;
    
    public override void OnStart()
    {
        col = new Collider2D[colliderCount.Value];
        
        if (isFindTarget.Value || target.Value != null)
        {
            isFindTarget.Value = true;
        }
        
        enemy.Value = transform.GetComponent<Enemy>();
        
        col = Physics2D.OverlapCircleAll(transform.position, float.MaxValue, whatIsTarget.Value);

        if (col.Length > 0)
        {
            target.Value = col[0].transform;
            isFindTarget.Value = true;
        }
        isFindTarget.Value = false;
    }

    public override TaskStatus OnUpdate()
    {
        if (target.Value != null) 
            return TaskStatus.Success;
        
        isFindTarget.Value = false;
        return TaskStatus.Failure;
    }
}
