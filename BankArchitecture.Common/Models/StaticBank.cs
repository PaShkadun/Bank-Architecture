using System.Collections.Generic;

namespace BankArchitecture.Common.Models
{
    public static class StaticBank
    {
        public static string Name;

        public static int Balance { get; set; }

        public static List<Account> Accounts { get; set; }
    }
}
