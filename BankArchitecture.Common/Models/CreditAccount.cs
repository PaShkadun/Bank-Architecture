using System.Collections.Generic;

namespace BankArchitecture.Common
{
    public class CreditAccount : Account
    {
        public List<Credit> Credits { get; set; }

        public CreditAccount()
        {

        }
    }
}
