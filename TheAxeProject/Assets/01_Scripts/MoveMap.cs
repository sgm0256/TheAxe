using System.Collections;
using System.Collections.Generic;
using ObjectPooling;
using UnityEngine;

public class MoveMap : MonoBehaviour
{
    public List<Transform> genTrm = new List<Transform>();
    
    [SerializeField] private PlayerManagerSO _playerManager;
    [SerializeField] private PoolManagerSO _poolManager;
    [SerializeField] private Transform _bush;
    [SerializeField] private Transform _tree;
    [SerializeField] private float _genTime = 12f;
    
    private Vector2 previousPlayerPos;

    private void Start()
    {
        previousPlayerPos = _playerManager.PlayerTrm.position;
        //StartCoroutine(GenBushAndTree());
    }

    private void Update()
    {
        if (Mathf.Abs(previousPlayerPos.y - _playerManager.PlayerTrm.position.y) >= 8.5f ||
            Mathf.Abs(previousPlayerPos.x - _playerManager.PlayerTrm.position.x) >= 8.5f)
        {
            transform.position = _playerManager.PlayerTrm.position;
            previousPlayerPos = _playerManager.PlayerTrm.position;
        }
    }

    private IEnumerator GenBushAndTree()
    {
        Transform environment = SingletonPoolManager.Instance.Pop(PoolEnumType.Environment, _poolManager.poolList[Random.Range(0, 2)]) as Transform;
        environment.position = genTrm[Random.Range(0, genTrm.Count + 1)].position;
        yield return new WaitForSeconds(_genTime);
    }
}
