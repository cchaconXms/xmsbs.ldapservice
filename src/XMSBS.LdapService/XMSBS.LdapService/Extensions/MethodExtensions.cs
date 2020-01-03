using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace XMSBS.LdapService.Extensions
{
    public static class MethodExtensions
    {
        public static void AddLdapService(this IServiceCollection services)
            => services.AddTransient<Interface.ILdapService, Service.LdapService>();
    }
}
