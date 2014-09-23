cas-sample-dotnet-webapp
========================

Example CASified .NET Web Application

## Requirements

* Internet Information Services: Make sure all "Application Development Features" are installed.
![](http://i.imgur.com/5ek9gdQ.png)
* .NET Framework v4
* Preferably, Visual Studio 2013 Professional Edition
* Forms-based Authentication must be enabled

This example was developed on Windows 8.1 64-bit with IIS v8.

## Overview 
All configuration is hosted inside the `Web.config` file. 

### Authentication
A custom `casClientConfig` node describes authentication details for CAS SSO:

```xml
<casClientConfig casServerLoginUrl="http://jasigcas.herokuapp.com/login"
                 casServerUrlPrefix="http://jasigcas.herokuapp.com"
                 serverName="http://mmoayyed.machine.net/sampleCAS/"
                 notAuthorizedUrl="~/NotAuthorized.aspx"
                 cookiesRequiredUrl="~/CookiesRequired.aspx"
                 redirectAfterValidation="true"
                 gateway="false" renew="false"
                 singleSignOut="false"
                 ticketTimeTolerance="5000"
                 ticketValidatorName="Cas10"
                 proxyTicketManager="CacheProxyTicketManager"
                 serviceTicketManager="CacheServiceTicketManager"
                 gatewayStatusCookieName="CasGatewayStatus"/>
```

* Note the `ticketValidatorName` is set to `Cas10`. If your CAS server supports newer CAS protocols, you could change this value to be `Cas20`.
* `serverName` should the url address of where this app is going to be hosted. By default, the sample app is deployed under IIS Default Website under a virtual name `sampleCAS`. No additional changes are required.
* `casServerLoginUrl` and `casServerUrlPrefix` are the url addresses of the cas login endpoint as well as the cas server address itself.

![](http://i.imgur.com/G77r0xh.png)

Forms-based authentication must also be enabled:

```xml
<authentication mode="Forms">
      <forms loginUrl="http://jasigcas.herokuapp.com/login" timeout="90" defaultUrl="~/Default.aspx" cookieless="UseCookies" slidingExpiration="true"/>
</authentication>
```

Make sure the login URLs match.

Note that all urls in this example are behind HTTP and not HTTPS. You will need to adjust accordingly per your own environment.

### Credentials
The CAS server url that is used in this example allows the following credentials:

* user id: casuser
* password: Mellon (case sensitive)


### Authorization
Authorization is handled by the .NET `roleManager` with an implementation provided by the .NET CAS Client:

```xml
<roleManager enabled="true" defaultProvider="AspNetReadOnlyXmlRoleProvider">
      <providers>
        <add name="AspNetReadOnlyXmlRoleProvider" type="DotNetCasClient.Security.ReadOnlyXmlRoleProvider" xmlFileName="~/App_Data/UserRoles.xml"/>
      </providers>
</roleManager>
```

Roles are specified in the `xmlFileName` location and are outputted back to the page when both AuthN and AuthZ are successful.

![](http://i.imgur.com/ImSwfCt.png)

