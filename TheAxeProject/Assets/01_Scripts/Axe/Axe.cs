using Core.Entities;
using ObjectPooling;
using System;
using UnityEngine;

public class Axe : Entity
{
    [SerializeField] private InputReaderSO _inputCompo;

    public Action OnAxeImpact;
    public Transform visualTrm;

    public bool isAttack = false;
    protected override void Awake()
    {
        base.Awake();
        _components.Add(_inputCompo.GetType(), _inputCompo);

        visualTrm = transform.Find("Visual");
        visualTrm.gameObject.SetActive(false);
    }

    public void Attack(Vector2 startPos)
    {
        isAttack = true;
        visualTrm.gameObject.SetActive(true);
        transform.position = startPos;
        transform.rotation = Quaternion.identity;

        GetCompo<Skill>().StartSkill();
    }

    public void EndAttack()
    {
        isAttack = false;
        visualTrm.gameObject.SetActive(false);
    }
}
