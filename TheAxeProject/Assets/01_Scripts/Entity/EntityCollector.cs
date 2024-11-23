using System;
using Core.InteractiveObjects;
using UnityEngine;

namespace Core.Entities
{
    [RequireComponent(typeof(Collider2D))]
    public class EntityCollector : MonoBehaviour, IEntityComponent
    {
        public event Action<InteractiveObjectInfoSO> GetObjectEvent; 
        private Entity _entity;
        
        [SerializeField] private LayerMask _whatIsItem;
        [SerializeField] private int _maxColliderCount = 10;
        
        private Collider2D[] _colliderArr;

        [field: SerializeField] public float GetCollectRadius { get; private set; } = 5f;
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
            _colliderArr = new Collider2D[_maxColliderCount];
        }

        private void Update()
        {
            int hit = Physics2D.OverlapCircleNonAlloc(transform.position, GetCollectRadius, _colliderArr,
                _whatIsItem);

            if (hit > 0)
            {
                for (int i = 0; i < hit; i++)
                {
                    Collider2D col = _colliderArr[i];
                    
                    if (col is null == false && col.TryGetComponent(out InteractiveObject obj))
                    {
                        obj.PickUpItem(_entity);
                    }
                }
            }
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            // TODO : 이렇게 중구난방으로 여기는 콜라이더 다른 곳은 Laycast 방식 바꿔야 할 듯
            // TODO : 일단 개발하고 바꾸기

            if (other.TryGetComponent(out InteractiveObject obj))
            {
                GetObjectEvent?.Invoke(obj.Info);
                obj.PushObject();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, GetCollectRadius);
        }
    }
}
