using System.Collections.Generic;

namespace ENSEKCoreWebApp.Models
{
    public class AccountViewModel
    {
        public int AccountId { get; set; }
        public List<Account> accounts { get; set; } = new List<Account>();
    }
}
