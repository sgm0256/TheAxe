using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace BTVisual
{
    public class BTView : GraphView
    {
        public new class UxmlFactory : UxmlFactory<BTView, UxmlTraits> { }
        public new class UxmlTraits : GraphView.UxmlTraits { }

        public Action<NodeView> OnNodeSelected;

        private BehaviourTree _tree;

        public BTView()
        {
            Insert(0, new GridBackground());

            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
        }

        public void PopulateView(BehaviourTree tree)
        {
            _tree = tree;

            graphViewChanged -= HandleGraphViewChange;

            DeleteElements(graphElements); //니가 그래프 AddElement한게 전부 여기 들어가 있어.
            graphViewChanged += HandleGraphViewChange;

            if(_tree.rootNode == null)
            {
                _tree.rootNode = _tree.CreateNode(typeof(RootNode)) as RootNode;
                EditorUtility.SetDirty(_tree);
                AssetDatabase.SaveAssets();
            }

            tree.nodes.ForEach(n => CreateNodeView(n)); //여기서 노드를 전부 만들었잖아.

            tree.nodes.ForEach(n =>
            {
                var children = tree.GetChildren(n); //n의 자식들을 전부 가져와서
                NodeView parent = FindNodeView(n); //이 노드SO에 대한 NodeView를 가져오는 함수
                children.ForEach(childSO =>
                {
                    NodeView child = FindNodeView(childSO);
                    Edge edge = parent.output.ConnectTo(child.input);
                    AddElement(edge);
                });
            });
        }

        private NodeView FindNodeView(Node n)
        {
            return GetNodeByGuid(n.guid) as NodeView;
        }

        private GraphViewChange HandleGraphViewChange(GraphViewChange changeInfo)
        {
            if(changeInfo.elementsToRemove != null) //지워진 노드가 존재한다.
            {
                foreach(var elem in changeInfo.elementsToRemove)
                {
                    if (elem is NodeView nv)
                        _tree.DeleteNode(nv.node); //실제 SO에서도 걔를 지워주는걸 해주면 돼/

                    if(elem is Edge edge)
                    {
                        NodeView parent = edge.output.node as NodeView;
                        NodeView child = edge.input.node as NodeView;
                        _tree.RemoveChild(parent.node, child.node);
                    }
                }
            }

            if(changeInfo.edgesToCreate != null)
            {
                changeInfo.edgesToCreate.ForEach(edge =>
                {
                    NodeView parent = edge.output.node as NodeView;
                    NodeView child = edge.input.node as NodeView;

                    _tree.AddChild(parent.node, child.node);
                });
            }

            return changeInfo;
        }

        private void CreateNodeView(Node node)
        {
            NodeView nodeView = new NodeView(node);
            nodeView.OnNodeSelected = OnNodeSelected;
            AddElement(nodeView); //그래프 뷰에 자식을 추가하는 함수
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            if (_tree == null)
            {
                evt.StopPropagation();
                return;
            }
           

            CreateContextMenu<ActionNode>("Action", evt);
            CreateContextMenu<ConditionNode>("Condition", evt);
            CreateContextMenu<DecoratorNode>("Decorator", evt);
            CreateContextMenu<CompositeNode>("Composite", evt);
        }

        private void CreateContextMenu<T>(string category, ContextualMenuPopulateEvent evt)
            where T : Node
        {
            Vector2 nodePosition = this.ChangeCoordinatesTo(contentViewContainer, evt.localMousePosition);
            var types = TypeCache.GetTypesDerivedFrom<T>();
            foreach (Type type in types)
            {
                evt.menu.AppendAction(
                    $"{category}/[{type.Name}]", 
                    (a) => CreateNode(type, nodePosition));
            }
        }

        private void CreateNode(Type type, Vector2 position)
        {
            Node node = _tree.CreateNode(type);
            node.position = position;
            CreateNodeView(node);
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            //드래그가 시작된 startPort와 맞물리는 것이 가능한 노드들의 리스트를 전부 가져와서
            // 연결을 수행할지 결정하는 거야.
            // 같은 노드인지 검사하고 
            return ports.Where(endPort => endPort.direction != startPort.direction && endPort.node != startPort.node).ToList();
        }
    }
}
