using Core.StatSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatCard : Card
{
    [SerializeField] private PlayerManagerSO playerSO;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI descText;

    private StatDataSO statData;

    public override void Init(DataSO data)
    {
        statData = (StatDataSO)data;

        iconImage.color = statData.color;

        levelText.text = "Lv." + (statData.level + 1);

        descText.text = string.Format(statData.desc, playerSO.Player.GetCompo<EntityStat>()
            .GetStat(statData.stat).Value + statData.increase);
    }

    public StatDataSO GetData()
    {
        return statData;
    }
}
