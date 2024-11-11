using System;
using UnityEngine;

namespace Core.Entities
{
    public class EntityLevel : MonoBehaviour, IEntityComponent
    {
        private Entity _entity;
        public event Action LevelUpEvent;
        
        // TODO : 아니 야발 
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
        }
        
        private void LevelUp()
        {
            LevelUpEvent?.Invoke();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            
        }
    }
}
