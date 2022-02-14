
namespace PL_Lab05
{
    public abstract class Node
    {
        public Node left, right;
        protected readonly string uniqueId;
        static private int nextNodeId = 0;
        protected Node(string x)
        {
            left = null;
            right = null;
            uniqueId = (x + " " + nextNodeId.ToString()).ToString();
            nextNodeId++;
        }
        abstract public Node Clone();
        abstract public double Evaluate();
    }
}