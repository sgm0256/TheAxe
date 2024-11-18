using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Input = UnityEngine.Input;

public class SkillSelecter : MonoBehaviour
{
    private GraphicRaycaster raycaster;
    private EventSystem eventSystem;
    private RectTransform curPocusCard;

    private UpgradeManager manager;

    private void Awake()
    {
        raycaster = transform.root.GetComponent<GraphicRaycaster>();
        eventSystem = EventSystem.current;

        manager = GetComponent<UpgradeManager>();
    }

    private void Update()
    {
        CardPocus();
        CardSelect();
    }

    private void CardSelect()
    {
        if (Input.GetMouseButtonDown(0) && curPocusCard != null)
        {
            SkillCard card = curPocusCard.GetComponent<SkillCard>();
            manager.ApplySkill(card.GetData().skillType);

            curPocusCard = null;
        }
    }

    private void CardPocus()
    {
        PointerEventData pointerData = new PointerEventData(eventSystem)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerData, results);

        foreach (var result in results)
        {
            RectTransform skillCard = result.gameObject.GetComponent<RectTransform>();
            if (result.gameObject.CompareTag("Background"))
            {
                ResetCurrentCard();
                return;
            }
            else if (result.gameObject.CompareTag("SkillCard"))
            {
                if (curPocusCard != skillCard)
                {
                    ResetCurrentCard();
                    curPocusCard = skillCard;
                    curPocusCard.DOAnchorPosY(0, 0.1f).SetUpdate(true);
                }
                return;
            }
        }
    }

    void ResetCurrentCard()
    {
        if (curPocusCard != null)
        {
            curPocusCard.DOAnchorPosY(-50, 0.1f).SetUpdate(true);
            curPocusCard = null;
        }
    }
}
