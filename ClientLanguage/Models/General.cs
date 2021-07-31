using System;
using System.Collections.Generic;
using System.Text;

namespace ClientLanguage.Models
{
    public class General
    {
        public string Edit = "Edit";
        public string Delete = "Delete";
        public string Update = "Update";
        public string Cancel = "Cancel";
        public string Save = "Save";

        private static General _instance = new General();
        public static General Instance
        {
            get { return _instance; }
        }
    }
}
