using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LR2
{
    internal class Program
    {

        public delegate int CountEqualNumbersDelegate(int sizeA, int sizeB);

        static int CountEqualNumbers(int sizeA, int sizeB)
        {
            Console.WriteLine("Генерация массивов и поиск одинаковых элементов запущены.");
            int[] arr1 = new int[sizeA];
            int[] arr2 = new int[sizeB];
            List<int> commonNumbers = new List<int>();

            int min = 1;
            int max = 100000;

            Random rnd = new Random();

            for (int i = 0; i < sizeA; i++)
            {
                arr1[i] = rnd.Next(min, max);
            }

            for (int i = 0; i < sizeB; i++)
            {
                arr2[i] = rnd.Next(min, max);
            }


            for (int i = 0; i < sizeA; i++)
            {
                int elem = arr1[i];
                for (int j = 0; j < sizeB; j++)
                {
                    if (elem == arr2[j])
                    {
                        if (!commonNumbers.Contains(elem))
                        {
                            commonNumbers.Add(elem);
                        }
                    }
                }
            }

            Console.WriteLine("\nПоиск завершен.");
            //Console.WriteLine("Массив 1: [{0}]", string.Join(", ", arr1));
            //Console.WriteLine("Массив 2: [{0}]", string.Join(", ", arr2));
            return commonNumbers.Count;
        }


        static void Main(string[] args)
        {
            CountEqualNumbersDelegate dlCount = CountEqualNumbers;

            IAsyncResult arCount = dlCount.BeginInvoke(30000, 30000, null, null);

            while (!arCount.IsCompleted)
            {
                Console.Write(".");
                Thread.Sleep(50);
            }

            int result = dlCount.EndInvoke(arCount);
            Console.WriteLine("\nКол-во элементов, присутствующих в обоих массивах: {0}", result);

            Console.ReadLine();
        }

    }
}


