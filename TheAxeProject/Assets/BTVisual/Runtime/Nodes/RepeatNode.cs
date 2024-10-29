using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTVisual
{
    public class RepeatNode : DecoratorNode
    {
        public int repeatCount;
        public bool isInfinite;

        private int _currentCount;

        public override void OnStart()
        {
            _currentCount = 0;
        }

        public override State OnUpdate()
        {
            state = child.Update();

            if(state == State.SUCCESS || state == State.FAILURE)
            {
                _currentCount++;
                if (_currentCount >= repeatCount && isInfinite == false)
                    return state;
            }

            return State.RUNNING;
        }
    }
}
