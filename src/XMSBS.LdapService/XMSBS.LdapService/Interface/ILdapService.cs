using XMSBS.LdapService.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace XMSBS.LdapService.Interface
{
    public interface ILdapService
    {
        LdapUser Login(string userName, string password);
        LdapUser FindUser(string userName);
    }
}
