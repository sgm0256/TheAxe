using Core.Entities;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private InputReaderSO _inputCompo;

    private void Update()
    {
        EntityMover mover = GetCompo<EntityMover>();
        mover.SetMovement(_inputCompo.Movement);
    }
}