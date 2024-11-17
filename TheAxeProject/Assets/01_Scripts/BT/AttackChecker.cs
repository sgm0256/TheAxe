using BehaviorDesigner.Runtime.Tasks;
using Core.Entities;

namespace MK.BT
{
    public class AttackChecker : Conditional
    {
        public SharedEnemy enemy;
        public bool isAttacked;

        private EntityAttacker _attacker;

        public override void OnStart()
        {
            base.OnStart();
            _attacker = enemy.Value.GetCompo<EntityAttacker>();
            isAttacked = _attacker.IsCanAttack;
        }

        public override TaskStatus OnUpdate()
        {
            if (isAttacked == false)
                return TaskStatus.Failure;

            return TaskStatus.Success;
        }
    }
}
