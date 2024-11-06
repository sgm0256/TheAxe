using Core.StatSystem;
using UnityEngine;

namespace Core.Entities
{
    public class EntityMover : MonoBehaviour, IEntityComponent, IAfterInitable
    {
        [Header("Move stats")] 
        [SerializeField] private StatSO _moveStat;

        private float _moveSpeed;

        public Vector2 Velocity => _rbCompo.velocity;
        public float SpeedMultiplier { get; set; } = 1f;
        
        private Rigidbody2D _rbCompo;
        private Entity _entity;
        private EntityRenderer _renderer;
        private EntityStat _stat;
        
        private Vector2 _movementVec;
        
        private Collider2D _collider;

        public void Initialize(Entity entity)
        {
            _entity = entity;
            _rbCompo = entity.GetComponent<Rigidbody2D>();
            _renderer = entity.GetCompo<EntityRenderer>();
            _stat = entity.GetCompo<EntityStat>();

            _collider = entity.GetComponent<Collider2D>();
        }
        
        public void AfterInit()
        {
            _stat.MoveSpeedStat.OnValueChange += HandleMoveSpeedChange;
            _moveSpeed = _stat.MoveSpeedStat.Value;
        }

        private void OnDestroy()
        {
            _stat.MoveSpeedStat.OnValueChange -= HandleMoveSpeedChange;
        }

        private void HandleMoveSpeedChange(StatSO stat, float current, float previous)
        {
            _moveSpeed = current;
        }

        public void StopImmediately()
        {
            _movementVec = Vector2.zero;
        }

        public void SetMovement(Vector2 movement) => _movementVec = movement.normalized;
        
        private void FixedUpdate()
        {
            MoveCharacter();
        }
        
        private void MoveCharacter()
        {
            _rbCompo.velocity = _moveSpeed * SpeedMultiplier * _movementVec;
            
            _renderer.FlipController(_rbCompo.velocity.x);
        }
    }
}