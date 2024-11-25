using Core.Entities;
using Core.StatSystem;
using MK.Enemy;
using ObjectPooling;
using UnityEngine;

public class BombSkill : Skill
{
    [SerializeField] private ParticleSystem flameParticle;

    protected override void Impact(Vector3 lastDir)
    {
        if (stat == null)
            stat = playerSO.Player.GetCompo<EntityStat>();

        Transform effectTrm = SingletonPoolManager.Instance.GetPoolManager(PoolEnumType.Effect).Pop(effectPoolType).GameObject.transform;
        effectTrm.position = transform.position;
        effectTrm.up = lastDir;
        effectTrm.localScale = Vector3.one * (1.2f + (skillData.level * 0.2f));

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
        if(flameParticle.isStopped)
            flameParticle.Play();


    }
}
