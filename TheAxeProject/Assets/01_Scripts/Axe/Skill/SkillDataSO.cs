using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "SO/Skill/SkillData")]
public class SkillDataSO : ScriptableObject
{
    public SkillType skillType;
    [TextArea]
    public string desc;
    [TextArea]
    public string desc2;
    public float damage;
    public float range;
    public float special;
    public Color color; //���� ã���� �ؽ��ķ� ��ü
}
