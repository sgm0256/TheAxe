using Core.Entities;
using UnityEngine;

namespace Core.InteractiveObjects
{
    public enum InteractiveType
    {
        Exp, Item, Coin
    }
    
    public abstract class InteractiveObject : MonoBehaviour
    {
        [field: SerializeField] public InteractiveObjectInfoSO Info { get; private set; }
        protected Entity _entity;

        public abstract void PickUpItem(Entity entity);
    }
}
