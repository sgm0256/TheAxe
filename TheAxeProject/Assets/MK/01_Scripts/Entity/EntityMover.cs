using Core.StatSystem;
using UnityEngine;

namespace Core.Entities
{
    public class EntityMover : MonoBehaviour, IEntityComponent, IAfterInitable
    {
        [Header("Move stats")] 
        [SerializeField] private StatSO _moveStat;
        [SerializeField] private float _moveSpeed = 5f;

        public Vector2 Velocity => _rbCompo.velocity;
        public float SpeedMultiplier { get; set; } = 1f;
        
        private Rigidbody2D _rbCompo;
        private Entity _entity;
        private EntityRenderer _renderer;
        private EntityStat _stat;
        
        private float _xMovement;
        
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
            _rbCompo.velocity = Vector2.zero;
        }

        public void SetMovement(float xMovement) => _xMovement = xMovement;
        
        private void FixedUpdate()
        {
            MoveCharacter();
        }
        
        private void MoveCharacter()
        {
            _rbCompo.velocity = new Vector2(_xMovement * _moveSpeed * SpeedMultiplier, _rbCompo.velocity.y);
            
            _renderer.FlipController(_xMovement);
        }
    }
}