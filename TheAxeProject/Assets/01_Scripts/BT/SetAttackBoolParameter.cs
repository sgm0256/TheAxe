using BehaviorDesigner.Runtime.Tasks;
using Core.Entities;

namespace MK.BT
{
    public class SetAttackBoolParameter : Action
    {
        public SharedEnemy enemy;

        private EntityAttacker _attacker;
        
        public override void OnStart()
        {
            base.OnStart();
            _attacker = enemy.Value.GetCompo<EntityAttacker>();
            _attacker.IsCanAttack = false;
        }
    }
}
