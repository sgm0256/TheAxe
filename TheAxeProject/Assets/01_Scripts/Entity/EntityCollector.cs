using System;
using Core.InteractiveObjects;
using UnityEngine;

namespace Core.Entities
{
    public class EntityCollector : MonoBehaviour, IEntityComponent
    {
        public event Action<InteractiveObjectInfoSO> GetObjectEvent; 
        
        // TODO : 콜렉팅 하고 먹을 수 있게 바꾸기 
        private Entity _entity;
        
        [SerializeField] private LayerMask _whatIsItem;
        [SerializeField] private int _maxColliderCount = 10;
        
        private Collider2D[] _colliderArr;
        // TODO : Collider는 어떻게 처리 할 지 생각해ㅐ 봐야 함
        private CircleCollider2D _myColl;

        [field: SerializeField] public float GetCollectRadius { get; private set; } = 5f;
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
            _myColl = GetComponent<CircleCollider2D>();
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
                // 여기서 먹은 거 처리해 줘야 됨
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, GetCollectRadius);
        }
    }
}
