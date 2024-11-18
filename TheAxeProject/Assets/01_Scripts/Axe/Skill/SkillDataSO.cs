using ObjectPooling;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skill/SkillData")]
public class SkillDataSO : ScriptableObject
{
    public PoolTypeSO poolType;
    public SkillType skillType;
    public Color color; //���� ã���� �ؽ��ķ� ��ü
}
