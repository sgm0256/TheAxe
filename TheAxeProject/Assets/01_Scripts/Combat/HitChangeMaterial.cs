using System.Collections;
using Core.Entities;
using UnityEngine;

public class HitChangeMaterial : MonoBehaviour, IEntityComponent
{
    [SerializeField] private Material _flashMaterial;
    [SerializeField] private float _hitTime = 0.2f;

    private Entity _entity;
    private EntityHealth _health;
    private SpriteRenderer _renderer;
    private Material _originMaterial;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _originMaterial = _renderer.material;
    }

    public void Initialize(Entity entity)
    {
        _entity = entity;
        _health = entity.GetCompo<EntityHealth>();
    }
    
    private void OnEnable()
    {
        _renderer.material = _originMaterial;
    }
    
    public void HitMaterial()
    {
        if (_health.IsDead) return;
        
        StartCoroutine(HitMaterialCoroutine());
    }
    
    private IEnumerator HitMaterialCoroutine()
    { 
        _renderer.material = _flashMaterial; 
        yield return new WaitForSeconds(_hitTime); 
        _renderer.material = _originMaterial;
    }
}
