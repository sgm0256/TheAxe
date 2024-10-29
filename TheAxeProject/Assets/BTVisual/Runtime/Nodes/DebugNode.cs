using UnityEngine;

namespace BTVisual
{
    public class DebugNode : ActionNode
    {
        public string message;

        public override void OnStart()
        {
            Debug.Log($"OnStart : {message}");
        }

        public override State OnUpdate()
        {
            Debug.Log($"OnUpdate : {message}");
            return State.SUCCESS;
        }

        public override void OnEnd()
        {
            Debug.Log($"OnEnd : {message}");
        }
    }
}
