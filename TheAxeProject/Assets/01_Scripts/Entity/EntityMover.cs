using System;
using System.Collections;
using Core.StatSystem;
using UnityEngine;

namespace Core.Entities
{
    public class EntityMover : MonoBehaviour, IEntityComponent, IAfterInitable
    {
        private float _moveSpeed;
        
        public Vector2 Velocity => _rbCompo.velocity;
        public float SpeedMultiplier { get; set; } = 1f;
        public bool CanManualMove { get; set; } = true;
        
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
            _collider.enabled = true;

            GameManager.Instance.OnGameClearEvent += HandleClear;
        }
        
        public void AfterInit()
        {
            _stat.MoveSpeedStat.OnValueChange += HandleMoveSpeedChange;
            _moveSpeed = _stat.MoveSpeedStat.Value;
        }

        private void OnEnable()
        {
            CanManualMove = true;
        }

        private void OnDestroy()
        {
            _stat.MoveSpeedStat.OnValueChange -= HandleMoveSpeedChange;
            GameManager.Instance.OnGameClearEvent -= HandleClear;
        }
        
        private void HandleClear()
        {
            _collider.enabled = false;
        }

        private void HandleMoveSpeedChange(StatSO stat, float current, float previous)
        {
            _moveSpeed = current;
        }

        public void StopImmediately()
        {
            _movementVec = Vector2.zero;
        }

        public void StopMove()
        {
            StartCoroutine(StopMoveCoroutine());
        }

        private IEnumerator StopMoveCoroutine()
        {
            CanManualMove = false;
            StopImmediately();
            yield return new WaitForSeconds(0.2f);
            CanManualMove = true;
        }

        public void SetMovement(Vector2 movement) => _movementVec = movement.normalized;
        
        private void FixedUpdate()
        {
            MoveCharacter();
        }
        
        private void MoveCharacter()
        {
            _rbCompo.velocity = _moveSpeed * SpeedMultiplier * _movementVec;
            Debug.Log(_movementVec);
            
            _renderer.FlipController(_rbCompo.velocity.x);
        }
    }
}