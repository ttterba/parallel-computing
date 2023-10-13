using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR9
{
    class Program
    {
        static void Main(string[] args)
        {
            int rows = new Random().Next(3, 6);
            int columns = new Random().Next(3, 6);
            int[][] matrix = GenerateRandomMatrix(rows, columns);

            Task<int>[] maxTasks = new Task<int>[columns];
            for (int i = 0; i < columns; i++)
            {
                int column = i;
                maxTasks[i] = Task.Factory.StartNew(() => FindMaxInColumn(matrix, column));
            }

            Task<int> minTask = Task.Factory.ContinueWhenAll(maxTasks, tasks =>
            {
                int minValue = int.MaxValue;
                foreach (Task<int> task in tasks)
                {
                    int maxValue = task.Result;
                    if (maxValue < minValue)
                    {
                        minValue = maxValue;
                    }
                }
                return minValue;
            });

            Console.WriteLine("Matrix:");
            PrintMatrix(matrix);

            for (int i = 0; i < columns; i++)
            {
                int maxValue = maxTasks[i].Result;
                Console.WriteLine($"Max value in column {i}: {maxValue}");
            }
            int minValueAll = minTask.Result;
            Console.WriteLine($"Min value in matrix: {minValueAll}");

            Console.ReadLine();
        }

        static int[][] GenerateRandomMatrix(int rows, int columns)
        {
            Random random = new Random();
            int[][] matrix = new int[rows][];
            for (int i = 0; i < rows; i++)
            {
                int[] row = new int[columns];
                for (int j = 0; j < columns; j++)
                {
                    row[j] = random.Next(1, 99);
                }
                matrix[i] = row;
            }
            return matrix;
        }

        static int FindMaxInColumn(int[][] matrix, int column)
        {
            int maxValue = int.MinValue;
            for (int i = 0; i < matrix.Length; i++)
            {
                int value = matrix[i][column];
                if (value > maxValue)
                {
                    maxValue = value;
                }
            }
            return maxValue;
        }

        static void PrintMatrix(int[][] matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    Console.Write($"{matrix[i][j],3}");
                }
                Console.WriteLine();
            }
        }
    }
}
