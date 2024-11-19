using Core.StatSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Entities
{
    public class EntityHealth : MonoBehaviour, IEntityComponent
    {
        public UnityEvent DeadEvent;
        public UnityEvent HitEvent;
        
        private Entity _entity;
        private EntityStat _stat;
        private float _currentHp;
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
            _stat = entity.GetCompo<EntityStat>();
        }

        private void Start()
        {
            _currentHp = _stat.HpStat.Value;
            _stat.HpStat.OnValueChange += HandleHPChange;
        }

        private void OnDestroy()
        {
            _stat.HpStat.OnValueChange -= HandleHPChange;
        }

        private void Update()
        {
            DeadCheck();
        }

        private void HandleHPChange(StatSO stat, float current, float previous)
        {
            _currentHp = current;
        }

        public void ApplyDamage(float damage, Entity dealer)
        {
            Debug.Log($"damage: {damage}");

            _stat.IncreaseBaseValue(_stat.HpStat, damage);
            HitEvent?.Invoke();
            DeadCheck();
        }

        private void DeadCheck()
        {
            if (_currentHp <= 0)
            {
                DeadEvent?.Invoke();
                // TODO : Pool로 바꾸기
                Destroy(gameObject);
            }
        }
    }
}
