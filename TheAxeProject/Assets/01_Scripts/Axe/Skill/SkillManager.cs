using MKDir;
using ObjectPooling;
using System;
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
    [SerializeField] private List<GameObject> axeList;

    private Dictionary<SkillType, Axe> skillOfAxeDictionary = new();
    private Dictionary<SkillType, int> skillLevelDictionary = new();

    protected override void Awake()
    {
        base.Awake();

        foreach(GameObject axePrefab in axeList)
        {
            Axe axe = Instantiate(axePrefab, Vector3.zero, Quaternion.identity).GetComponent<Axe>();
            SkillType type = axe.GetCompo<Skill>().Type;
            skillOfAxeDictionary.Add(type, axe);
        }

        foreach (SkillType skillType in System.Enum.GetValues(typeof(SkillType)))
        {
            skillLevelDictionary.Add(skillType, 0);
        }
    }

    public Axe GetAxeOfSkillType(SkillType type)
    {
        return skillOfAxeDictionary.TryGetValue(type, out Axe axe) ? axe : null;
    }

    public int GetSkillLevel(SkillType type)
    {
        return skillLevelDictionary.TryGetValue(type, out int level) ? level : 0;
    }

    public void SetSkillLevel(SkillType type, int level)
    {
        if (skillLevelDictionary.ContainsKey(type))
            skillLevelDictionary[type] = level;
    }
}
