using System.Collections.Generic;

namespace BankArchitecture.Common.Models
{
    public abstract class Account
    {
        public string Id { get; set; }

        public double Balance { get; set; }

        public List<Card> Cards { get; set; }

        public Account()
        {
        }
    }
}
