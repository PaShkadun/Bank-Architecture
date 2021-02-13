using System.Collections.Generic;

namespace BankArchitecture.Common
{
    public abstract class Account
    {
        public string Id { get; init; }

        public double Balance { get; set; }

        public List<Card> Cards { get; set; }

        public Account()
        {
        }
    }
}
