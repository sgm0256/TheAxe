using MK.Enemy;
using UnityEngine;

public class NormalSkill : Skill
{
    protected override void Impact()
    {
        base.Impact();

        //강력 스킬
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
}
