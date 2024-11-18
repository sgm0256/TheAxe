using UnityEngine;

public class SkillCard : MonoBehaviour
{
    private SkillDataSO skillData;

    public void Init(SkillDataSO data, UpgradeManager manager)
    {
        skillData = data;
    }

    public SkillDataSO GetData()
    {
        return skillData;
    }
}
