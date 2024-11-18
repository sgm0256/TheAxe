using ObjectPooling;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skill/SkillData")]
public class SkillDataSO : ScriptableObject
{
    public PoolTypeSO poolType;
    public SkillType skillType;
    public Color color; //에셋 찾으면 텍스쳐로 교체
}
