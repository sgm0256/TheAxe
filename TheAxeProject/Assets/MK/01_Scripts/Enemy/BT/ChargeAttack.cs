using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class ChargeAttack : Action
{
    public PoolTypeSO _attackLoadPool;
    public SharedTransform targetTrm;
    public SharedEnemy enemy;
    private Vector2 _attackDirection;

    public override void OnStart()
    {
        _attackDirection = targetTrm.Value.position - transform.position;
        AttackLoad attackload = enemy.Value.PoolManager.Pop(_attackLoadPool) as AttackLoad;
        attackload?.StartCharge();
        attackload.transform.position = transform.position;
        attackload.transform.rotation = Quaternion.Euler(_attackDirection);
    }
}
