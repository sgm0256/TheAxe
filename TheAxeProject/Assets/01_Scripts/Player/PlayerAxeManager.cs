using Core.Entities;
using ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAxeManager : MonoBehaviour, IEntityComponent
{
    [SerializeField] private Transform axeContainer;
    [SerializeField] private PoolTypeSO normalAxeType;
    [SerializeField] private int maxAxeCount = 3;
    [SerializeField] private float rotationSpeed = 5f;
    private float spawnCoolTime = 1f;
    private bool isSpawning = false;
    private int orderIdx = 1;

    private InputReaderSO input;

    private List<PoolTypeSO> skillList = new();
    private List<Axe> axeList = new List<Axe>();

    public void Initialize(Entity entity)
    {
        input = entity.GetCompo<InputReaderSO>();
        input.FireEvent += Attack;

        skillList.Add(normalAxeType);
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

    public void UpgradeSkill(SkillType type)
    {
        int level = SkillManager.Instance.GetSkillLevel(type);

        if(level == 1)
        {
            PoolTypeSO typeSo = SkillManager.Instance.GetSkillPoolType(type.ToString());
            skillList.Add(typeSo);
        }

        foreach(Axe axe in axeList)
        {
            if(axe.GetCompo<Skill>().Type == type)
            {
                // skill upgrade
            }
        }
    }

    private IEnumerator CreateAxe()
    {
        yield return new WaitForSeconds(spawnCoolTime);
        //yield return null;

        PoolTypeSO axeType = skillList[orderIdx++];
        if (orderIdx > skillList.Count - 1)
            orderIdx = 0;
        Axe axe = SingletonPoolManager.Instance.GetPoolManager(PoolEnumType.Axe).Pop(axeType) as Axe;

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
            axeList[i].GetCompo<AxeMover>().Sort(angle, isSpawn && isLast);
        }
    }

    private void Attack()
    {
        if (axeList.Count == 0)
            return;

        Axe axe = axeList[0];
        axeList.Remove(axe);
        SortAxe(false);

        axe.GetCompo<Skill>().StartSkill();
    }
}
