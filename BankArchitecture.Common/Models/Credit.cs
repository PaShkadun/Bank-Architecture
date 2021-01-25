namespace BankArchitecture.Common
{
    public class Credit
    {
        public int MonthesOfDebt { get; set; }

        public int Monthes { get; set; }

        public int MonthlySum { get; set; }

        public Credit(double money, int monthes)
        {
            Monthes = monthes;
            MonthlySum = (int)(money / monthes);
        }
    }
}
