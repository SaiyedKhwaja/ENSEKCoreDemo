using System;
using System.Collections.Generic;
using System.Text;

namespace ClientLanguage.Models
{
    public class Account
    {
        public string Title = "Accounts";
        public string AccountId = "Account Id";
        public string FirstName = "First Name";
        public string LastName = "Last Name";
        public string MySettings = "My Settings";
        public string UpdateAccount = "Update Account Details";

        private static Account _instance = new Account();
        public static Account Instance
        {
            get { return _instance; }
        }
    }
}
