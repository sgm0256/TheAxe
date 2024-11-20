using Core.Entities;
using MK.Enemy;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] protected SkillDataSO skillData;

    public SkillType Type;

    protected Axe axe;
    protected AxeMover mover;

    protected bool isUpgradedAxe = false;
    protected int level = 0;

    public virtual void Awake()
    {
        axe = GetComponentInParent<Axe>();
        mover = axe.GetCompo<AxeMover>();

        axe.OnAxeImpact += Impact;
    }

    protected virtual void Impact()
    {
        axe.EndAttack();
    }

    public virtual void StartSkill()
    {
        mover.AttackMove();
    }

    public virtual void UpgradeSkill() 
    {
        level++;
        SkillManager.Instance.SetSkillLevel(Type, level);
    }

    protected virtual void FlightSkill() { }

    public int GetLevel()
    {
        return level;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!axe.isAttack)
            return;

        if (collision.TryGetComponent(out Enemy enemy))
        {
            if (isUpgradedAxe)
            {
                FlightSkill();
            }
            else
            {
                enemy.GetCompo<EntityHealth>().ApplyDamage(350f, axe);
            }
        }
    }
}
