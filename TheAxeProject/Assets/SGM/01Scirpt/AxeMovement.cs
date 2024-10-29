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
        // �������� ��ǥ�� ������ �Ÿ� ���
        float target_Distance = Vector3.Distance(transform.position, targetPoint);

        // �ʱ� �ӵ� ���
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * _angle * Mathf.Deg2Rad) / _gravity);

        // XZ ��鿡���� �ӵ� ���
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(_angle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(_angle * Mathf.Deg2Rad);

        // ���� �ð� ���
        float flightDuration = target_Distance / Vx;

        // �߻� ���� ����
        transform.rotation = Quaternion.LookRotation(targetPoint - transform.position);

        // ���� �ð� ���� �̵�
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
