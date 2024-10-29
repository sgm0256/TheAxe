using System.Collections.Generic;

namespace BTVisual
{
    public abstract class CompositeNode : Node
    {
        public List<Node> children = new List<Node>();

        public override Node Clone()
        {
            CompositeNode node = Instantiate(this);
            node.children = children.ConvertAll(child => child.Clone());
            return node;
        }
    }
}
