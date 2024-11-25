using ObjectPooling;
using UnityEngine;

namespace MK.BT
{
    public class RangeAttack : ChargeAttack
    {
        public PoolTypeSO _attackType;
        public float size = 3f;

        public override void OnStart()
        {
            base.OnStart();
            _attackLoad.transform.position = target.Value.position;
            _attackLoad.transform.localScale = new Vector3(size, size, 1);
        }
        
        protected override void HandleReadyToAttack()
        {
            AttackFire attack = SingletonPoolManager.Instance.Pop(PoolEnumType.RanageAttack, _attackType) as AttackFire;
            attack.transform.position = _attackLoad.transform.position;
            attack.transform.localScale = new Vector3(size, size, 1);
            _isCanAttack = true;
        }
    }
}
