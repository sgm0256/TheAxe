using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private List<SkillDataSO> skillDataList = new();

    private void Start()
    {
        SkillManager.Instance.AddSKill(FindSkillData(SkillType.Normal));
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SkillManager.Instance.AddSKill(FindSkillData(SkillType.Lightning));
        }
    }

    private SkillDataSO FindSkillData(SkillType skillType)
    {
        foreach (SkillDataSO skillData in skillDataList)
            if (skillData.skillType == SkillType.Normal)
                return skillData;
        return null;
    }
}
