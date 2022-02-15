using System;
using System.Collections;

namespace Lab8
{
    /// <summary>
    /// Interfejs klas modyfikujących sekwencje
    /// </summary>
    public interface IModifier
    {
        /// <summary>
        /// Nazwa modyfikatora
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Metoda modyfikuje kolekcje
        /// </summary>
        /// <param name="sequence">Sekwencja do modyfikacji</param>
        /// <returns>Zmodyfikowana sekwencja</returns>
        IEnumerable Modify(IEnumerable sequence);
    }
    class FirstN : IModifier
    {
        public string Name => "First 5 numbers";
        int n;
        public FirstN(int x)
        { this.n = x; }
        public IEnumerable Modify(IEnumerable sequence)
        {
            int i = 0;
            foreach(var seq in sequence)
            {
                if (i++ >= n) yield break;
                yield return seq;
            }
        }
    }
    class LinearTransform : IModifier
    {
        public string Name => "Linear Transform";
        private int a, b;
        public LinearTransform(int a, int b)
        {
            this.a = a;
            this.b = b;
        }
        public IEnumerable Modify(IEnumerable sequence)
        {
            foreach(var seq in sequence)
            {
                yield return a * (int)seq + b;
            }
        }
    }
    class Unique : IModifier
    {
        public string Name => "Unique: ";

        public IEnumerable Modify(IEnumerable sequence)
        {
            int? last = null;
            foreach(int seq in sequence)
            {
                if (seq != last)
                    yield return seq;
                last = seq;
            }
        }
    }
    class Prime : IModifier
    {
        public string Name => "Prime: ";

        public IEnumerable Modify(IEnumerable sequence)
        {
            foreach(int seq in sequence)
            {
                bool ret = true;
                for(int i = 2;i <= seq/2 ;i++)
                {
                    if (seq % i == 0)
                    {
                        ret = false;
                        break;
                    }
                }
                if (ret && seq >=2 ) yield return seq;
            }
        }
    }
    class ComposedModifier : IModifier
    {
        public string Name => "ComposedModifier: ";
        private IModifier[] tab;
        public ComposedModifier(IModifier[]tab)
        {
            this.tab = (IModifier[])tab.Clone();
        }
        public IEnumerable Modify(IEnumerable sequence)
        {
            foreach (var elem in tab)
            {
                sequence = elem.Modify(sequence);
            }
            return sequence;
        }
    }
}
