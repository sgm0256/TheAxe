using System;
using System.Collections.Generic;
using Core.Entities;
using UnityEngine;
using Random = UnityEngine.Random;

public class UpgradeManager : MonoBehaviour
{
    public event Action OnSelectSkillEvent;
    public event Action OnStartSelectSkillEvent;
    
    [SerializeField] private StatCard statCardPrefab;
    [SerializeField] private SkillCard skillCardPrefab;
    [SerializeField] private List<DataSO> DataList;

    private List<int> spawnIdxList = new();
    private Card[] Cards;

    private CanvasGroup canvasGroup;

    [HideInInspector]
    public bool IsSelect = false;

    private int cardCnt => DataList.Count < 3 ? DataList.Count : 3;

    private void Awake()
    {
        canvasGroup = GetComponentInParent<CanvasGroup>();
    }

    private void Start()
    {
        GameManager.Instance.Player.GetCompo<EntityLevel>().LevelUpEvent += (level) => StartSelectSkill();

        SkillManager.Instance.AddSKill(FindSkillData(SkillType.Normal));

        foreach (DataSO data in DataList)
            data.ResetInfo();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            StartSelectSkill();
    }

    public void StartSelectSkill()
    {
        OnStartSelectSkillEvent?.Invoke();
        Time.timeScale = 0;

        SetSpawnCardList();
        CardSpawn();
        InitCard();
        OpenPanel();

        IsSelect = true;
    }

    private void SetSpawnCardList()
    {
        List<int> availableIndices = new List<int>();

        int listCnt = DataList.Count;
        for (int i = 0; i < listCnt; i++)
            availableIndices.Add(i);

        for (int i = 0; i < cardCnt; i++)
        {
            int randomIndex = Random.Range(0, listCnt - i);
            spawnIdxList.Add(availableIndices[randomIndex]);

            availableIndices.RemoveAt(randomIndex);
        }
    }

    private void CardSpawn()
    {
        for (int i = 0; i < cardCnt; ++i)
            if (DataList[spawnIdxList[i]].GetType() == typeof(SkillDataSO))
                Instantiate(skillCardPrefab, transform);
            else if (DataList[spawnIdxList[i]].GetType() == typeof(StatDataSO))
                Instantiate(statCardPrefab, transform);

        Cards = GetComponentsInChildren<Card>();
    }

    private void InitCard()
    {
        for (int i = 0; i < cardCnt; i++)
            Cards[i].Init(DataList[spawnIdxList[i]]);
    }

    private void OpenPanel()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void ApplySkill(SkillType type)
    {
        SkillDataSO data = FindSkillData(type);
        data.Upgrade();

        int level = data.level;
        if (level == 1)
            SkillManager.Instance.AddSKill(FindSkillData(data.skillType));
        if (level == 5)
            DataList.Remove(FindSkillData(type));

        CloseSelectSkill();
    }

    public void UpgradeStat(StatType type)
    {
        StatDataSO data = FindStatData(type);
        data.Upgrade();

        int level = data.level;
        if (level == 5)
            DataList.Remove(FindStatData(type));

        CloseSelectSkill();
    }

    private void CloseSelectSkill()
    {
        spawnIdxList.Clear();
        IsSelect = false;

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        foreach(Card data in Cards)
            Destroy(data.gameObject);

        Time.timeScale = 1f;
        OnSelectSkillEvent?.Invoke();
    }

    private SkillDataSO FindSkillData(SkillType skillType)
    {
        foreach (DataSO data in DataList)
            if (data.GetType() == typeof(SkillDataSO))
            {
                SkillDataSO skillData = (SkillDataSO)data;
                if (skillData.skillType == skillType)
                    return skillData;
            }
        return null;
    }

    private StatDataSO FindStatData(StatType statType)
    {
        foreach (DataSO data in DataList)
            if (data.GetType() == typeof(StatDataSO))
            {
                StatDataSO statData = (StatDataSO)data;
                if (statData.statType == statType)
                    return statData;
            }
        return null;
    }
}
