using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab07B
{
    class BST
    {
        public class Node
        {
            public int Key { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }


            public Node(int key, Node left = null, Node right = null)
            {
                Key = key;
                Left = left;
                Right = right;
            }

            public override int GetHashCode()
            {
                int res = Key;
                int leftHashCode = Left != null ? Left.GetHashCode() : 0;
                int rightHashCode = Right != null ? Right.GetHashCode() : 0;
                res ^= leftHashCode;
                res ^= rightHashCode;
                return res;
            }
        }
        public void Insert(int k)
        {
            Node n = new Node(k);
            if (root == null)
            {
                root = n;
                Count++;
                return;
            }

            Node prev = null;
            Node tmp = root;
            while (tmp != null)
            {
                if (k < tmp.Key)
                {
                    prev = tmp;
                    tmp = tmp.Left;
                }
                else if (k > tmp.Key)
                {
                    prev = tmp;
                    tmp = tmp.Right;
                }
                else
                    return;
            }

            if (k < prev.Key)
            {
                prev.Left = n;
                Count++;
            }
            else
            {
                prev.Right = n;
                Count++;
            }

        }

        public bool Contains(int k)
        {
            Node n = new Node(k);
            Node tmp = root;
            while (tmp != null)
            {
                if (k < tmp.Key)
                {
                    tmp = tmp.Left;
                }
                else if (k > tmp.Key)
                {
                    tmp = tmp.Right;
                }
                else
                    return true;
            }
            return false;
        }

        private Node root;
        public Node Root { get { return root; } }
        public int Count { get; private set; }
        public bool IsEmpty { get { return Count == 0 ? true : false; } }
        public BST()
        {
            this.root = null;
            this.Count = 0;
        }
        public BST(int[] tab)
        {
            for (int i = 0; i < tab.Length; i++)
                Insert(tab[i]);
        }
        /* === Stage 2: === */

        public void Inorder(Node node, List<int> x)
        {
            if (node == null) return;
            Inorder(node.Left, x);
            x.Add(node.Key);
            Inorder(node.Right, x);
        }
        public static explicit operator int[](BST x)
        {
            List<int> list = new List<int>();
            x.Inorder(x.root, list);
            return list.ToArray();
        }
        public static implicit operator BST(int[] tab)
        {
            return new BST(tab);
        }
        public BST Clone()
        {
            int[] x = (int[])this;
            return new BST(x);
        }
        public override string ToString()
        {
            return "[" + string.Join(",", (int[])this) + "]";
        }
        public override bool Equals(object obj)
        {
            return (BST)obj == (BST)this;
        }
        public override int GetHashCode()
        {
            return root.GetHashCode();
        }
        /* === Stage 3: === */

        public static BST operator +(BST tree1, BST tree2)
        {
            List<int> tmp = new List<int>();
            int[] tab1 = ((int[])tree1);
            int[] tab2 = ((int[])tree2);
            for (int i = 0; i < tab1.Length; i++)
            {
                bool add = true;
                for (int j = 0; j < tab2.Length; j++)
                {
                    if (tab1[i] == tab2[j])
                    {
                        add = false;
                        break;
                    }
                }
                if (add) tmp.Add(tab1[i]);
            }
            for (int j = 0; j < tab2.Length; j++)
            {
                tmp.Add(tab2[j]);
            }
            return new BST(tmp.ToArray());
        }
        public static BST operator *(BST tree1, BST tree2)
        {
            int[] tab1 = ((int[])tree1);
            int[] tab2 = ((int[])tree2);
            List<int> tmp = new List<int>();
            for (int i = 0; i < tab1.Length; i++)
            {
                bool add = false;
                for (int j = 0; j < tab2.Length; j++)
                {
                    if (tab1[i] == tab2[j])
                    {
                        add = true;
                        break;
                    }
                }
                if (add) tmp.Add(tab1[i]);
            }
            return new BST(tmp.ToArray());
        }
        public static bool operator ==(BST tree1, BST tree2)
        {
            int[] tab1 = ((int[])tree1);
            int[] tab2 = ((int[])tree2);
            if (tab1.Length != tab2.Length) return false;
            for (int i = 0; i < tab1.Length; i++)
                if (tab1[i] != tab2[i]) return false;
            return true;
        }
        public static bool operator !=(BST tree1, BST tree2)
        {
            return !(tree1 == tree2);
        }
        public void Deconstruct(out BST smaller, out BST higher)
        {
            int[] tab = ((int[])this);
            List<int> small = new List<int>();
            List<int> high = new List<int>();
            for (int i = 0; i < tab.Length / 2; i++)
            {
                small.Add(tab[i]);
                if (tab.Length % 2 == 1)
                    high.Add(tab[tab.Length / 2 + i + 1]);
                high.Add(tab[tab.Length / 2 + i]);
            }
            smaller = new BST(small.ToArray());
            higher = new BST(high.ToArray());
        }
       public int this[int val]
        {
            get { int[] x = ((int[])this); return val == 1 ? x[x.Length - 1] : x[0]; }
        }
    }
}
