using System;
using System.Collections.Generic;
using System.Text;

namespace XMSBS.LdapService.Entity
{
    public class LdapUser
    {
        public string UserName { get; set; }
        public Dictionary<string, string> Properties { get; set; }

        public LdapUser()
        {
            Properties = new Dictionary<string, string>();
        }
    }
}
