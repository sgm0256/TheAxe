using Core.Entities;
using UnityEngine;

public class Skill : MonoBehaviour, IEntityComponent
{
    public SkillType Type;

    protected Axe axe;
    protected AxeMover mover;

    protected int currentLevel = 1;
    protected bool isAttack = false;
    protected bool isUpgradedAxe = false;

    public virtual void Initialize(Entity entity)
    {
        axe = (Axe)entity;
        mover = axe.GetCompo<AxeMover>();
    }

    public virtual void StartSkill() 
    {
        isAttack = true;
        mover.AttackMove();
    }

    public virtual void UpgradeSkill(int level) { }
}
