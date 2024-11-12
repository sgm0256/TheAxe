using Core.InteractiveItem;
using UnityEngine;

namespace Core.Entities
{
    public class EntityCollector : MonoBehaviour, IEntityComponent
    {
        // TODO : 콜렉팅 하고 먹을 수 있게 바꾸기 
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
                    
                    if (col is null == false && col.TryGetComponent(out IPickUpItem item))
                    {
                        item.PickUpItem(_entity);
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, GetCollectRadius);
        }
    }
}
