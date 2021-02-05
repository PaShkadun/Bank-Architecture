using System.Collections.Generic;

namespace BankArchitecture.Common.Models
{
    public class MainBank
    {
        public static string Name;

        public static int Balance { get; set; }

        public static List<Account> Accounts { get; set; }

        static MainBank()
        {
            Accounts = new List<Account>();
        }
    }
}
