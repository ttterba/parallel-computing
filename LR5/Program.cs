using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LR5
{
    internal class Program
    {

        static int[,] matrix = new int[3, 3];

        static ManualResetEvent manualResetEvent = new ManualResetEvent(false);

        static void Main(string[] args)
        {
            FillMatrixWithRandomNumbers();

            PrintMatrix(matrix);

            Thread thread = new Thread(new ThreadStart(TransformMatrix));

            Console.WriteLine("Transforming matrix asynchronously...");

            thread.Start();

            manualResetEvent.WaitOne();

            Console.WriteLine("Matrix transformation completed.");

            PrintMatrix(matrix);

            Console.ReadKey();
        }

        static void FillMatrixWithRandomNumbers()
        {
            Random random = new Random();
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = random.Next(1, 10);
                }
            }
        }

        static void TransformMatrix()
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (matrix[i, j] % 2 == 0)
                    {
                        matrix[i, j] = (int)Math.Tan(matrix[i, j]);
                    }
                }
            }

            manualResetEvent.Set();
        }

        static void PrintMatrix(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(matrix[i, j].ToString() + "\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
