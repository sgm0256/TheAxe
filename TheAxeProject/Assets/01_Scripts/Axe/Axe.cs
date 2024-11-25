using Core.Entities;
using Core.StatSystem;
using ObjectPooling;
using System;
using UnityEngine;

public class Axe : Entity, IPoolable
{
    [SerializeField] private PlayerManagerSO playerSO;
    [SerializeField] private StatSO sizeStat;
    [SerializeField] private InputReaderSO _inputCompo;

    public Action<Vector3> OnAxeImpact;
    public Transform visualTrm;
    private Skill skillCompo;

    [HideInInspector]
    public bool isAttack = false;

    public PoolTypeSO PoolType => skillCompo.skillData.poolType;

    public GameObject GameObject => gameObject;

    protected override void Awake()
    {
        base.Awake();
        _components.Add(_inputCompo.GetType(), _inputCompo);

        visualTrm.gameObject.SetActive(false);
        skillCompo = GetComponentInChildren<Skill>();

        SingletonPoolManager.Instance.OnAllPushEvent += () => SingletonPoolManager.Instance.GetPoolManager(PoolEnumType.Axe).Push(this);
    }

    public void Attack(Vector2 startPos)
    {
        isAttack = true;
        visualTrm.gameObject.SetActive(true);
        visualTrm.localScale = Vector3.one * playerSO.Player.GetCompo<EntityStat>().GetStat(sizeStat).Value;
        transform.parent = null;
        transform.position = startPos;
        transform.rotation = Quaternion.Euler(0, 0, 45);

        skillCompo.StartSkill();
    }

    public Skill GetSkill()
    {
        return skillCompo;
    }

    public void SetUpPool(Pool pool)
    {
        
    }

    public void ResetItem()
    {
        isAttack = false;
    }
}
