using Core.Entities;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public SkillType Type;

    protected Axe axe;
    protected AxeMover mover;

    protected bool isUpgradedAxe = false;

    public virtual void StartSkill() 
    {
        mover.AttackMove();
    }

    public virtual void UpgradeSkill(int level) { }
}
