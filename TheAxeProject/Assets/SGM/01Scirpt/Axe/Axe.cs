using UnityEngine;

public class Axe : MonoBehaviour
{
    public InputReaderSO InputReader;

    public AxeMovement MovementCompo { get; private set; }

    private void Awake()
    {
        MovementCompo = GetComponent<AxeMovement>();
    }
}
