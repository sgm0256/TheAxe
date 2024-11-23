using Core.Entities;
using Core.StatSystem;
using ObjectPooling;
using System;
using UnityEngine;

public class Axe : Entity, IPoolable
{
    [SerializeField] private StatSO sizeStat;
    [SerializeField] private InputReaderSO _inputCompo;

    public Action OnAxeImpact;
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

        visualTrm = transform.Find("Visual");
        visualTrm.gameObject.SetActive(false);
        skillCompo = GetComponentInChildren<Skill>();
    }

    public void Attack(Vector2 startPos)
    {
        isAttack = true;
        visualTrm.gameObject.SetActive(true);
        visualTrm.localScale = Vector3.one * GameManager.Instance.Player.GetCompo<EntityStat>().GetStat(sizeStat).Value;
        transform.parent = null;
        transform.position = startPos;
        transform.rotation = Quaternion.identity;

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
