using ObjectPooling;
using UnityEngine;

public class EffectPlayer : MonoBehaviour, IPoolable
{
    [SerializeField] private PoolTypeSO poolType;
    private Animator anim;

    public PoolTypeSO PoolType => poolType;

    public GameObject GameObject => gameObject;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void AnimationEnd()
    {
        SingletonPoolManager.Instance.GetPoolManager(PoolEnumType.Effect).Push(this);
    }

    public void SetUpPool(Pool pool)
    {
    }

    public void ResetItem()
    {
        anim.SetTrigger("Play");
    }
}
