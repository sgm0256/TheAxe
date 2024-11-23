using Core.Entities;
using Core.StatSystem;
using MK.Enemy;
using ObjectPooling;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public SkillDataSO skillData;
    public StatSO damageStat;

    protected Axe axe;
    protected AxeMover mover;

    public virtual void Awake()
    {
        axe = GetComponentInParent<Axe>();
        mover = axe.GetCompo<AxeMover>();

        axe.OnAxeImpact += Impact;
    }

    protected virtual void Impact()
    {
        SingletonPoolManager.Instance.GetPoolManager(PoolEnumType.Axe).Push(axe);
    }

    public virtual void StartSkill()
    {
        mover.AttackMove();
    }

    protected virtual void FlightSkill(GameObject obj) { }

    public int GetLevel()
    {
        return skillData.level;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!axe.isAttack)
            return;

        if (collision.TryGetComponent(out Enemy enemy))
        {
            if (skillData.isFlight)
            {
                FlightSkill(collision.gameObject);
            }
            else
            {
                float damage = skillData.damage;
                enemy.GetCompo<EntityHealth>().ApplyDamage(damage, axe);
            }
        }
    }
}
