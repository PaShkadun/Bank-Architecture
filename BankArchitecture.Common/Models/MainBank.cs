using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BankArchitecture.Common.Models
{
    public class MainBank
    {
        public string Name;

        public int Balance { get; set; }

        [JsonIgnore]
        public List<Account> Accounts { get; set; }

        public MainBank()
        {
            Accounts = new List<Account>();
        }
    }
}
