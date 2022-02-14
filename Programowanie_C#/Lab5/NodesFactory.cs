using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Windows.Markup;
namespace PL_Lab05
{
   static class NodesFactory
    {
        public static Node CreateNode(string x)
        {
            if (x == null)
                return null;
            switch (x)
            {
                case "null":
                    return null;
                case "+":
                    return new ArithmNode(ArithmOperation.Addition);
                case "-":
                    return new ArithmNode(ArithmOperation.Subtraction);
                case "*":
                    return new ArithmNode(ArithmOperation.Multiplication);
                case "/":
                    return new ArithmNode(ArithmOperation.Division);
                default:
                    double konst = double.Parse(x,CultureInfo.InvariantCulture);
                    return new ValueNode(konst);
            }
        }
        public static Node[] CreateMultipleNodes(string[] tab)
        {
            if (tab == null)
                return null;
            Node[] x = new Node[tab.Length];
            for(int i = 0; i <tab.Length;i++)
            {
                x[i] = CreateNode(tab[i]);
            }
            return x;
        }
        public static Node CreateTree(Node[] tab)
        {
            if (tab.Length < 1)
                return null;
            int i_active = 0;
            for (int i = 1; i<tab.Length; i++)
            {
                if (tab[i_active].left == null)
                    tab[i_active].left = tab[i];
                else
                {
                    tab[i_active].right = tab[i];
                    i_active++;
                }
            }
            return tab[0];
        }
    }

}