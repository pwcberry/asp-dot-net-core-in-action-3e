# Learning

## Get started

Set the connections string as a local secret:

```powershell
# At the root of the solution
dotnet user-secrets init --project ./data/RazorApp.Data.csproj
dotnet user-secrets set "ConnectionString" $PATH_TO_SQLITE_FILE --project .\data\RazorApp.Data.csproj
```

This is more useful than specifying an environment variable when only using a database in a local environment.

## References

* [Safe storage of app secrets in development in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-10.0)