using Core.Entities;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private InputReaderSO _inputCompo;

    protected override void Awake()
    {
        _components.Add(_inputCompo.GetType(), _inputCompo);
        base.Awake();
    }

    private void Update()
    {
        EntityMover mover = GetCompo<EntityMover>();
        mover.SetMovement(_inputCompo.Movement);
    }
}