using Core.Entities;
using Core.InteractiveItem;
using DG.Tweening;
using UnityEngine;

namespace Core.ExpSystem
{
    public class Exp : MonoBehaviour, IPickUpItem
    {
        [SerializeField] private ExpInfoSO _info;
        [SerializeField] private float _duration = 0.3f;
        [SerializeField] private float _speed = 3f;
        [SerializeField] private float _getDistance = 2f;
        [SerializeField] private float _speedWeighting = 3f;

        private Entity _target;
        private Rigidbody2D _myRigid;
        
        private bool _isPick = false;
        private bool _isMovement = false;
        private float _acceleration = 1f;

        private void Awake()
        {
            _myRigid = GetComponent<Rigidbody2D>();
        }

        public void PickUpItem(Entity entity)
        {
            if (_isPick) return;
            _isPick = true;

            _target = entity;

            Vector2 direction = (_target.transform.position - transform.position).normalized;
            Vector2 targetPosition = (Vector2)transform.position - direction * _getDistance;

            transform.DOMove(targetPosition, _duration)
                .SetEase(Ease.Linear)
                .OnComplete(() => _isMovement = true);
        }

        private void FixedUpdate()
        {
            if (_isMovement)
            {
                Vector2 direction = _target.transform.position - transform.position;
                _myRigid.velocity = (_speed + _acceleration) * direction.normalized;
                _acceleration += Time.deltaTime * _speedWeighting;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Entity entity))
            {
                // TODO : 풀 넣기
                Destroy(gameObject);
            }
        }
    }
}
