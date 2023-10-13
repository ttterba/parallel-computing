using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LR7
{
    class RandomMatrix
    {
        private int[,] matrix;

        public RandomMatrix(int rows, int cols)
        {
            matrix = new int[rows, cols];
            Random rand = new Random();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = rand.Next(100);
                }
            }
        }

        public int CountOddElements()
        {
            int oddCount = 0;
            ThreadPool.QueueUserWorkItem((state) =>
            {
                int startRow = (int)state;
                int endRow = startRow + matrix.GetLength(0) / Environment.ProcessorCount;
                for (int i = startRow; i < endRow; i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (matrix[i, j] % 2 == 1)
                        {
                            Interlocked.Increment(ref oddCount);
                        }
                    }
                }
            });
            return oddCount;
        }
    }

    class Program
    {
        static void Main()
        {
            RandomMatrix matrix = new RandomMatrix(100, 100);
            int oddCount = matrix.CountOddElements();
            Console.WriteLine("Odd elements count: {0}", oddCount);
        }
    }
}
