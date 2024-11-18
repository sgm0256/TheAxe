using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillCard : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI descText;
    [SerializeField] private TextMeshProUGUI desc2Text;

    private SkillDataSO skillData;

    public void Init(SkillDataSO data)
    {
        skillData = data;

        iconImage.color = skillData.color;

        int level = SkillManager.Instance.GetSkillLevel(skillData.skillType);
        levelText.text = "Lv." + (level + 1);

        descText.text = string.Format(skillData.desc, skillData.damage, skillData.range);
        if (level >= 2)
            desc2Text.text = string.Format(skillData.desc2, skillData.special);
        else
            desc2Text.text = " ";
    }

    public SkillDataSO GetData()
    {
        return skillData;
    }
}
