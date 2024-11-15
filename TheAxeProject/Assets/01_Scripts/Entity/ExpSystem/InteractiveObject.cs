using System;
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
        [SerializeField] protected InteractiveOverride InfoOverride;
        public InteractiveObjectInfoSO Info { get; private set; }
        protected Entity _entity;
        protected virtual void Awake()

        {
            Info = InfoOverride.CreateInfo();
        }

        public abstract void PickUpItem(Entity entity);
    }
}
