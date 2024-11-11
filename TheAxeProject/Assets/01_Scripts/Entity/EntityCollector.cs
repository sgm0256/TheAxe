using Core.ExpSystem;
using UnityEngine;

namespace Core.Entities
{
    public class EntityCollector : MonoBehaviour, IEntityComponent
    {
        private Entity _entity;
        
        [SerializeField] private LayerMask _whatIsExp;
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
                _whatIsExp);

            if (hit > 0)
            {
                for (int i = 0; i < hit; i++)
                {
                    Collider2D col = _colliderArr[i];
                    
                    if (col is null == false && col.TryGetComponent(out Exp exp))
                    {
                        exp.PickUpItem(_entity);
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
