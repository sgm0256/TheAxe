using Core.StatSystem;
using UnityEngine;

namespace Core.Entities
{
    public class EntityAttacker : MonoBehaviour, IEntityComponent
    {
        public bool IsCanAttack { get; set; } = true;

        [SerializeField] private StatSO _attackSpeedStat;
        private Entity _entity;
        private EntityStat _stat;
        
        private float _time = 0;
        private float _originAttackSpeed = 0;
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
            _stat = entity.GetCompo<EntityStat>();
        }
        
        private void Start()
        {
            // 초당 공격 속도
            _originAttackSpeed = 1 / _stat.GetStat(_attackSpeedStat).Value;
            _stat.GetStat(_attackSpeedStat).OnValueChange += HandleAttackSpeedChange;
            _time = _originAttackSpeed;
        }

        private void HandleAttackSpeedChange(StatSO stat, float current, float previous)
        {
            _originAttackSpeed = 1 / current;
        }

        private void OnDestroy()
        {
            _stat.GetStat(_attackSpeedStat).OnValueChange-= HandleAttackSpeedChange;
        }

        private void Update()
        {
            if (IsCanAttack == false)
            {
                _time -= Time.deltaTime;

                if (_time <= 0)
                {
                    _time = _originAttackSpeed;
                    IsCanAttack = true;
                }
            }
        }
    }
}
