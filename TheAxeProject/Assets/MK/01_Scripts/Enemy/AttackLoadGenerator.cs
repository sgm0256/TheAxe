using UnityEngine;

public class AttackLoadGenerator : MonoBehaviour
{
    [SerializeField] protected PoolManagerSO _poolManager;
    public PoolManagerSO PoolManager => _poolManager;

    private void Awake()
    {
        _poolManager.InitializePool(transform);
    }
}
