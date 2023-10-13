using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LR4
{
    internal class Program
    {

        public delegate int RandomMatrixSumDelegate(int rowsNum, int columnsNum);

        static void Main(string[] args)
        {
            RandomMatrixSumDelegate dlMatrix = RandomMatrixSum;
            dlMatrix.BeginInvoke(5000, 6000, RandomMatrixSumCompleted, dlMatrix);

            for (int i = 0; i < 300; i++)
            {
                Console.Write(".");
                Thread.Sleep(50);
            }

            Console.ReadLine();
        }

        static void RandomMatrixSumCompleted(IAsyncResult ar)
        {
            if (ar == null) throw new ArgumentNullException("ar");
            RandomMatrixSumDelegate dl = ar.AsyncState as RandomMatrixSumDelegate;

            //Неверный тип объекта
            Trace.Assert(dl != null, "Invalid object type");

            int result = dl.EndInvoke(ar);

            Console.WriteLine("Сумма элементов матрицы случайных чисел: {0}", result);
        }

        static int RandomMatrixSum(int rowsNum, int columnsNum)
        {
            int result = 0;
            Console.WriteLine("RandomMatrixSum запущен");
            int[,] matrix = new int[rowsNum, columnsNum];
            Random rnd = new Random();
            
            //Генерация матрицы целых чисел
            for (int i = 0; i < rowsNum; i++)
            {
                for (int j = 0; j < columnsNum; j++)
                {
                    matrix[i,j] = rnd.Next(100);
                    if (rnd.NextDouble() >= 0.5)
                    {
                        matrix[i, j] = -matrix[i, j];
                    }
                }
            }

            //Подсчет суммы
            for (int i = 0; i < rowsNum; i++)
            {
                for (int j = 0; j < columnsNum; j++)
                {
                    result += matrix[i,j];
                }
            }
            Console.WriteLine("RandomMatrixSum завершен");

            return result;
        }

    }
}
