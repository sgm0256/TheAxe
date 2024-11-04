using DG.Tweening;
using System.Collections;
using UnityEngine;

public class AxeController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float rotateSpeed = 1f;
    [SerializeField] private float attackAngle = 45f;
    private int dir;
    private float curAngle = 0;
    private float gravity = 9.8f;
    private bool isAttack = false;

    private Transform visualTrm;
    private Player player;

    private void Start()
    {
        visualTrm = transform.Find("Visual");
        player = GetComponentInParent<Player>();
    }

    private void Update()
    {
        Rotate();

        if (!isAttack)
            transform.rotation = Quaternion.identity;
    }

    private void Rotate()
    {
        if (isAttack)
            visualTrm.rotation = Quaternion.Euler(0, 0, visualTrm.rotation.eulerAngles.z + (rotateSpeed * -dir));
    }

    public void MoveTheCircle(float moveAngle, bool isSpawn)
    {
        if (isSpawn)
        {
            Vector3 pos = (Quaternion.Euler(0, 1, moveAngle) * transform.parent.up).normalized;
            transform.DOLocalMove(pos, 0.1f);
            curAngle = moveAngle;
        }
        else
        {
            StartCoroutine(MoveCoroutine(moveAngle));
        }
    }

    private IEnumerator MoveCoroutine(float moveAngle)
    {
        float timer = 0;
        while (timer <= 0.1f)
        {
            timer += Time.deltaTime;

            float angle = Mathf.Lerp(curAngle, moveAngle, timer * 10);
            Vector3 pos = (Quaternion.Euler(0, 0, angle) * transform.parent.up).normalized;
            transform.localPosition = pos;

            yield return null;
        }

        curAngle = moveAngle;
    }

    public void StartAttack()
    {
        Vector2 mousePos = player.GetCompo<InputReaderSO>().MousePos;
        Vector2 targetPoint = Camera.main.ScreenToWorldPoint(mousePos);

        StopAllCoroutines();
        transform.parent = null;
        transform.DOKill();

        dir = (Random.Range(0, 2) == 0 ? -1 : 1);

        StartCoroutine(Attack(targetPoint));
    }

    private IEnumerator Attack(Vector2 targetPoint)
    {
        isAttack = true;

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
            transform.Translate(new Vector3(Vx, dir * (Vy - (gravity * elapse_time)), 0) * Time.deltaTime * moveSpeed);
            elapse_time += Time.deltaTime * moveSpeed;
            yield return null;
        }

        Destroy(gameObject);
    }
}
