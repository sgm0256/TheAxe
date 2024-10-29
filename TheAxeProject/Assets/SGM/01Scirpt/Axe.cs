using UnityEngine;

public class Axe : MonoBehaviour
{
    public AxeAttack AttackCompo { get; private set; }
    public AxeMovement MovementCompo { get; private set; }

    private void Awake()
    {
        AttackCompo = GetComponent<AxeAttack>();
        MovementCompo = GetComponent<AxeMovement>();
    }
}
