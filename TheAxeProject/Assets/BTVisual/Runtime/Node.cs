using UnityEngine;

namespace BTVisual
{
    public enum State
    {
        RUNNING,
        FAILURE,
        SUCCESS
    }

    public abstract class Node : ScriptableObject
    {
        [HideInInspector] public State state = State.RUNNING;
        [HideInInspector] public bool isStarted = false;
        [HideInInspector] public string guid;
        [HideInInspector] public Vector2 position;

        private void OnEnable()
        {
            OnAwake();
        }

        public State Update()
        {
            if(isStarted == false)
            {
                OnStart();
                isStarted = true;
            }

            state = OnUpdate();

            if(state == State.FAILURE || state == State.SUCCESS)
            {
                OnEnd();
                isStarted = false;
            }
            return state;
        }

        public virtual void OnAwake() {}
        public virtual void OnStart() { }
        public virtual State OnUpdate() { return State.SUCCESS; }
        public virtual void OnEnd() { }

        public virtual Node Clone()
        {
            return Instantiate(this);
        }
    }
}
