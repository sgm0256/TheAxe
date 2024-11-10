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
                    //���� ����
                }
                break;
            case 3:
                {
                    isUpgradedAxe = true;
                }
                break;
            case 4:
                {
                    //���� ���� 2
                }
                break;
            case 5:
                {
                    //��ȭ
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
                //��ų ����
            }
            else
            {
                //�� ����
            }
        }
    }
}
