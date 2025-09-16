# SafeVault

Secure ASP.NET Core 9.0 MVC web application with:

- 🔐 Input validation (XSS, SQLi)
- 🔑 Authentication with cookie scheme
- 🧑‍⚖️ Role-based access control (RBAC)
- 🧪 Security tests (unit)

## Users (after seeding)

| Email               | Password  | Role  |
| ------------------- | --------- | ----- |
| admin@safevault.com | Admin123! | Admin |
| user@safevault.com  | User123!  | User  |

## Run the project

```bash
dotnet build
dotnet run
dotnet test
```
