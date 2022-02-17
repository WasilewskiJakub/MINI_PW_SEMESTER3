﻿#define STAGE1
#define STAGE2
#define STAGE3
#define STAGE4

using System;
using System.Collections.Generic;
using System.Xml.XPath;


namespace LAB_2021_PL_10B
{
    class Program
    {
        static void Main(string[] args)
        {
            //Stage 1 - 1.5 point
#if STAGE1
            {
                Console.WriteLine("STAGE 1");
                var fib = new Dictionary<int, int>();
                //Uzupełnij
                int fib1 = 1, fib2 = 1, i = 1;
                fib.AddElementsFromFunction(10,()=>
                { 
                    if (i <= 2)
                        return (i++, 1);
                    int x = fib1 + fib2;
                    fib1 = fib2;
                    fib2 = x;
                    return (i++, x);
                });
                Console.Write("Dictionary: ");
                PrintIEnumerable(fib);

                var ints = new List<int> {10, -5, 0, 4, 6, -6, 2321, -2452, 342, -67, 2};
                //Wykorzystaj funkcję lambda
                ints.Sort((x,y) => {
                    if (Math.Abs(x) == Math.Abs(y))
                        return x > 0 ? -1 : 1;
                    return Math.Abs(x) > Math.Abs(y) ? -1 : 1;
                });
                Console.Write("Sorted list: ");
                PrintIEnumerable(ints);
                Console.WriteLine("\n\n");
            }
#endif
            //Stage 2 - 1 point
#if STAGE2
            {
                Console.WriteLine("STAGE 2");
                var randomInt = Generators.NextInteger(5, 13);
                Console.WriteLine("Next integer");
                for (int i = 0; i < 10; i++)
                {
                    Console.Write(randomInt() + ", ");
                }

                Console.WriteLine("\n\n");


                Dictionary<int, double> doubles = new Dictionary<int, double>
                {
                    {0, 0.2}, {1, 5.6f}, {2, 2}, {3, 4}, {4, -24}, {5, 16.2134124}, {6, 22}, {7, 82}, {8, 8.6},
                    {9, 12.4}
                };
                var lookUpKey = Generators.LookUpKey(doubles, 1000);
                Console.WriteLine("LookUpKey");
                for (int i = 0; i <= 10; i++)
                {
                    Console.WriteLine((i, lookUpKey(i)));
                }

                Console.WriteLine((1000, lookUpKey(1000)));
                Console.WriteLine("\n\n");
            }
#endif
            //Stage 3 - 1 point
#if STAGE3
            {
                Console.WriteLine("Stage 3");
                //f = |x-5|
                Func<double,double> f = (x) => Math.Abs(x - 5);
                //f = sin(x)*1/10*x
                Func<double, double> g = (x) => Math.Sin(x) * 1 / 10 * x;
                Console.WriteLine("Distance");
                var distance = FunctionsManipulator.Distance(f, g);
                for (int i = -5; i <= 5; i++)
                {
                    Console.WriteLine($"x: {i}, f(g(x)): {distance(i)}");
                }

                Console.WriteLine("\n\n");

                //f = x ^ 3 + x ^ 2 - 5x + 3
                f = (x) => Math.Pow(x, 3) + Math.Pow(x, 2) - 5 * x + 3;
                var integral = FunctionsManipulator.Integral(f);
                Console.WriteLine("Integral");
                for (int i = 1; i <= 10; i++)
                {
                    Console.WriteLine($"f({i}): {f(i)}, f({2 * i}): {f(2 * i)}, integral from {i} to {2 * i}: {integral(i, 2 * i)}");
                }

                Console.WriteLine("\n\n");
            }
#endif
            //Stage 4 - 1.5 points
#if STAGE4
            {
                Console.WriteLine("STAGE 4");
                Random random = new Random(0);
                Console.WriteLine("Partition extension");
                var ints = new List<int> {10, -5, 0, 4, 6, 2321, -2452, 342, -67, 2};
                PrintIEnumerable(ints);

                //Uzupełnij
                int absSum = 0;
                var tuple = ints.Partition((x)=>
                {
                    absSum += Math.Abs(x);
                    return x % 2 == 0;
                    });
                Console.WriteLine($"Even");
                PrintIEnumerable(tuple.Item1);
                Console.WriteLine($"Odd");
                PrintIEnumerable(tuple.Item2);
                Console.WriteLine($"Sum of absolute values: {absSum}");
                Console.WriteLine("\n\n");

                Console.WriteLine("ReplaceIf extension");
                var doubles = new List<double>();
                for (int i = 0; i < 10; i++)
                {
                    doubles.Add(random.NextDouble() * 20);
                }

                PrintIEnumerable(doubles);

                //Uzupełnij
                var replacedDoubles = doubles.ReplaceIf((x) => x - (int)x > 0.5,
                    (x)=>x/2 - (x - (int)x));
                PrintIEnumerable(replacedDoubles);
            }
#endif
        }

        private static void PrintIEnumerable<T>(IEnumerable<T> ints)
        {
            foreach (var i in ints)
            {
                Console.Write(i + ", ");
            }

            Console.WriteLine();
        }
    }
}