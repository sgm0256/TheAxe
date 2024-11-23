using MKDir;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoSingleton<SkillManager>
{
    [SerializeField] private List<SkillDataSO> SkillDataList = new();

    public List<SkillDataSO> UseSkillList = new();

    private Dictionary<SkillType, SkillDataSO> skillOfDataDictionary = new();

    protected override void Awake()
    {
        base.Awake();

        foreach (SkillDataSO dataSO in SkillDataList)
        {
            SkillType type = dataSO.skillType;
            skillOfDataDictionary.Add(type, dataSO);
        }
    }

    public SkillDataSO GetDataOfSkillType(SkillType type)
    {
        return skillOfDataDictionary.TryGetValue(type, out SkillDataSO dataSO) ? dataSO : null;
    }

    public void AddSKill(SkillDataSO skillData)
    {
        UseSkillList.Add(skillData);
    }
}