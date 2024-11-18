using DG.Tweening;
using UnityEngine;

public class SkillSelecter : MonoBehaviour
{
    private UpgradeManager manager;
    private SkillCard[] skillCards;

    private int selectCardIdx = 0;
    private bool isGetCards = false;

    private void Awake()
    {
        manager = GetComponent<UpgradeManager>();
    }

    private void Update()
    {
        if (!manager.IsSelect) return;
        if (!isGetCards)
        {
            skillCards = GetComponentsInChildren<SkillCard>();
            isGetCards = true;
        }

        int input = 0;
        if (Input.GetKeyDown(KeyCode.A))
            input = -1;
        else if (Input.GetKeyDown(KeyCode.D))
            input = 1;

        if (input != 0)
            SkillSelectPocus(input);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SkillType type = skillCards[selectCardIdx].GetData().skillType;
            manager.ApplySkill(type);
            selectCardIdx = 0;

            isGetCards = false;
        }
    }

    private void SkillSelectPocus(int input)
    {
        int prev = selectCardIdx;
        selectCardIdx = Mathf.Clamp(selectCardIdx + input, 0, 2);

        RectTransform rect = skillCards[prev].GetComponent<RectTransform>();
        rect.DOAnchorPosY(-50, 0.1f).SetUpdate(true);

        rect = skillCards[selectCardIdx].GetComponent<RectTransform>();
        rect.DOAnchorPosY(0, 0.1f).SetUpdate(true);
    }
}
