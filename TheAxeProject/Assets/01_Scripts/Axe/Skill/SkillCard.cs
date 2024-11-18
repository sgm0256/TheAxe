using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillCard : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI descText;

    private SkillDataSO skillData;

    public void Init(SkillDataSO data)
    {
        skillData = data;

        iconImage.color = skillData.color;
        levelText.text = "Lv." + SkillManager.Instance.GetSkillLevel(skillData.skillType);
        descText.text = string.Format(skillData.desc, skillData.stat1, skillData.stat2);
    }

    public SkillDataSO GetData()
    {
        return skillData;
    }
}
