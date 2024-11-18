using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private SkillCard skillCardPrefab;
    [SerializeField] private List<SkillDataSO> skillDataList;

    private List<int> spawnIdxList = new();
    private SkillCard[] skillCards;

    private CanvasGroup canvasGroup;

    [HideInInspector]
    public bool IsSelect = false;

    private void Awake()
    {
        canvasGroup = GetComponentInParent<CanvasGroup>();
    }

    private void Start()
    {
        SkillManager.Instance.AddSKill(FindSkillData(SkillType.Normal));
        CardSpawn();
    }

    private void CardSpawn()
    {
        for (int i = 0; i < 3; ++i)
            Instantiate(skillCardPrefab, transform);

        skillCards = GetComponentsInChildren<SkillCard>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            StartSelectSkill();
    }

    public void StartSelectSkill()
    {
        SetSpawnCardList();
        OpenPanel();

        IsSelect = true;
    }

    private void SetSpawnCardList()
    {
        List<int> availableIndices = new List<int>();

        int listCnt = skillDataList.Count;
        for (int i = 0; i < listCnt; i++)
            availableIndices.Add(i);

        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, listCnt - i);
            spawnIdxList.Add(availableIndices[randomIndex]);

            availableIndices.RemoveAt(randomIndex);
        }

        InitCard();
    }

    private void InitCard()
    {
        for (int i = 0; i < 3; i++)
            skillCards[i].Init(skillDataList[spawnIdxList[i]], this);
    }

    private void OpenPanel()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    private void CloseSelectSkill()
    {
        spawnIdxList.Clear();
        IsSelect = false;

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void ApplySkill(SkillType type)
    {
        Axe axe = SkillManager.Instance.GetAxeOfSkillType(type);
        axe.GetSkill().UpgradeSkill();

        int level = axe.GetSkill().GetLevel();
        if (level == 1)
            SkillManager.Instance.AddSKill(FindSkillData(axe.GetSkill().Type));
        if (level == 5)
            skillDataList.Remove(FindSkillData(type));

        CloseSelectSkill();
    }

    private SkillDataSO FindSkillData(SkillType skillType)
    {
        foreach (SkillDataSO skillData in skillDataList)
            if (skillData.skillType == skillType)
                return skillData;
        return null;
    }
}
