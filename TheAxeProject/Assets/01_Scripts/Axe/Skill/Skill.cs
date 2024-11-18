using UnityEngine;

public class Skill : MonoBehaviour
{
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

    public int GetLevel()
    {
        return level;
    }
}
