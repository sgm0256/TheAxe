using UnityEngine;

public class SkillCard : MonoBehaviour
{
    private SkillDataSO skillData;

    public void Init(SkillDataSO data)
    {
        this.skillData = data;
    }

    public SkillDataSO GetData()
    {
        return skillData;
    }
}
