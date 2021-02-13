using System.Collections.Generic;

namespace BankArchitecture.Common
{
    public class DebitAccount : Account
    {
        public DebitAccount()
        {
            Cards = new List<Card>();
        }
    }
}
