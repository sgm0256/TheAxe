using Core.StatSystem;
using DG.Tweening;
using ObjectPooling;
using System.Collections;
using UnityEngine;

public class VisualAxe : MonoBehaviour, IPoolable
{
    [SerializeField] private PlayerManagerSO playerSO;
    [SerializeField] private StatSO sizeStat;
    [SerializeField] private PoolTypeSO poolType;

    public PoolTypeSO PoolType => poolType;
    public GameObject GameObject => gameObject;
    public SkillDataSO SkillData { get; private set; }

    private SpriteRenderer spriteRender;
    private IEnumerator coroutine = null;


    private void Awake()
    {
        spriteRender = GetComponentInChildren<SpriteRenderer>();

        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        playerSO.Player.GetCompo<EntityStat>().GetStat(sizeStat).OnValueChange += (stat, current, previous) => transform.localScale = Vector3.one * stat.Value;
    }

    private void Update()
    {
        Vector3 dir = playerSO.Player.transform.position - transform.position;
        transform.up = -dir.normalized;
    }

    public void ResetItem()
    {
        StopAllCoroutines();
        transform.DOKill();

        transform.localPosition = Vector3.zero;
    }

    public void SetUpPool(Pool pool)
    {
        
    }

    public void Init(SkillDataSO data)
    {
        SkillData = data;
        spriteRender.sprite = data.sprite;
    }

    public void Sort(float moveAngle, bool isSpawn)
    {
        if (isSpawn)
        {
            Vector3 pos = (Quaternion.Euler(0, 1, moveAngle) * transform.root.up).normalized;
            transform.DOLocalMove(pos * transform.localScale.x, 0.2f);
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
            Vector3 pos = (Quaternion.Euler(0, 0, angle) * transform.root.up).normalized;
            transform.localPosition = pos * transform.localScale.x;

            yield return null;
        }

        coroutine = null;
    }
}
