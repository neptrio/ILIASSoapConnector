# ILIASSoapConnector

Library for using the ILIAS-Soap endpoint in C# projects.

A code generator in Visual Studio is available for SOAP enpoints. If you don't want to use it or need more control, you can use this abstraction.

Installation NuGet:
```
PM > Install-Package ILIASSoapConnector
```

Usage:
```csharp
var connector = new ILSoapConnector("https://your-ilias-installation.com/webservice/soap/server.php","client-id", "soap-account-username", "password");
var ilUser = await connector.GetUserAsync(ilUserId);
```

