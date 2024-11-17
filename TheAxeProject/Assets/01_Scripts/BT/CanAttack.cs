using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace MK.BT
{
    public class CanAttack : Conditional
    {
        public SharedBool isAttack;

        public override TaskStatus OnUpdate()
        {
            if (isAttack.Value == false)
                return TaskStatus.Failure;

            return TaskStatus.Success;
        }
    }
}
