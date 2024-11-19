using DG.Tweening;
using ObjectPooling;
using System.Collections;
using UnityEngine;

public class VisualAxe : MonoBehaviour, IPoolable
{
    [SerializeField] private PoolTypeSO poolType;

    public PoolTypeSO PoolType => poolType;
    public GameObject GameObject => gameObject;
    public SkillDataSO SkillData { get; private set; }

    private SpriteRenderer spriteRender;
    private IEnumerator coroutine = null;


    private void Awake()
    {
        spriteRender = GetComponent<SpriteRenderer>();
    }

    public void ResetItem()
    {
        StopAllCoroutines();
        transform.DOKill();

        transform.localPosition = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    public void SetUpPool(Pool pool)
    {

    }

    public void Init(SkillDataSO data)
    {
        SkillData = data;

        spriteRender.color = data.color;
    }

    public void Sort(float moveAngle, bool isSpawn)
    {
        if (isSpawn)
        {
            Vector3 pos = (Quaternion.Euler(0, 1, moveAngle) * transform.parent.up).normalized;
            transform.DOLocalMove(pos * 2, 0.2f);
        }
        else
        {
            if (coroutine != null)
                StopCoroutine(coroutine);

            coroutine = MoveToAngle(moveAngle);
            StartCoroutine(coroutine);
        }
    }

    private IEnumerator MoveToAngle(float moveAngle)
    {
        float curAngle = Quaternion.FromToRotation(Vector3.up, transform.localPosition - Vector3.zero).eulerAngles.z;

        float timer = 0;
        while (timer <= 0.2f)
        {
            timer += Time.deltaTime;

            float angle = Mathf.Lerp(curAngle, moveAngle, timer * 5f);
            Vector3 pos = (Quaternion.Euler(0, 0, angle) * transform.parent.up).normalized;
            transform.localPosition = pos * 2;

            yield return null;
        }

        coroutine = null;
    }
}
