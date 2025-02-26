# UserManagement API

A microservice-based **User Management API** built with **ASP.NET Core 9** in C#. This API is designed for **plug-and-play user authentication and role-based access control (RBAC)**, supporting **PostgreSQL 17** as the database engine.

This project is currently developed on **macOS**, but it can be set up on **Windows** and **Linux** as well.

## **Table of Contents**

- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [Configuration](#configuration)
- [Running Database Migrations](#running-database-migrations)
- [Running the API](#running-the-api)
- [Development](#development)
- [Testing](#testing)
- [Contributing](#contributing)
- [License](#license)

---

## **Prerequisites**

Before running this API, ensure you have the following installed:

1. **.NET 9 SDK**

   - Download and install from the [official .NET website](https://dotnet.microsoft.com/).
   - Verify installation:
     ```bash
     dotnet --version
     ```

2. **PostgreSQL 17**

   - **macOS** (using Homebrew):
     ```bash
     brew install postgresql@17
     ```
   - **Ubuntu/Debian**:
     ```bash
     sudo apt update
     sudo apt install postgresql postgresql-contrib
     ```
   - **Windows**:
     - Download and install from [PostgreSQL website](https://www.postgresql.org/download/).

3. **Entity Framework Core CLI**  
   If not already installed, run:

   ```bash
   dotnet tool install --global dotnet-ef
   ```

4. **An IDE or Editor**
   - Recommended: **Visual Studio Code**, **JetBrains Rider**, or **Visual Studio**.

## **Getting Started**

Follow these steps to set up the project:

### **1. Clone the Repository**

```bash
git clone https://github.com/KuyeselaOrganization/UserManagementAPI.git
cd UserManagementAPI
```

### **2. Restore Dependencies**

Not necessary if it is a new setup otherwise run the following command to restore dependencies:

```bash
dotnet restore
```

### **3. Start PostgreSQL (If not running)**

- **macOS**:
  ```bash
  brew services start postgresql@17
  ```
- **Ubuntu/Debian**:
  ```bash
  sudo systemctl start postgresql
  ```

### **4. Create a PostgreSQL Database**

Run the following command to create a new database for the API:

```bash
psql -U your_username -c "CREATE DATABASE kuyeselaUserDb;"
```

**Replace** `your_username` with your PostgreSQL username.

---

## **Configuration**

### **1. Copy the Example Configurations**

Run the following command to copy the default settings:

```bash
cp appsettings.json.example appsettings.json
cp appsettings.Development.json.example appsettings.Development.json
```

If `appsettings.json.example` does not exist, manually create `appsettings.json`.

### **2. Update Database Connection String**

Open **`appsettings.json`** and update the connection string to match your PostgreSQL credentials:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=usermanagement;Username=your_username;Password=your_password"
  },
  "JwtSettings": {
    "Secret": "YOUR_SECRET_KEY",
    "Issuer": "UserManagementService",
    "Audience": "UserManagementClients",
    "ExpiryMinutes": 60
  }
}
```

Replace:

- **`your_username`** → Your PostgreSQL username.
- **`your_password`** → Your PostgreSQL password.

---

## **Running Database Migrations**

Before running the API, apply database migrations:

```bash
dotnet ef database update
```

If you encounter an error **"`dotnet ef` not found"**, install EF Core CLI using:

```bash
dotnet tool install --global dotnet-ef
```

Developers or engineers who would like to add a migrations file can run the following command:

```bash
dotnet ef migrations add MigrationName
```

### **Database Seeding**

On startup, the system **automatically creates an "Administrator" role and a default admin user**:

- **Admin Email:** `admin@system.com`
- **Admin Password:** `Admin@123`

After running migrations, you can log in with these credentials.

---

## **Running the API**

To start the API locally, run:

```bash
dotnet run
```

Once the server is running, you can access the API at:

- **HTTP:** `http://localhost:5003`
- **HTTPS:** `https://localhost:7022`

### **Swagger API Documentation**

To explore available API endpoints using **Swagger**, navigate to:

```
http://localhost:5003/swagger
```

---

## **Development**

### **1. Hot Reload**

For automatic code updates while running:

```bash
dotnet watch run
```

### **2. Resetting the Database**

If you need to start over, reset the database:

```bash
dotnet ef database drop
dotnet ef database update
```

---

## **Testing**

The project includes **unit tests** to validate the API's functionality.

To run tests:

```bash
dotnet test
```

---

## **Contributing**

Contributions are welcome! 🚀 If you’d like to improve this project:

1. **Fork the repository**
2. **Create a feature branch**
3. **Commit your changes**
4. **Push to your fork**
5. **Open a pull request**

Please follow the project's **coding style** and ensure **all tests pass** before submitting changes.

---

## **License**

This project is licensed under the **MIT License**. See `LICENSE` for more details.

---
