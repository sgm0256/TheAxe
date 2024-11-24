using ObjectPooling;
using UnityEngine;

public class AttackFire : MonoBehaviour, IPoolable
{
    public PoolTypeSO PoolType { get; }
    public GameObject GameObject { get; }
    public void SetUpPool(Pool pool)
    {
        
    }

    public void ResetItem()
    {
        
    }
}
