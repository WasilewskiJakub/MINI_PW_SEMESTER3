using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;


namespace Lab9B
{
    interface IMyDictionary<TKey, TValue>
    {
        int Count { get; }
        void Add(TKey key, TValue value);
        bool Contains(TKey key);
        bool TryGetValue(TKey key, out TValue value);
        bool Remove(TKey key);
    }
    public class MyDictionary<TKey, TValue> : IMyDictionary<TKey, TValue>, IEnumerable<(TKey, TValue, int)>
        where TKey: struct
    {
        public class Node
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public int Counter { get; set; }
            public Node(TKey key, TValue val)
            {
                this.Key = key;
                this.Value = val;
                this.Counter = 0;
            }
        }
        public Node[] elements;
        int count;
        public MyDictionary()
        {
            this.elements = new Node[4];
            this.count = 0;
        }
        public int Count { get { return count; } }

        public void Add(TKey key, TValue value)
        {
            int idx = Search(key);
            if(idx!= -1)
            {
                this.elements[idx].Value = value;
                Fixposition(idx);
                return;
            }
            if(this.elements.Length == count)
            {
                Array.Resize(ref this.elements, this.elements.Length * 2);
                this.elements[this.count++] = new Node(key, value);
                return;
            }
            this.elements[this.count++] = new Node(key, value);
        }

        public bool Contains(TKey key)
        {
            int idx = Search(key);
            if(idx != -1) Fixposition(idx);
            return idx == -1 ? false : true;
        }
        public int Search(TKey key)
        {
            if (this.count == 0) return -1;
            for(int i = 0; i <this.count;i++)
            {
                if (this.elements[i].Key.Equals(key))
                    return i;
            }
            return -1;
        }
        public bool Remove(TKey key)
        {
            int idx = Search(key);
            if (idx == -1) return false;
            for (int i = idx; i < this.count - 1; i++)
            {
                this.elements[i] = this.elements[i + 1];
            }
            this.count--;
            return true;

        }
        int Fixposition(int idx)
        {
            this.elements[idx].Counter += 1;
            int k = idx;
            for (int i = idx; i > 0; i--)
                if (this.elements[i - 1].Counter < this.elements[i].Counter)
                {
                    (this.elements[i - 1], this.elements[i]) = (this.elements[i], this.elements[i - 1]);
                    k = i - 1;
                }
            return k;
        }
        public bool TryGetValue(TKey key, out TValue value)
        {
            int idx = Search(key);
            if(idx==-1)
            {
                value = default(TValue);
                return false;
            }
            else
            {
                value = this.elements[idx].Value;
                Fixposition(idx);
                return true;

            }
        }
        public override string ToString()
        {
            string ret = "";
            for (int i = 0; i < this.count; i++)
            {
                ret += $"[{elements[i].Key}:{elements[i].Value}({elements[i].Counter})] ";
            }
            return ret;
        }

        public IEnumerator<(TKey, TValue, int)> GetEnumerator()
        {
            for (int i = 0; i < this.count; i++)
                yield return (elements[i].Key, elements[i].Value, elements[i].Counter);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
    static class MyDictionaryExtensions
    {
        public static TValue[] GetValues<TKey, TValue>(this MyDictionary<TKey, TValue> dictionary)
            where TKey :struct
        {
            TValue[] tab = new TValue[0];
            int idx = 0;
            for(int i = 0; i<dictionary.Count;i++)
            {
                bool add = true;
                for(int j = 0; j < tab.Length;j++)
                    if(dictionary.elements[i].Value.Equals(tab[j]))
                    {
                        add = false;
                    }
                if(add)
                {
                    Array.Resize(ref tab, tab.Length + 1);
                    tab[idx++] = dictionary.elements[i].Value;
                }
                
            }
            return tab;
        }
        public static TValue MinValue<TKey, TValue>(this MyDictionary<TKey, TValue> dictionary)
            where TKey:struct
            where TValue : IComparable
        {
            TValue min = dictionary.elements[0].Value;
            for(int i = 1;i<dictionary.Count;i++)
            {
                if (dictionary.elements[i].Value.CompareTo(min) < 0)
                    min = dictionary.elements[i].Value;
            }
            return min;
        }
    }
}
