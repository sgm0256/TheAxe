using Core.Entities;
using System.Collections;
using UnityEngine;

public class AxeMover : MonoBehaviour, IEntityComponent
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float rotateSpeed = 1f;
    [SerializeField] private float attackAngle = 45f;
    private int dir;
    private float gravity = 9.8f;

    private Axe axe;

    private void Update()
    {
        axe.visualTrm.rotation = Quaternion.Euler(0, 0, axe.visualTrm.rotation.eulerAngles.z + (rotateSpeed * -dir));
    }

    public void Initialize(Entity entity)
    {
        axe = (Axe)entity;
    }

    public void AttackMove()
    {
        Vector2 mousePos = axe.GetCompo<InputReaderSO>().MousePos;
        Vector2 targetPoint = Camera.main.ScreenToWorldPoint(mousePos);

        dir = (Random.Range(0, 2) == 0 ? -1 : 1);

        StartCoroutine(Attack(targetPoint));
    }

    private IEnumerator Attack(Vector2 targetPoint)
    {
        Transform axeTrm = axe.transform;

        float target_Distance = Vector2.Distance(axeTrm.position, targetPoint);

        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * attackAngle * Mathf.Deg2Rad) / gravity);

        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(attackAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(attackAngle * Mathf.Deg2Rad);

        float flightDuration = target_Distance / Vx;

        float angleToTarget = Mathf.Atan2(targetPoint.y - axeTrm.position.y, targetPoint.x - axeTrm.position.x) * Mathf.Rad2Deg;
        axeTrm.rotation = Quaternion.Euler(0, 0, angleToTarget);

        Vector3 lastDir = new();
        float elapse_time = 0;
        while (elapse_time < flightDuration)
        {
            lastDir = ((Vector3)targetPoint - transform.position).normalized;
            axeTrm.Translate(new Vector3(Vx, dir * (Vy - (gravity * elapse_time)), 0) * Time.deltaTime * moveSpeed);

            elapse_time += Time.deltaTime * moveSpeed;
            yield return null;
        }
        axe.OnAxeImpact?.Invoke(lastDir);
    }
}
