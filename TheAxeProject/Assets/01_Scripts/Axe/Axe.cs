using Core.Entities;
using DG.Tweening;
using ObjectPooling;
using System;
using System.Collections;
using UnityEngine;

public class Axe : Entity, IPoolable
{
    [SerializeField] private PoolTypeSO poolType;

    public PoolTypeSO PoolType => poolType;

    public GameObject GameObject => gameObject;

    public Action OnAxeImpact;

    public void ResetItem()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    public void SetUpPool(Pool pool)
    {
        
    }
}
