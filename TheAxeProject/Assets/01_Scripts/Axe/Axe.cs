using Core.Entities;
using System;
using UnityEngine;

public class Axe : Entity
{
    [SerializeField] private InputReaderSO _inputCompo;

    public Action OnAxeImpact;
    public Transform visualTrm;
    private Skill skillCompo;

    public bool isAttack = false;
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
        transform.parent = null;
        transform.position = startPos;
        transform.rotation = Quaternion.identity;

        skillCompo.StartSkill();
    }

    public void EndAttack()
    {
        isAttack = false;
        visualTrm.gameObject.SetActive(false);
        transform.SetParent(SkillManager.Instance.transform);
    }

    public Skill GetSkill()
    {
        return skillCompo;
    }
}
