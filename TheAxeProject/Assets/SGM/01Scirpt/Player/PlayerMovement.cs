using UnityEngine;

public class PlayerMovement : MonoBehaviour, IPlayerComponent
{
    [SerializeField] private float speed = 10f;

    private InputReaderSO inputReader;

    public void Initialize(Player player)
    {
        inputReader = player.GetCompo<InputReaderSO>();
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        Vector3 movement = inputReader.Movement;
        movement.z = 0;

        transform.position += movement * speed * Time.deltaTime;
    }
}
