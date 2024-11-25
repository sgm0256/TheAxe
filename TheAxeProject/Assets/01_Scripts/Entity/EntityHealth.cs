using System;
using Core.StatSystem;
using ObjectPooling;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Entities
{
    public class EntityHealth : MonoBehaviour, IEntityComponent
    {
        public PoolTypeSO _popupType;
        public UnityEvent OnDeadEvent;
        public UnityEvent OnHitEvent;
        public bool IsDead => _isDead;
        
        private Entity _entity;
        private EntityStat _stat;
        private float _currentHp;
        private bool _isDead = false;
        
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

        private void OnEnable()
        {
            _isDead = false;
            _currentHp = _stat.HpStat.Value;
        }

        private void OnDestroy()
        {
            _stat.HpStat.OnValueChange -= HandleHPChange;
            OnDeadEvent.RemoveAllListeners();
            OnHitEvent.RemoveAllListeners();
        }

        private void HandleHPChange(StatSO stat, float current, float previous)
        {
            _currentHp = current;
        }

        public void ApplyDamage(float damage, Entity dealer = default)
        {
            _currentHp -= damage;
            HitPopDamage(damage);
            OnHitEvent?.Invoke();
            DeadCheck();
        }
        
        private void DeadCheck()
        {
            if (_isDead) return;
            
            if (_currentHp <= 0)
            {
                _isDead = true;
                GameManager.Instance.CurrentEnemyKillCount++;
                OnDeadEvent?.Invoke();
            }
        }

        public void HitPopDamage(float damage)
        {
            PopUpText popUp = SingletonPoolManager.Instance.Pop(PoolEnumType.InteractiveObject, _popupType) as PopUpText;
            popUp.StartPopUp(((int)damage).ToString(), transform.position);
        }
    }
}
