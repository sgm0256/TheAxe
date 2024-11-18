using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "SO/Skill/SkillData")]
public class SkillDataSO : ScriptableObject
{
    public SkillType skillType;
    [TextArea]
    public string desc;
    public float stat1;
    public float stat2;
    public Color color; //에셋 찾으면 텍스쳐로 교체
}
