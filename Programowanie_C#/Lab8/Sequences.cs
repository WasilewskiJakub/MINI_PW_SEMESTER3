using System;
using System.Collections;

namespace Lab8
{
    class Naturals : IEnumerable
    {
        private int start;
        public Naturals(int x = 0)
        {
            this.start = x;
        }
        public IEnumerator GetEnumerator()
        {
            int i = start;
           while(true)
            {
                yield return i++;
            }
        }
    }
    class RandomNumbers : IEnumerable
    {
        private int seed;
        private int max;

        public RandomNumbers(int mx, int sd)
        {
            this.seed = sd;
            this.max = mx;
        }

        public IEnumerator GetEnumerator()
        {
            Random rd = new Random(this.seed);
           while(true)
            {
                yield return rd.Next(this.max);
            }
        }
    }
    class Tribonacci: IEnumerable
    {
        private int t0, t1, t2;
        public Tribonacci()
        {
            t0 = t1 = 0;
            t2 = 1;
        }

        public IEnumerator GetEnumerator()
        {
            for(int i = 0; ; i++)
            {
                if (i < 2)
                    yield return 0;
                if (i == 2)
                    yield return 1;
                if(i>2)
                {
                    int x = t0 + t1 + t2;
                    t0 = t1;
                    t1 = t2;
                    t2 = x;
                    yield return x;
                }
            }
        }
    }
    class Catalan : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            int c0 = 1;
            for(int i = 0; ; i++)
            {
                c0 = c0 * 2 * (2 * i + 1) / (i + 2);
                yield return c0;    
            }
        }
    }
    class Polynomial : IEnumerable
    {
        private int[] wsk;
        public Polynomial(int[] tab)
        {
            wsk = new int[tab.Length];
            for (int i = 0; i < tab.Length; i++)
                wsk[i] = tab[i];
        }
        private int Getval(int x)
        {
            int y = 0;
            for (int i = 0; i < wsk.Length; i++)
            {
                int val = wsk[i];
                for (int j = 0; j < i; j++)
                {
                    val *= x;
                }
                y += val;
            }
            return y;
        }
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; ; i++)
            {
                yield return Getval(i);
            }
        }
    }
}
