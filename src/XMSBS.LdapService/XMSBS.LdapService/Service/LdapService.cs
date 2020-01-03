using Microsoft.Extensions.Options;
using Novell.Directory.Ldap;
using XMSBS.LdapService.Entity;
using XMSBS.LdapService.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace XMSBS.LdapService.Service
{
    public class LdapService : ILdapService
    {
        private readonly LdapConfiguration _ldapConfig;
        private readonly LdapConnection _ldapConn;

        public LdapService(IOptions<LdapConfiguration> config)
        {
            this._ldapConfig = config.Value;
            this._ldapConn = new LdapConnection()
            {
                SecureSocketLayer = this._ldapConfig.Security
            };
        }

        public LdapUser FindUser(string userName)
        {
            _ldapConn.Connect(_ldapConfig.Url, this._ldapConfig.Security ? LdapConnection.DEFAULT_SSL_PORT : LdapConnection.DEFAULT_PORT);
            _ldapConn.Bind(_ldapConfig.BindDn, _ldapConfig.BindCredentials);

            var searchFilter = string.Format(_ldapConfig.SearchFilter, userName);

            var result = _ldapConn.Search(
            _ldapConfig.SearchBase,
            LdapConnection.SCOPE_SUB,
            searchFilter,
            _ldapConfig.Attributes,
            false);

            try
            {
                var user = result.next();
                if (user != null)
                {
                    _ldapConn.Read(userName);
                    if (_ldapConn.Connected)
                    {
                        var ldapUser = new LdapUser();

                        ldapUser.UserName = userName;

                        foreach (var attr in _ldapConfig.Attributes)
                        {
                            var userAttr = user.getAttribute(attr);
                            if (userAttr != null)
                            {
                                if (!string.IsNullOrWhiteSpace(userAttr.StringValue))
                                    ldapUser.Properties.Add(attr, userAttr.StringValue);
                            }
                        }

                        return ldapUser;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Login failed.", ex);
            }

            _ldapConn.Disconnect();
            return null;
        }

        public LdapUser Login(string userName, string password)
        {
            _ldapConn.Connect(_ldapConfig.Url, this._ldapConfig.Security ? LdapConnection.DEFAULT_SSL_PORT : LdapConnection.DEFAULT_PORT);
            _ldapConn.Bind(_ldapConfig.BindDn, _ldapConfig.BindCredentials);

            var searchFilter = string.Format(_ldapConfig.SearchFilter, userName);

            var result = _ldapConn.Search(
            _ldapConfig.SearchBase,
            LdapConnection.SCOPE_SUB,
            searchFilter,
            _ldapConfig.Attributes,
            false);

            try
            {
                var user = result.next();
                if (user != null)
                {
                    _ldapConn.Bind(user.DN, password);
                    if (_ldapConn.Bound)
                    {
                        var ldapUser = new LdapUser();

                        ldapUser.UserName = userName;
                        
                        foreach (var attr in _ldapConfig.Attributes)
                        {
                            var userAttr = user.getAttribute(attr);
                            if(userAttr != null)
                            {
                                if (!string.IsNullOrWhiteSpace(userAttr.StringValue))
                                    ldapUser.Properties.Add(attr, userAttr.StringValue);
                            }                            
                        }

                        return ldapUser;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Login failed.", ex);
            }

            _ldapConn.Disconnect();
            return null;
        }
    }
}
