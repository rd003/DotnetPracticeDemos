# Authenticate .NET Apis with JWT

## Topics covered

- JWT authentication
- Role based authorization

## Tech used

- .NET 9 Api (controller based)
- No database (used List<T> collection for storing and retrieving the user data)

## How to run the project

Clone the repository, open it in VS Code, run the command `dotnet run`.

## Test credentials

```txt
User:

username: user
password: user123

Admin

username: admin
password: admin123
```

## Endpoints

Note: you can aslo test the end points from `JwtDotnet9.http` file. Make sure to install `Rest Client` extenion for Vs Code. If you are using Visual Studio 2022, then it is integrated with it.

### Login endpoint

```txt
POST http://localhost:5163/accounts/login
Content-Type: application/json

{
    "username":"admin",
    "password":"admin123"
}
```

### Weatherforcast

```txt
GET http://localhost:5163/weatherforecast/
Accept: application/json
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFkbWluIiwianRpIjoiYjU0ODY3YTgtM2QwZS00N2NkLThiMWQtZjU3ZDJjOTY5MmQzIiwicm9sZSI6IkFkbWluIiwibmJmIjoxNzQ3NTY2MjA4LCJleHAiOjE3NDc1NjcxMDgsImlhdCI6MTc0NzU2NjIwOCwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MTYzIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo1MTYzIn0.OjqtxwC5z-SWuVdPQjMb_BgffEVyMqbeOlNc3NgMcjw
```

Good luck..