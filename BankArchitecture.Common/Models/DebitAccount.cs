using System.Collections.Generic;

namespace BankArchitecture.Common.Models
{
    public class DebitAccount : Account
    {
        public DebitAccount()
        {
            Cards = new List<Card>();
        }
    }
}
