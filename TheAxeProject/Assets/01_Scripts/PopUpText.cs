using System;
using DG.Tweening;
using ObjectPooling;
using TMPro;
using UnityEngine;

public class PopUpText : MonoBehaviour, IPoolable
{
    private TextMeshPro _popUpText;

    private void Awake()
    {
        _popUpText = GetComponent<TextMeshPro>();
        SingletonPoolManager.Instance.OnAllPushEvent += HandleAllPush;
    }

    private void OnDestroy()
    {
        SingletonPoolManager.Instance.OnAllPushEvent -= HandleAllPush;
    }

    private void HandleAllPush()
    {
        SingletonPoolManager.Instance.Push(PoolEnumType.InteractiveObject, this);
    }

    public void StartPopUp(string text, Vector3 pos,
        float yDelta = 2f)
    {
        _popUpText.SetText(text);
        transform.position = pos;

        float scaleTime = 0.2f;
        float fadeTime = 1.5f;

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(2.5f, scaleTime));
        seq.Append(transform.DOScale(1.2f, scaleTime));
        seq.Append(transform.DOScale(0.3f, fadeTime));
        seq.Join(_popUpText.DOFade(0, fadeTime));
        seq.Join(transform.DOLocalMoveY(pos.y + yDelta, fadeTime));
        seq.AppendCallback(() => SingletonPoolManager.Instance.Push(PoolEnumType.InteractiveObject, this));
    }

    [field: SerializeField] public PoolTypeSO PoolType { get; set; }
    public GameObject GameObject => gameObject;

    private Pool _myPool;

    public void SetUpPool(Pool pool)
    {
        _myPool = pool;
    }

    public void ResetItem()
    {
        transform.localScale = Vector3.one;
        _popUpText.alpha = 1f;
    }
}
