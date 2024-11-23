using Core.Entities;
using Core.StatSystem;
using ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAxeManager : MonoBehaviour, IEntityComponent
{
    [SerializeField] private PoolTypeSO visualAxePoolType;
    [SerializeField] private Transform axeContainer;
    [SerializeField] private StatSO axeCntStat;
    private float spawnCoolTime = 1f;
    private bool isSpawning = false;
    private int orderIdx = 0;
    private int maxAxeCount = 5;

    private InputReaderSO input;
    private Entity entity;

    private List<VisualAxe> axeList = new();

    public void Initialize(Entity entity)
    {
        this.entity = entity;

        input = entity.GetCompo<InputReaderSO>();
        input.FireEvent += Attack;
    }

    private void Start()
    {
        EntityStat stat = entity.GetCompo<EntityStat>();
        stat.GetStat(axeCntStat).OnValueChange += (stat, cur, prev) =>
        {
            maxAxeCount = (int)stat.Value;
            spawnCoolTime -= 0.15f;
        };
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

        SkillDataSO data = SkillManager.Instance.UseSkillList[orderIdx++];
        if (orderIdx > SkillManager.Instance.UseSkillList.Count - 1)
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

        Axe axe = SingletonPoolManager.Instance.GetPoolManager(PoolEnumType.Axe).Pop(visualAxe.SkillData.poolType) as Axe;
        axe.Attack(visualAxe.transform.position);

        SingletonPoolManager.Instance.GetPoolManager(PoolEnumType.Axe).Push(visualAxe);
    }
}
