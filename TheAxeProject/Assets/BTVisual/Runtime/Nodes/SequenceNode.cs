using System.Collections.Generic;
using System.Linq;

namespace BTVisual
{

    public class SequenceNode : CompositeNode
    {
        public bool canAbort;
        private int _current;

        private List<Node> _conditionList;

        public override void OnAwake()
        {
            _conditionList = children.Where(x => x as ConditionNode != null).ToList();
        }

        public override void OnStart()
        {
            _current = 0;
        }

        public override State OnUpdate()
        {
            if (canAbort)
            {
                foreach (Node n in _conditionList)
                {
                    State state = n.Update();
                    if(state == State.FAILURE)
                    {
                        return State.FAILURE;
                    }
                }
            }

            var child = children[_current];
            switch (child.Update())
            {
                case State.RUNNING:
                    return State.RUNNING;
                case State.FAILURE:
                    return State.FAILURE;
                case State.SUCCESS:
                    _current++;
                    break;
            }


            return _current == children.Count ? State.SUCCESS : State.RUNNING;
        }
    }
}

