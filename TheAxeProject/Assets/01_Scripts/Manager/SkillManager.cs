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

    protected override void Awake()
    {
        base.Awake();

        foreach (GameObject axePrefab in axeList)
        {
            Axe axe = Instantiate(axePrefab, Vector3.zero, Quaternion.identity, transform).GetComponent<Axe>();
            SkillType type = axe.GetSkill().skillData.skillType;
            skillOfAxeDictionary.Add(type, axe);
        }
    }

    public Axe GetAxeOfSkillType(SkillType type)
    {
        return skillOfAxeDictionary.TryGetValue(type, out Axe axe) ? axe : null;
    }

    public void AddSKill(SkillDataSO skillData)
    {
        SkillList.Add(skillData);
    }
}
