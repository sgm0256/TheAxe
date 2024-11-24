using Core.Entities;
using Core.StatSystem;
using MK.Enemy;
using ObjectPooling;
using System.Linq;
using UnityEngine;

public class NormalSkill : Skill
{
    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private float radius = 0.5f;
    [SerializeField] private float knockbackpower = 10f;

    EntityStat stat;

    protected override void Impact(Vector3 lastDir)
    {
        if (stat == null)
            stat = GameManager.Instance.Player.GetCompo<EntityStat>();

        Transform effectTrm = SingletonPoolManager.Instance.GetPoolManager(PoolEnumType.Effect).Pop(effectPoolType).GameObject.transform;
        effectTrm.position = transform.position;
        effectTrm.up = lastDir;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, skillData.range / 2, whatIsEnemy);

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out Enemy enemy))
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                float damage = skillData.damage + stat.GetStat(damageStat).Value * (distance < radius ? 1f : 0.5f);
                enemy.GetCompo<EntityHealth>().ApplyDamage(damage, axe);
            }
        }

        base.Impact(lastDir);
    }

    protected override void FlightSkill(GameObject obj)
    {
        Enemy enemy = obj.GetComponent<Enemy>();
        enemy.GetCompo<EntityHealth>().ApplyDamage(damage + skillData.damage, axe);
    }
}
