
namespace PL_Lab05
{
    public enum ArithmOperation
    {
        Addition = 0,
        Subtraction = 1,
        Multiplication = 2,
        Division = 3
    }
    class ArithmNode : Node
    {
        private ArithmOperation operation;
        public ArithmNode(ArithmOperation x):base($"{x}-Node-Id")
        {
            operation = x;
        }
        public ArithmNode((ArithmOperation operation, Node left, Node right) x) :base($"{x.operation}-Node-Id")
        {
            this.operation = x.operation;
            left = x.left;
            right = x.right;
        }
        private ArithmNode(ArithmNode x) : this(x.operation)
        {
            left = x.left == null ? null : x.left.Clone();
            right = x.right == null ? null : x.right.Clone();
        }
        public override string ToString()
        {
            return $"Id:{base.uniqueId}\nOperation:{this.operation}\n";
        }
        public override Node Clone()
        {
            return new ArithmNode(this);
        }

        public override double Evaluate()
        {
            if (this.left == null || this.right == null) return 0;
            switch(this.operation)
            {
                case ArithmOperation.Addition:
                    return this.left.Evaluate() + this.right.Evaluate();
                case ArithmOperation.Division:
                    if (this.right.Evaluate() == 0 || this.left.Evaluate() == 0)
                        return 0;
                    return this.left.Evaluate() / this.right.Evaluate();
                case ArithmOperation.Multiplication:
                    return this.left.Evaluate() * this.right.Evaluate();
                case ArithmOperation.Subtraction:
                    return this.left.Evaluate() - this.right.Evaluate();
                default:
                    return 0;
            }
        }

    }
}