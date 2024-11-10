using Core.Entities;
using DG.Tweening;
using ObjectPooling;
using System.Collections;
using UnityEngine;

public class AxeMover : MonoBehaviour, IEntityComponent
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float rotateSpeed = 1f;
    [SerializeField] private float attackAngle = 45f;
    private int dir;
    private float gravity = 9.8f;

    private IEnumerator coroutine = null;

    private Transform visualTrm;
    private Entity parentEntity;
    private Axe axe;

    public void Initialize(Entity entity)
    {
        parentEntity = transform.root.GetComponent<Entity>();

        axe = (Axe)entity;
        visualTrm = transform.Find("Visual");
    }

    public void Sort(float moveAngle, bool isSpawn)
    {
        if (isSpawn)
        {
            Vector3 pos = (Quaternion.Euler(0, 1, moveAngle) * transform.parent.up).normalized;
            transform.DOLocalMove(pos, 0.2f);
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
            transform.localPosition = pos;

            yield return null;
        }

        coroutine = null;
    }

    public void AttackMove()
    {
        Vector2 mousePos = parentEntity.GetCompo<InputReaderSO>().MousePos;
        Vector2 targetPoint = Camera.main.ScreenToWorldPoint(mousePos);

        if (coroutine != null)
            StopCoroutine(coroutine);

        transform.DOKill();
        transform.parent = null;

        dir = (Random.Range(0, 2) == 0 ? -1 : 1);

        StartCoroutine(Attack(targetPoint));
    }

    private IEnumerator Attack(Vector2 targetPoint)
    {
        float target_Distance = Vector2.Distance(transform.position, targetPoint);

        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * attackAngle * Mathf.Deg2Rad) / gravity);

        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(attackAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(attackAngle * Mathf.Deg2Rad);

        float flightDuration = target_Distance / Vx;

        float angleToTarget = Mathf.Atan2(targetPoint.y - transform.position.y, targetPoint.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angleToTarget);

        float elapse_time = 0;
        while (elapse_time < flightDuration)
        {
            Rotate();

            transform.Translate(new Vector3(Vx, dir * (Vy - (gravity * elapse_time)), 0) * Time.deltaTime * moveSpeed);

            elapse_time += Time.deltaTime * moveSpeed;
            yield return null;
        }

        axe.OnAxeImpact.Invoke();
        SingletonPoolManager.Instance.GetPoolManager(PoolEnumType.Axe).Push(axe);
    }

    private void Rotate()
    {
        visualTrm.rotation = Quaternion.Euler(0, 0, visualTrm.rotation.eulerAngles.z + (rotateSpeed * -dir));
    }
}
