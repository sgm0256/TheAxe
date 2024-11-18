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

        //강력 스킬
    }

    public override void UpgradeSkill()
    {
        base.UpgradeSkill();

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
