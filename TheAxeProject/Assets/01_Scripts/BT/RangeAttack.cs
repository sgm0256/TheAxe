using ObjectPooling;
using UnityEngine;

namespace MK.BT
{
    public class RangeAttack : ChargeAttack
    {   
        public float size = 3f;

        public override void OnStart()
        {
            base.OnStart();
            _attackLoad.transform.position = target.Value.position;
            _attackLoad.transform.localScale = new Vector3(size, size, 1);
        }
        
        protected override void HandleReadyToAttack()
        {
            // TODO : RanageAttack 만들기
            //Transform attack = SingletonPoolManager.Instance.GetPoolManager(PoolEnumType.RanageAttack).Pop(attackLoadType) as Transform;
            _isCanAttack = true;
        }
    }
}
