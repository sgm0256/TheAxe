using MKDir;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    Normal,
    Lightning,
    Ice,
    Tornado,
    Bomb,
    Poison
}

public class SkillManager : MonoSingleton<SkillManager>
{
    [SerializeField] private List<GameObject> axeList = new();

    public List<SkillDataSO> SkillList = new();

    private Dictionary<SkillType, Axe> skillOfAxeDictionary = new();
    private Dictionary<SkillType, int> skillLevelDictionary = new();

    protected override void Awake()
    {
        base.Awake();

        foreach (GameObject axePrefab in axeList)
        {
            Axe axe = Instantiate(axePrefab, Vector3.zero, Quaternion.identity, transform).GetComponent<Axe>();
            SkillType type = axe.GetSkill().Type;
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
        Debug.Log($"set: {type},{level}");
        if (skillLevelDictionary.ContainsKey(type))
            skillLevelDictionary[type] = level;
    }

    public void AddSKill(SkillDataSO skillData)
    {
        Debug.Log($"add: {skillData.name}");
        SkillList.Add(skillData);
    }
}
