using Core.Entities;
using ObjectPooling;
using UnityEngine;

public class AttackFire : MonoBehaviour, IPoolable
{
    [field: SerializeField] public PoolTypeSO PoolType { get; set; }
    public GameObject GameObject => gameObject;

    private Pool _myPool;

    private Animator _animator;
    
    public void SetUpPool(Pool pool)
    {
        _myPool = pool;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        SingletonPoolManager.Instance.OnAllPushEvent += HandleAllPush;
    }

    private void OnDestroy()
    {
        SingletonPoolManager.Instance.OnAllPushEvent -= HandleAllPush;
    }

    private void HandleAllPush()
    {
        SingletonPoolManager.Instance.Push(PoolEnumType.RanageAttack, this);
    }

    public void ResetItem()
    {
        _animator.Play("enemy_fire");
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out Player player))
            {
                EntityHealth health = player.GetCompo<EntityHealth>();
                health.ApplyDamage(10);
            }
        }
    }

    public void AnimTrigger()
    {
        SingletonPoolManager.Instance.Push(PoolEnumType.RanageAttack, this);
    }
}
