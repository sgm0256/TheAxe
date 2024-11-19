using MK.Enemy;
using UnityEngine;

public class NormalSkill : Skill
{
    public override void Awake()
    {
        base.Awake();

        Type = SkillType.Normal;
    }

    protected override void Impact()
    {
        base.Impact();

        //���� ��ų
    }

    protected override void FlightSkill()
    {
        
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
}
