# WatchHub

A Sample N-Layered .NET Core Project
Demonstrating Clean Architecture and the Generic Repository Pattern.

## Packages

### Application Core
```
Install-Package Ardalis.Specification
````

### Infrastructure
```
Install-package Microsoft.EntityFrameworkCore
Install-package Npgsql.EntityFrameworkCore.PostgreSQL
Install-package Microsoft.EntityFrameworkCore.Tools
Install-package Microsoft.EntityFrameworkCore.Design
Install-package Microsoft.AspNetCore.Identity.EntityFrameworkCore
Install-package Ardalis.Specification.EntityFrameworkCore
```

### UnitTests
```
Install Package NSubstitute
```

### Migrations

Before running the following commands, make sure that Web is set as startup project. Run the following commands on the project "Infrastructure".

### Infrastructure
```
Add-Migration InitialCreate -Context WatchHubContext -o Data/Migrations
Update-Database -Context WatchHubContext

Add-Migration InitialIdentity -Context AppIdentityContext -o Identity/Migrations
Update-Database -Context AppIdentityContext
```