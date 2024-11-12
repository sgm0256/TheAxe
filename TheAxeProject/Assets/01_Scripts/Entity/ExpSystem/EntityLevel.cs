using System;
using Core.InteractiveItem;
using UnityEngine;

namespace Core.Entities
{
    public class EntityLevel : MonoBehaviour, IEntityComponent
    {
        private Entity _entity;
        public event Action LevelUpEvent;

        private float _level = 0f;
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
        }
        
        private void LevelUp()
        {
            // TODO : Level 방식 만들기
            _level += 0.1f;
            LevelUpEvent?.Invoke();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // TODO : 이렇게 중구난방으로 여기는 콜라이더 다른 곳은 Laycast 방식 바꿔야 할 듯
            // TODO : 일단 개발하고 바꾸기

            if (other.TryGetComponent(out IPickUpItem item))
            {
                LevelUp();
            }
        }
    }
}
