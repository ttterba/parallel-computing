using System;
using System.Globalization;

namespace LR1
{
    class Program
    {
        delegate void MyFirstDelegate(int pParam);
        delegate int Summ(int a, int v);
        delegate string ConverterFromFloat(float D);
        delegate string GetAString();


        static void Main(string[] args)
        {

            string clientTicketCode = "e2vpzu843qrrzrvq4";
            string victoryCombination = "qrrz";


            bool hasVictoryFunc() =>
                clientTicketCode.Contains(victoryCombination);


            Action<Func<bool>, double, double> printResult = (checkFunc, bet, coef) =>
            {
                string result = "";
                if (checkFunc())
                {
                    result = "Победа. Выигрыш составил: " + (bet * coef).ToString();
                } else
                {
                    result = "Поражение.";
                }

                Console.WriteLine(result);
            };

            printResult(hasVictoryFunc, 10000.0, 1.18);
        }
    }
}