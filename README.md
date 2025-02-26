# UserManagement API

A microservice-based UserManagement API for ASP.NET Core Web API developed in C# using .NET 9. This API is part of a larger microservices architecture, and relies on PostgreSQL 17 as its database engine. Currently developed on macOS, but it can be set up on Windows or Linux as well.

## Table of Contents

- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [Configuration](#configuration)
- [Running the API](#running-the-api)
- [Development](#development)
- [Contributing](#contributing)
- [License](#license)

## Prerequisites

- .NET 9 SDK  
   Download from the [official .NET website](https://dotnet.microsoft.com/).
- PostgreSQL 17  
   On macOS, install via Homebrew:  
   `brew install postgresql@17`
- An IDE or Editor of your choice (e.g., Visual Studio Code, Rider)

## Getting Started

1. **Clone the Repository**

   ```bash
   git clone https://github.com/KuyeselaOrganization/UserManagementAPI.git
   cd UserManagementAPI
   ```

2. **Restore Dependencies**

   ```bash
   dotnet restore
   ```

3. **Setup PostgreSQL Database**

   - Start the PostgreSQL service:
     ```bash
     brew services start postgresql@17
     ```
   - Create a new database (e.g., `usermanagement`):
     ```bash
     psql -U your_username -c "CREATE DATABASE usermanagement;"
     ```

4. **Configuration**
   Items to configure and copy

   ```bash
    cp appsettings.json.example appsettings.json
    cp appsettings.Development.json.example appsettings.Development.json
   ```

   Copy `appsettings.json.example` to `appsettings.json` if available.

   Update the connection string and other settings as needed in `appsettings.json`.

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Database=usermanagement;Username=your_username;Password=your_password"
     },
     "Logging": {
       "LogLevel": {
         "Default": "Information",
         "Microsoft": "Warning",
         "Microsoft.Hosting.Lifetime": "Information"
       }
     }
   }
   ```

## Running the API

To run the API locally:

```bash
dotnet run
```

The API will be available at `https://localhost:5001` or `http://localhost:5000`.

## Development

- **Hot Reload:**  
   The .NET 9 SDK supports Hot Reload for faster development.
- **Database Migrations:**  
   Use Entity Framework Core migrations to update your database schema:

  ```bash
  dotnet ef migrations add InitialCreate
  dotnet ef database update
  ```

- **Testing:**  
   Write tests using your preferred framework (e.g., xUnit). Run them with:
  ```bash
  dotnet test
  ```

## Contributing

Contributions are welcome! Please fork the repository and create a pull request for any changes. Make sure your code follows the project conventions and that all tests pass before submission.

## License

Distributed under the MIT License. See `LICENSE` for more information.
