using MKDir;
using ObjectPooling;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    Lightning,
    Ice,
    Tornado,
    Bomb,
    Poison
}

public class SkillManager : MonoSingleton<SkillManager>
{
    [SerializeField] private List<PoolTypeSO> poolTypes;

    private Dictionary<string, PoolTypeSO> skillPoolTypeDictionary= new();
    private Dictionary<SkillType, int> skillLevelDictionary = new();

    protected override void Awake()
    {
        base.Awake();

        foreach(PoolTypeSO poolType in poolTypes)
        {
            skillPoolTypeDictionary.Add(poolType.name, poolType);
        }

        foreach (SkillType skillType in System.Enum.GetValues(typeof(SkillType)))
        {
            skillLevelDictionary.Add(skillType, 0);
        }
    }

    public PoolTypeSO GetSkillPoolType(string skillName)
    {
        return skillPoolTypeDictionary.TryGetValue(skillName, out PoolTypeSO poolType) ? poolType : null;
    }

    public int GetSkillLevel(SkillType type)
    {
        return skillLevelDictionary.TryGetValue(type, out int level) ? level : 0;
    }

    public void SetSkillLevel(SkillType type, int level)
    {
        if (skillLevelDictionary.ContainsKey(type))
        {
            skillLevelDictionary[type] = level;
        }
    }
}
