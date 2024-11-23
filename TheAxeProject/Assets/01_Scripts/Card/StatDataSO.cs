using Core.StatSystem;
using UnityEngine;

public enum StatType
{
    Speed,
    Size,
    Damage,
    AxeCount
}

[CreateAssetMenu(menuName = "SO/Data/StatData")]
public class StatDataSO : DataSO
{
    public StatType statType;
    public StatSO stat;
    public Color color; //에셋 찾으면 텍스쳐로 교체

    [TextArea]
    public string desc;

    public float increase;

    public int level;

    public void Upgrade()
    {
        level++;
        GameManager.Instance.Player.GetCompo<EntityStat>().GetStat(stat).BaseValue += increase;
    }

    public override void ResetInfo()
    {
        level = 0;
    }
}
