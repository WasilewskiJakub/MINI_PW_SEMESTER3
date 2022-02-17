using System;
using System.Collections.Generic;
using System.Linq;

namespace LAB_2021_PL_10B
{
    //Stage 1 - 1.5 points
    public static class DictionaryExtender
    {
       public static void AddElementsFromFunction<T>(this Dictionary<int, T> dict, int n, Func<(int,T)> f )
        {
            for(int i = 0; i <n;i++)
            {
                var x = f();
                dict.Add(x.Item1, x.Item2);
            }
        }
    }

    //Stage 2 - 1 point
    public static class Generators
    {
        static int iter = 0;
        public static Func<int> NextInteger (int startingValue, int increaseValue)
        {
            return delegate() { return startingValue + iter++ * increaseValue; };
        }
        public static Func<int,T> LookUpKey<T>(IDictionary<int, T> diction, int keyIncreaseValue)
        {
            return delegate (int key)
            {
                if (diction.ContainsKey(key))
                {
                    T value = diction[key];
                    diction.Remove(key);
                    diction.Add(key + keyIncreaseValue, value);
                    return value;
                }
                diction.Add(key, default(T));
                return default(T);
            };
        }
    }

    //Stage 3 - 1 point
    public static class FunctionsManipulator
    {
        public static Func<double, double> Distance(Func<double, double> f1, Func<double, double> f2) =>
            (double x) => Math.Abs(f1(x) - f2(x));

        public static Func<double, double, double> Integral(Func<double, double> f1) =>
            (double x1, double x2) => (f1(x2) - f1(x1)) * (x2 - x1);
    }


    //Stage 4 - 1.5 points
    public static class ExtensionMethods
    {
      public static (IEnumerable<T>, IEnumerable<T>) Partition<T>(this IEnumerable<T> list, Func<T,bool> f)
        {
            List<T> @false = new List<T>();
            List<T> @true = new List<T>();
            foreach(var elem in list)
            {
                if (f(elem))
                    @true.Add(elem);
                else
                    @false.Add(elem);
            }
            return (@true, @false);
        }
        public static List<T> ReplaceIf<T>(this IEnumerable<T> lista, Func<T, bool> chooseFunc, Func<T, T> replaceFunc)
        {
            List<T> chosen = new List<T>();
            foreach(T elem in lista)
            {
                if (chooseFunc(elem))
                    chosen.Add(replaceFunc(elem));
                else
                    chosen.Add(elem);
            }
            return chosen;
        }
    }
    
}