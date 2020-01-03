# XMSBS.LdapService

## Configuración

Se debe definir en el **appsettings.json** usando la siguiente estructura:

```json 
"LdapConfiguration": {
    "Url": "",
    "Security": false,
    "BindDn": "Dn=Dnuser",
    "BindCredentials": "pwdDnUser",
    "SearchBase": "DC=AC,DC=LOCAL",
    "SearchFilter": "(&(objectClass=user)(objectClass=person)(sAMAccountName={0}))",
    "Attributes": ["memberOf", "displayName", "sAMAccountName", "userPrincipalName"]
 }
  ```
  
Se debe definir la configuracion en el metodo **ConfigureServices** de la clase Startup.cs

```c#
services.Configure<LdapConfiguration>(Configuration.GetSection("LdapConfiguration"));
```

Ademas agregar el metodo de extension que permitira usar el servicio en el contexto de la app
```c#
services.AddLdapService();
```

## Uso de depedencia

```c#
public class AccountController : Controller
{
        private readonly ILdapService _ldapService;

        public AccountController(ILdapService ldapService)
        {
            this._ldapService = ldapService;
        }
}
```
