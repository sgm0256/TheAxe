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
    public Color color; //에셋 찾으면 텍스쳐로 교체
}
