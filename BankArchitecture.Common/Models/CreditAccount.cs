using System.Collections.Generic;

namespace BankArchitecture.Common.Models
{
    public class CreditAccount : Account
    {
        public List<Credit> Credits { get; set; }

        public CreditAccount()
        {
            Credits = new List<Credit>();
            Cards = new List<Card>();
        }
    }
}
