using ObjectPooling;
using UnityEngine;
using UnityEngine.UIElements;

public enum SkillType
{
    Normal,
    Lightning,
    Ice,
    Tornado,
    Bomb,
    Poison
}

[CreateAssetMenu(menuName = "SO/Data/SkillData")]
public class SkillDataSO : DataSO
{
    public PoolTypeSO poolType;
    public SkillType skillType;
    public Sprite sprite;

    [TextArea]
    public string desc;
    [TextArea]
    public string desc2;

    [HideInInspector]
    public bool isFlight;

    [HideInInspector]
    public float damage;
    public float baseDamage;
    public float damageIncrease;
    [HideInInspector]
    public float range;
    public float baseRange;
    public float rangeIncrease;
    [HideInInspector]
    public float special;
    public float baseSpecial;
    public float specialIncrease;

    public int level;

    public void Upgrade()
    {
        level++;

        switch (level)
        {
            case 1:
                {
                    damage = baseDamage;
                    range = baseRange;
                    special = baseSpecial;
                }
                break;
            case 2:
                {
                    damage += damageIncrease;
                    range += rangeIncrease;
                }
                break;
            case 3:
                {
                    damage += damageIncrease;
                    range += rangeIncrease;
                    isFlight = true;
                }
                break;
            case 4:
                {
                    damage += damageIncrease;
                    range += rangeIncrease;
                    special += specialIncrease;
                }
                break;
            case 5:
                {
                    //ÁøÈ­
                }
                break;
        }
    }

    public override void ResetInfo()
    {
        level = 0;
        damage = baseDamage;
        range = baseRange;
        special = baseSpecial;
    }
}
