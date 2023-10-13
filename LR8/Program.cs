using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR8
{
    class Program
    {
        static int[,] GenerateRandomMatrix(int rows, int cols)
        {
            int[,] matrix = new int[rows, cols];
            Random rand = new Random();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = rand.Next(100);
                }
            }
            return matrix;
        }

        static int FindMinValue(int[,] matrix)
        {
            int minVal = int.MaxValue;
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            Parallel.For(0, rows, (i, state) =>
            {
                for (int j = 0; j < cols; j++)
                {
                    int curVal = matrix[i, j];
                    if (curVal < minVal)
                    {
                        minVal = curVal;
                        if (minVal == 0)
                        {
                            state.Stop();
                            return;
                        }
                    }
                }
            });
            return minVal;
        }

        static void Main(string[] args)
        {
            int[,] matrix = GenerateRandomMatrix(10, 10);
            Task<int> task = Task.Run(() => FindMinValue(matrix));
            Console.WriteLine("Min value: {0}", task.Result);

            Console.ReadLine();
        }
    }
}
