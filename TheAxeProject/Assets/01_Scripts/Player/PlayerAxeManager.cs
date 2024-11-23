using BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform;
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
    private float attackCoolTime = 0.05f;
    private bool isSpawning = false;
    private bool isAttacking = false;
    private bool isAttackHold = false;
    private int orderIdx = 0;
    private int maxAxeCount = 5;

    private InputReaderSO input;
    private Entity entity;

    private List<VisualAxe> axeList = new();

    public void Initialize(Entity entity)
    {
        this.entity = entity;

        input = entity.GetCompo<InputReaderSO>();
        input.FireEvent += (isAttack) => isAttackHold = isAttack;
    }

    private void Start()
    {
        EntityStat stat = entity.GetCompo<EntityStat>();
        stat.GetStat(axeCntStat).OnValueChange += (stat, cur, prev) => maxAxeCount = (int)stat.Value;

        entity.GetCompo<EntityLevel>().LevelUpEvent += (level) => spawnCoolTime -= 0.1f;
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
        if(isAttackHold && axeList.Count > 0 && !isAttacking)
        {
            isAttacking = true;
            StartCoroutine(Attack());
        }

        Rotate();
    }

    private void Rotate()
    {
        axeContainer.Rotate(0, 0, Time.deltaTime * 100);
    }

    private IEnumerator CreateAxe()
    {
        SkillDataSO data = SkillManager.Instance.UseSkillList[orderIdx++];
        if (orderIdx > SkillManager.Instance.UseSkillList.Count - 1)
            orderIdx = 0;

        VisualAxe axe = SingletonPoolManager.Instance.GetPoolManager(PoolEnumType.Axe)
            .Pop(visualAxePoolType) as VisualAxe;
        axe.transform.SetParent(axeContainer, false);
        axe.Init(data);

        axeList.Add(axe);
        SortAxe(true);

        yield return new WaitForSeconds(spawnCoolTime);
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

    private IEnumerator Attack()
    {
        Vector3 mousePos = input.MousePos;

        float maxdistance = 0;
        VisualAxe attackAxe = null;
        foreach(VisualAxe visualAxe in axeList)
        {
            float distance = Vector3.Distance(mousePos, visualAxe.transform.position);

            if(distance > maxdistance)
            {
                maxdistance = distance;
                attackAxe = visualAxe;
            }
        }

        axeList.Remove(attackAxe);
        SortAxe(false);

        Axe axe = SingletonPoolManager.Instance.GetPoolManager(PoolEnumType.Axe).Pop(attackAxe.SkillData.poolType) as Axe;
        axe.Attack(attackAxe.transform.position);

        SingletonPoolManager.Instance.GetPoolManager(PoolEnumType.Axe).Push(attackAxe);

        yield return new WaitForSeconds(attackCoolTime);
        isAttacking = false;
    }
}
