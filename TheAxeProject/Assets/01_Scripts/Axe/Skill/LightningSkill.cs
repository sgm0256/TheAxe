using Core.Entities;
using MK.Enemy;
using UnityEngine;

public class LightningSkill : Skill
{
    public override void Initialize(Entity entity)
    {
        base.Initialize(entity);

        Type = SkillType.Lightning;
    }

    public override void UpgradeSkill(int level)
    {
        currentLevel = level;
        SkillManager.Instance.SetSkillLevel(SkillType.Lightning, currentLevel);

        switch (currentLevel)
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
        if (!isAttack)
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
