using System.Collections;
using UnityEngine;

public class AxeMovement : Axe
{
    private Vector3 originPos;

    [SerializeField] float _angle = 45f;
    private float _gravity = 9.8f;

    private void Awake()
    {
        originPos = transform.position;

        AttackCompo.OnAttackEvent += (targetPoint) => StartCoroutine(Attack(targetPoint));
    }

    public IEnumerator Attack(Vector3 targetPoint)
    {
        // 시작점과 목표점 사이의 거리 계산
        float target_Distance = Vector3.Distance(transform.position, targetPoint);

        // 초기 속도 계산
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * _angle * Mathf.Deg2Rad) / _gravity);

        // XZ 평면에서의 속도 계산
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(_angle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(_angle * Mathf.Deg2Rad);

        // 비행 시간 계산
        float flightDuration = target_Distance / Vx;

        // 발사 방향 설정
        transform.rotation = Quaternion.LookRotation(targetPoint - transform.position);

        // 비행 시간 동안 이동
        float elapse_time = 0;
        while (elapse_time < flightDuration)
        {
            transform.Translate(0, (Vy - (_gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);
            elapse_time += Time.deltaTime;
            yield return null;
        }

        transform.position = originPos;
    }
}
