using Core.Entities;
using UnityEngine;

namespace Core.InteractiveItem
{
    public class Item : MonoBehaviour, IPickUpItem
    {
        private Entity _entity;
        
        // TODO : 아이템 정보 추가
        
        public void PickUpItem(Entity entity)
        {
            _entity = entity;
        }
    }
}
