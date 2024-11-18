using System;
using Core.InteractiveObjects;
using UnityEngine;

namespace Core.Entities
{
    public class EntityLevel : MonoBehaviour, IEntityComponent, IAfterInitable
    {
        public event Action<int> LevelUpEvent;

        [SerializeField] private float _levelUpNeedExp = 5f;
        
        private Entity _entity;
        private EntityCollector _collector;
        private int _level = 0;
        private float _expValue = 0f;
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
        }
        
        public void AfterInit()
        {
            _collector = _entity.GetCompo<EntityCollector>();
            _collector.GetObjectEvent += HandleGetObject;
        }

        private void HandleGetObject(InteractiveObjectInfoSO info)
        {
            if (info == null)
            {
                Debug.LogError("Info is Null");
                return;
            }
            // TODO : 여기서 먹은 거 정보 받아서 처리
            // TODO : 근데 이러면 아이템 따로 코인 따로 해야하는 불상도 발생 할 수 있기에 바꿔야 함
            
            if (info.Type == InteractiveType.Exp)
            {
                ExpUp(info.BaseValue);
            }
        }

        private void ExpUp(float exp)
        {
            _expValue += exp;

            if (_expValue >= _levelUpNeedExp)
            {
                _expValue -= _levelUpNeedExp;
                LevelUp();
            }
        }
        
        private void LevelUp()
        {
            _level++;
            _levelUpNeedExp += (_levelUpNeedExp / 2);
            Debug.Log("Level Up!!!");
            LevelUpEvent?.Invoke(_level);
        }
    }
}
