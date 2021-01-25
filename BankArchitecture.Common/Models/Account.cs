using System.Collections.Generic;

namespace BankArchitecture.Common
{
    public abstract class Account
    {
        public int Id { get; set; }

        public double Balance { get; set; }

        public List<Card> Cards { get; set; }

        public Account(int money)
        {
            Balance = money;
        }

        public Account()
        {
            
        }
    }
}
