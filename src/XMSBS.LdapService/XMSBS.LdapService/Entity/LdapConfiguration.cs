using System;
using System.Collections.Generic;
using System.Text;

namespace XMSBS.LdapService.Entity
{
    public class LdapConfiguration
    {
        public string Url { get; set; }
        public bool Security { get; set; }
        public string BindDn { get; set; }
        public string BindCredentials { get; set; }
        public string SearchBase { get; set; }
        public string SearchFilter { get; set; }
        public string[] Attributes { get; set; }
    }
}
