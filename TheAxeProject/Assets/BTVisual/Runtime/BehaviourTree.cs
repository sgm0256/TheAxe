using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BTVisual
{
    [CreateAssetMenu(menuName = "BTVisual/Tree")]
    public class BehaviourTree : ScriptableObject
    {
        public Node rootNode;
        public State treeState = State.RUNNING;

        public List<Node> nodes = new List<Node>();

        public State Update()
        {
            if(rootNode.state == State.RUNNING)
            {
                treeState = rootNode.Update();
            }
            return treeState;
        }

        public Node CreateNode(Type type)
        {
            Node node = ScriptableObject.CreateInstance(type) as Node;
            node.name = type.Name;
            node.guid = GUID.Generate().ToString();
            nodes.Add(node);

            AssetDatabase.AddObjectToAsset(node, this);
            AssetDatabase.SaveAssets();
            return node;
        }

        public void DeleteNode(Node node)
        {
            nodes.Remove(node); //리스트에서 제거하고
            AssetDatabase.RemoveObjectFromAsset(node);
            AssetDatabase.SaveAssets();
        }

        public void AddChild(Node parent, Node child)
        {
            if(parent is DecoratorNode decorator)
            {
                decorator.child = child;
                return;
            }

            if(parent is CompositeNode composite)
            {
                composite.children.Add(child);
                return;
            }

            if(parent is RootNode root)
            {
                root.child = child;
                return;
            }
        }

        public void RemoveChild(Node parent, Node child)
        {
            if(parent is DecoratorNode decorator)
            {
                decorator.child = null;
                return;
            }

            if(parent is CompositeNode composite)
            {
                composite.children.Remove(child);
                return;
            }

            if(parent is RootNode root)
            {
                root.child = null;
                return;
            }
        }

        public List<Node> GetChildren(Node parent)
        {
            List<Node> children = new List<Node>();

            if (parent is CompositeNode composite)
                return composite.children;

            DecoratorNode decorator = parent as DecoratorNode;
            if (decorator != null && decorator.child != null)
                children.Add(decorator.child);

            RootNode root = parent as RootNode;
            if (root != null && root.child != null)
                children.Add(root.child);

            return children;
        }

        public BehaviourTree Clone()
        {
            BehaviourTree tree = Instantiate(this);
            tree.rootNode = tree.rootNode.Clone(); //자식까지 전파되면서 클론되어 들어온다.

            tree.nodes = new List<Node>();
            Traverse(tree.rootNode, node => tree.nodes.Add(node));
            return tree;
        }

        public void Traverse(Node node, Action<Node> VisitorCallback)
        {
            if(node != null)
            {
                VisitorCallback?.Invoke(node);
                var children = GetChildren(node);
                children.ForEach(child => Traverse(child, VisitorCallback));
            }
        }
    }
}
