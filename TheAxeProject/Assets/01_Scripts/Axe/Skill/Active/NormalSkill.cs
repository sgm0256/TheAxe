using MK.Enemy;
using UnityEngine;

public class NormalSkill : Skill
{
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

        switch (skillData.level)
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
