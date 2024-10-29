using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace BTVisual
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node
    {
        public Node node;
        public Port input;
        public Port output;
        public Action<NodeView> OnNodeSelected;

        public NodeView(Node node) : base("Assets/BTVisual/Editor/DataBind/NodeView.uxml")
        {
            this.node = node;
            this.title = node.name;

            viewDataKey = node.guid;
            style.left = node.position.x;
            style.top = node.position.y;

            CreateInputPorts();
            CreateOutputPorts();
            SetUpClasses();
        }

        private void SetUpClasses()
        {
            if (node is ActionNode)
                AddToClassList("action");
            else if (node is CompositeNode)
                AddToClassList("composite");
            else if (node is ConditionNode)
                AddToClassList("condition");
            else if (node is DecoratorNode)
                AddToClassList("decorator");
            else if (node is RootNode)
                AddToClassList("root");
        }

        private void CreateInputPorts()
        {
            if(node is ActionNode || node is CompositeNode || node is DecoratorNode || node is ConditionNode)
            {
                input = InstantiatePort(
                    Orientation.Vertical, 
                    Direction.Input, 
                    Port.Capacity.Single, 
                    typeof(bool));
            }

            if(input != null)
            {
                input.portName = "";
                inputContainer.Add(input);
            }
        }

        private void CreateOutputPorts()
        {
            if(node is ActionNode || node is ConditionNode)
            {

            }else if(node is CompositeNode)
            {
                output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Multi, typeof(bool));
            }else if(node is DecoratorNode || node is RootNode)
            {
                output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
            }

            if(output != null)
            {
                output.portName = "";
                outputContainer.Add(output);
            }
        }

        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            node.position.x = newPos.xMin;
            node.position.y = newPos.yMin;
        }

        public override void OnSelected()
        {
            base.OnSelected();
            OnNodeSelected?.Invoke(this);
        }
    }
}