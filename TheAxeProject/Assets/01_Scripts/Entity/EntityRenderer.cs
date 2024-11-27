using UnityEngine;

namespace Core.Entities
{
    public class EntityRenderer : MonoBehaviour, IEntityComponent
    {
        public float FacingDirection { get; private set; } = 1;

        private Entity _entity;
        private Animator _animator;
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
            _animator = GetComponent<Animator>();
        }

        #region FlipControl

        public void Flip()
        {
            FacingDirection *= -1;
            transform.Rotate(0, 180f, 0);
        }

        public void FlipController(float xMove)
        {
            if (Mathf.Abs(FacingDirection + xMove) < 0.5f)
                Flip();
        }
        
        #endregion
    }
}