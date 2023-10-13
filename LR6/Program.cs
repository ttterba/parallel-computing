using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LR6
{
    internal class Program
    {
        static int[] vector1;
        static int[] vector2;
        static int[] result;


        static AutoResetEvent autoResetEvent = new AutoResetEvent(false);

        static void Main(string[] args)
        {
            int size = 5; // размер векторов

            vector1 = new int[size];
            vector2 = new int[size];
            result = new int[size];

            FillVectorsWithRandomNumbers();

            Thread thread1 = new Thread(() => CalculateVectorSum(vector1, vector2, result, 0, size / 2));
            Thread thread2 = new Thread(() => CalculateVectorSum(vector1, vector2, result, size / 2, size));

            thread1.Start();
            thread2.Start();

            autoResetEvent.WaitOne();

            PrintVector(vector1, "Vector 1:");
            PrintVector(vector2, "Vector 2:");
            PrintVector(result, "Vector sum:");

            Console.ReadKey();


        }

        static void FillVectorsWithRandomNumbers()
        {
            Random random = new Random();

            for (int i = 0; i < vector1.Length; i++)
            {
                vector1[i] = random.Next(1, 10);
                vector2[i] = random.Next(1, 10);
            }
        }

        static void CalculateVectorSum(int[] vector1, int[] vector2, int[] result, int start, int end)
        {
            for (int i = start; i < end; i++)
            {
                result[i] = vector1[i] + vector2[i];
            }

            if (Interlocked.Decrement(ref start) == 0)
            {
                autoResetEvent.Set();
            }
        }

        static void PrintVector(int[] vector, string message)
        {
            Console.WriteLine(message);

            for (int i = 0; i < vector.Length; i++)
            {
                Console.WriteLine(vector[i].ToString() + "\t");
            }

            Console.WriteLine();
        }
    }
}
