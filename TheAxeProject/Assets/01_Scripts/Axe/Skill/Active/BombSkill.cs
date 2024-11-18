using MK.Enemy;
using UnityEngine;

public class BombSkill : Skill
{
    public override void Awake()
    {
        base.Awake();

        Type = SkillType.Bomb;
    }

    protected override void Impact()
    {
        base.Impact();

        //���� ��ų
    }

    public override void UpgradeSkill()
    {
        base.UpgradeSkill();

        switch (level)
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
        if (!axe.isAttack)
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
