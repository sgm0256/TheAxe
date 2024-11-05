using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class RangeDected : Conditional
{
    public LayerMask _whatIsTarget;
    public SharedTransform target;
    public float radiusRange;
    
    private Collider2D[] _colliders;

    public override TaskStatus OnUpdate()
    {
        _colliders = Physics2D.OverlapCircleAll(transform.position, radiusRange, _whatIsTarget);
        if (_colliders.Length > 0)
        {
            target.Value = _colliders[0].transform;
            return TaskStatus.Success;
        }
        
        return TaskStatus.Failure;
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.red;
        if (transform != null)
        {
            Gizmos.DrawWireSphere(transform.position, radiusRange);
        }
    }
}
