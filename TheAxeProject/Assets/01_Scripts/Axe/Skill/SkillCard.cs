using UnityEngine;

public class SkillCard : MonoBehaviour
{
    private SkillDataSO skillData;

    public void Init(SkillDataSO skillData)
    {

    }

    public SkillDataSO GetData()
    {
        return skillData;
    }
}
