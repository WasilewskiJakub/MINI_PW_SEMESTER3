using System.Diagnostics;

namespace PL_Lab05
{
    class ValueNode : Node
    {
        private double value;
        private static int nextNodeId = 0;
        public ValueNode(double val = 0) : base(("Value-Node-Id-" + nextNodeId.ToString()).ToString())
        {
            value = val;
            ValueNode.nextNodeId++;
        }
        private ValueNode(ValueNode x) : this(x.value)
        {
        }
        public override string ToString()
        {
            return $"ID:{uniqueId}\nValue{value}\n";

        }
        public override Node Clone()
        {
            return new ValueNode(this);
        }

        public override double Evaluate()
        {
            return this.value;
        }
        public void SetValue(double value)
        {
            this.value = value;
        }
        public static ValueNode[] Sort(ValueNode[] tab)
        {
            for(int i = 0; i <tab.Length; i++)
            {
                for(int j = 0; j <tab.Length-i-1; j++)
                {
                    if (tab[j].value > tab[j + 1].value)
                        (tab[j], tab[j + 1]) = (tab[j + 1], tab[j]);
                }
            }
            return tab;
        }
    }
}