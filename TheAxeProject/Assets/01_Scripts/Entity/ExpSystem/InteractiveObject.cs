using Core.Entities;
using DG.Tweening;
using ObjectPooling;
using UnityEngine;

namespace Core.InteractiveObjects
{
    public enum InteractiveType
    {
        Exp, Item, Coin
    }
    
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class InteractiveObject : MonoBehaviour, IPoolable
    {
        public GameObject GameObject { get => gameObject; }
        public InteractiveObjectInfoSO Info { get; private set; }
        
        [field: SerializeField] public PoolTypeSO PoolType { get; set; }
        [SerializeField] protected InteractiveOverride InfoOverride;
        [SerializeField] protected float _duration = 0.3f;
        [SerializeField] protected float _speed = 3f;
        [SerializeField] protected float _getDistance = 2f;
        [SerializeField] protected float _speedWeighting = 3f;
        
        protected Entity _entity;
        protected Pool _myPool;
        protected Rigidbody2D _myRigid;
        protected bool _isPick = false;
        protected bool _isMovement = false;
        protected float _acceleration = 1f;
        
        protected virtual void Awake()
        {
            Info = InfoOverride.CreateInfo();
            _myRigid = GetComponent<Rigidbody2D>();
        }
        
        protected virtual void FixedUpdate()
        {
            if (_isMovement)
            {
                Vector2 direction = _entity.transform.position - transform.position;
                _myRigid.velocity = (_speed + _acceleration) * direction.normalized;
                _acceleration += Time.deltaTime * _speedWeighting;
            }
        }
        
        public void SetUpPool(Pool pool)
        {
            _myPool = pool;
        }

        public virtual void ResetItem()
        {
            _isPick = false; 
            _isMovement = false;
        }
        
        public virtual void PickUpItem(Entity entity)
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

        public void PopObject(Transform transform)
        {
            Transform obj = SingletonPoolManager.Instance.Pop(PoolEnumType.InteractiveObject, PoolType) as Transform;
            obj = transform;
        }
        
        public void PushObject()
        {
            SingletonPoolManager.Instance.Push(PoolEnumType.InteractiveObject, this);

        }
    }
}
