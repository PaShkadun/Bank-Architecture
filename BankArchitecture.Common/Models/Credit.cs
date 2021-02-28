using System;

namespace BankArchitecture.Common
{
    public class Credit
    {
        private const int NumbersAfterPoint = 2;

        public int MonthesOfDebt { get; set; }

        public int Monthes { get; set; }

        public double MonthlySum { get; set; }

        public Credit(double money, int monthes)
        {
            Monthes = monthes;
            MonthlySum = Math.Round(money / monthes, NumbersAfterPoint);
        }
    }
}
