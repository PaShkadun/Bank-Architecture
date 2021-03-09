using System.Collections.Generic;

namespace BankArchitecture.Common.Models
{
    public class CreditCard : Card
    {
        public List<Credit> Credits { get; set; }

        public CreditCard()
        {
            Credits = new List<Credit>();
        }
    }
}
