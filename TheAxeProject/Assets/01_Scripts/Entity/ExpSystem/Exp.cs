using Core.Entities;
using Core.InteractiveObjects;
using DG.Tweening;
using ObjectPooling;
using UnityEngine;

namespace Core.ExpSystem
{
    public class Exp : InteractiveObject, IPoolable
    {
        [SerializeField] private float _duration = 0.3f;
        [SerializeField] private float _speed = 3f;
        [SerializeField] private float _getDistance = 2f;
        [SerializeField] private float _speedWeighting = 3f;
        
        [field: SerializeField] public PoolTypeSO PoolType { get; private set; }
        public GameObject GameObject { get => gameObject; }

        private Pool _pool;
        private Rigidbody2D _myRigid;
        
        private bool _isPick = false;
        private bool _isMovement = false;
        private float _acceleration = 1f;
        
        public void SetUpPool(Pool pool)
        {
            _pool = pool;
        }

        public void ResetItem()
        {
            _isPick = false; 
            _isMovement = false;
        }

        protected override void Awake()
        {
            base.Awake();
            _myRigid = GetComponent<Rigidbody2D>();
        }

        public override void PickUpItem(Entity entity)
        {
            if (_isPick) return;
            _isPick = true;

            _entity = entity;

            Vector2 direction = (_entity.transform.position - transform.position).normalized;
            Vector2 targetPosition = (Vector2)transform.position - direction * _getDistance;

            transform.DOMove(targetPosition, _duration)
                .SetEase(Ease.Linear)
                .OnComplete(() => _isMovement = true);
        }

        private void FixedUpdate()
        {
            if (_isMovement)
            {
                Vector2 direction = _entity.transform.position - transform.position;
                _myRigid.velocity = (_speed + _acceleration) * direction.normalized;
                _acceleration += Time.deltaTime * _speedWeighting;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Entity entity))
            {
                SingletonPoolManager.Instance.GetPoolManager(PoolEnumType.InteractiveObject)?.Push(this);
                Destroy(gameObject);
            }
        }
    }
}
