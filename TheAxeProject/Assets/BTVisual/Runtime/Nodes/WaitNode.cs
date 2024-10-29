using UnityEngine;

namespace BTVisual
{
    public class WaitNode : ActionNode
    {
        public float duration = 1f;
        private float _startTime;

        public override void OnStart()
        {
            _startTime = Time.time;
        }

        public override State OnUpdate()
        {
            if (Time.time - _startTime > duration)
            {
                return State.SUCCESS;
            }
            return State.RUNNING;
        }
    }
}
