using System;
using System.Collections;
using ObjectPooling;
using UnityEngine;

public class AttackLoad : MonoBehaviour, IPoolable
{
    [SerializeField] private PoolEnumType type;
    [SerializeField] private Transform _gauge;
    [SerializeField] private float _duration = 1f;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    [field: SerializeField] public Transform AttackPoint { get; private set; }

    public float Duration
    {
        get => _duration;
        set => _duration = Mathf.Clamp(value, 0, int.MaxValue);
    }
    
    [SerializeField] private PoolTypeSO _poolType;
    public PoolTypeSO PoolType => _poolType;
    public GameObject GameObject => gameObject;

    private Pool _myPool;

    public event Action ReadyToAttackEvent;

    public void ChargeAttackLoad() => StartCoroutine(ChargeAlpha());
    public void StartCharge() => StartCoroutine(Charging());

    private void Awake()
    {
        _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 0.2f);
    }

    private IEnumerator ChargeAlpha()
    {
        float time = 0;
        float start = 0.2f;
        Color end = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 1f);

        while (time < _duration)
        {
            _spriteRenderer.color = new Color(
                _spriteRenderer.color.r,
                _spriteRenderer.color.g, 
                _spriteRenderer.color.b,
                Mathf.Lerp(0f, 1f, time / _duration));
            
            time += Time.deltaTime;
            yield return null;
        }

        _spriteRenderer.color = end;
        SingletonPoolManager.Instnace.GetPoolManager(type).Push(this);
        ReadyToAttackEvent?.Invoke();
    }
    
    private IEnumerator Charging()
    {
        float time = 0;
        Vector3 start = _gauge.localScale;

        while (time < _duration)
        {
            _gauge.localScale = Vector3.Lerp(start, new Vector3(1, 1, 1), time / _duration);
            time += Time.deltaTime;
            yield return null;
        }
        
        _gauge.localScale = new Vector3(1, 1, 1);
        ReadyToAttackEvent?.Invoke();
    }
    
    public void SetUpPool(Pool pool)    
    {
        _myPool = pool; 
    }

    public void ResetItem()
    {
        _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 0.2f);
    }
}
