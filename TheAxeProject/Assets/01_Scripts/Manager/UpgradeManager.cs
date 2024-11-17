using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private SkillCard skillCardPrefab;

    [SerializeField] private List<SkillDataSO> skillDataList = new();

    private List<int> spawnIdxList;

    private CanvasGroup canvasGroup;

    private bool isSelect = false;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        SkillManager.Instance.AddSKill(FindSkillData(SkillType.Normal));
    }

    public void StartSelectSkill()
    {
        CardSpawn();
        SetSpawnCardList();
        OpenPanel();

        isSelect = true;
    }

    private void CardSpawn()
    {
        for (int i = 0; i < 3; ++i)
        {
            Instantiate(skillCardPrefab, transform);
        }
    }

    private void SetSpawnCardList()
    {
        List<int> availableIndices = new List<int>();

        int listCnt = SkillManager.Instance.SkillList.Count;
        for (int i = 0; i < listCnt; i++)
            availableIndices.Add(i);

        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, listCnt - i);
            spawnIdxList.Add(availableIndices[randomIndex]);

            availableIndices.RemoveAt(randomIndex);
        }
    }

    private void OpenPanel()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    private SkillDataSO FindSkillData(SkillType skillType)
    {
        foreach (SkillDataSO skillData in skillDataList)
            if (skillData.skillType == skillType)
                return skillData;
        return null;
    }
}
