using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace MK.BT
{
    public class RushAttack : Action
    {
        public SharedEnemy enemy;
        public float power;

        private Rigidbody2D _myRigid;
        
        public override void OnStart()
        {
            base.OnStart();
            
            _myRigid = enemy.Value.MyRigid;
            
            Debug.Log("Attack!!!");
            // 공격

            _myRigid.AddRelativeForce(Vector3.right * power, ForceMode2D.Impulse);
        }
    }
}
