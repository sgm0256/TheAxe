using Core.Entities;
using MK.Enemy;
using UnityEngine;

public class LightningSkill : Skill, IEntityComponent
{
    public void Initialize(Entity entity)
    {
        axe = (Axe)entity;
        mover = axe.GetCompo<AxeMover>();
        Type = SkillType.Lightning;

        axe.OnAxeImpact += Impact;
    }

    private void Impact()
    {
        //강력 스킬

        axe.EndAttack();
    }

    public override void UpgradeSkill(int level)
    {
        switch (level)
        {
            case 2:
                {
                    //스텟 증가
                }
                break;
            case 3:
                {
                    isUpgradedAxe = true;
                }
                break;
            case 4:
                {
                    //스텟 증가 2
                }
                break;
            case 5:
                {
                    //진화
                }
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!axe.isAttack)
            return;

        if (collision.TryGetComponent(out Enemy enemy))
        {
            if (isUpgradedAxe)
            {
                //스킬 피해
            }
            else
            {
                //걍 피해
            }
        }
    }
}
