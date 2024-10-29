using BehaviorDesigner.Runtime;
using MK.Enemy;

public class SharedEnemy : SharedVariable<Enemy>
{
    public static implicit operator SharedEnemy(Enemy enemy)
    {
        return new SharedEnemy { Value = enemy };
    }
}
