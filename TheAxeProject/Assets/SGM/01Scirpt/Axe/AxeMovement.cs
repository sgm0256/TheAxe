using System.Collections;
using UnityEngine;

public class AxeMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    private float angle = 45f;
    private float gravity = 9.8f;

    private void Awake()
    {
        InputReaderSO inputReader = GetComponent<Axe>().InputReader;
        inputReader.FireEvent += () =>
        {
            Vector2 mousePos = inputReader.MousePos;
            Vector2 targetPoint = Camera.main.ScreenToWorldPoint(mousePos);
            StartCoroutine(Attack(targetPoint));
        };
    }

    public IEnumerator Attack(Vector2 targetPoint)
    {
        float target_Distance = Vector2.Distance(transform.position, targetPoint);

        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * angle * Mathf.Deg2Rad) / gravity);

        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(angle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(angle * Mathf.Deg2Rad);

        float flightDuration = target_Distance / Vx;

        float angleToTarget = Mathf.Atan2(targetPoint.y - transform.position.y, targetPoint.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angleToTarget);

        float elapse_time = 0;
        while (elapse_time < flightDuration)
        {
            transform.Translate(Vx * Time.deltaTime * speed, (Vy - (gravity * elapse_time)) * Time.deltaTime * speed, 0);
            elapse_time += Time.deltaTime * speed;
            yield return null;
        }
    }
}
