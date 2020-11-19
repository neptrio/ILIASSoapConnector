# ILIASSoapConnector

Library for using the ILIAS-Soap endpoint in C# projects.

A code generator in Visual Studio is available for SOAP enpoints. If you don't want to use it or need more control, you can use this library.

This library was developed for my own immediate use. Many improvements in design and completeness are necessary.

Installation NuGet:
```
PM > Install-Package ILIASSoapConnector
```

Usage:
```csharp
var endpoint = new ILSoapEndpoint("https://your-ilias-installation.com/webservice/soap/server.php","client-id", "soap-account-username", "password");
var ilUser = await endpoint.GetUserAsync(ilUserId);
```

