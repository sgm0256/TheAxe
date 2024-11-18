using Core.Entities;
using ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAxeManager : MonoBehaviour, IEntityComponent
{
    [SerializeField] private PoolTypeSO visualAxePoolType;
    [SerializeField] private Transform axeContainer;
    [SerializeField] private int maxAxeCount = 3;
    [SerializeField] private float rotationSpeed = 5f;
    private float spawnCoolTime = 1f;
    private bool isSpawning = false;
    private int orderIdx = 0;

    private InputReaderSO input;

    private List<VisualAxe> axeList = new();

    public void Initialize(Entity entity)
    {
        input = entity.GetCompo<InputReaderSO>();
        input.FireEvent += Attack;
    }

    private void Update()
    {
        if (axeList.Count < maxAxeCount && !isSpawning)
        {
            //if (Input.GetKeyDown(KeyCode.Space))
            {
                isSpawning = true;
                StartCoroutine(CreateAxe());
            }
        }
    }

    private IEnumerator CreateAxe()
    {
        yield return new WaitForSeconds(spawnCoolTime);
        //yield return null;

        SkillDataSO data = SkillManager.Instance.SkillList[orderIdx++];
        if (orderIdx > SkillManager.Instance.SkillList.Count - 1)
            orderIdx = 0;

        VisualAxe axe = SingletonPoolManager.Instance.GetPoolManager(PoolEnumType.Axe)
            .Pop(visualAxePoolType) as VisualAxe;
        axe.transform.SetParent(transform, false);
        axe.Init(data);

        axeList.Add(axe);
        SortAxe(true);

        isSpawning = false;
    }

    private void SortAxe(bool isSpawn)
    {
        if (axeList.Count == 0)
            return;

        float curAngle = 360 / axeList.Count;

        for (int i = 0; i < axeList.Count; i++)
        {
            float angle = i * curAngle;
            bool isLast = i == axeList.Count - 1;
            axeList[i].Sort(angle, isSpawn && isLast);
        }
    }

    private void Attack()
    {
        if (axeList.Count == 0)
            return;

        VisualAxe visualAxe = axeList[0];
        axeList.Remove(visualAxe);
        SortAxe(false);

        Axe axe = SkillManager.Instance.GetAxeOfSkillType(visualAxe.SkillData.skillType);
        axe.Attack(visualAxe.transform.position);

        SingletonPoolManager.Instance.GetPoolManager(PoolEnumType.Axe).Push(visualAxe);
    }
}
