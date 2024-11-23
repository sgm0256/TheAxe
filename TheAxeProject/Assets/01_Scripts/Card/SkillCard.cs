using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillCard : Card
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI descText;
    [SerializeField] private TextMeshProUGUI desc2Text;

    private SkillDataSO skillData;

    public override void Init(DataSO data)
    {
        skillData = (SkillDataSO)data;

        iconImage.color = skillData.color;

        int level = skillData.level;
        levelText.text = "Lv." + (level + 1);

        if (level == 0)
            descText.text = string.Format(skillData.desc, skillData.baseDamage, skillData.baseRange);
        else
            descText.text = string.Format(skillData.desc, skillData.damage + skillData.damageIncrease, skillData.range + skillData.rangeIncrease);

        if(level == 2)
            desc2Text.text = string.Format(skillData.desc2, skillData.special);
        else if (level > 2)
            desc2Text.text = string.Format(skillData.desc2, skillData.special + skillData.specialIncrease);
        else
            desc2Text.text = " ";
    }

    public SkillDataSO GetData()
    {
        return skillData;
    }
}
