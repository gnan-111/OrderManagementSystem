# OrderManagementSystem API

An ASP.NET Core Web API project for managing users and orders, with support for user authentication, JWT-based authorization, and clean architecture practices using services and repositories. Includes a React frontend in the `clientapp/` directory.

---

## Features

- User Registration and Login with JWT Authentication
- Role-based Authorization
- CRUD operations for Orders
- Exception handling middleware
- DTO-based API model separation
- Entity Framework Core + Code First Migrations
- CORS configured for React frontend integration

---

## Project Structure

```
OrderManagementSystem/
├── Controllers/               # API endpoints
├── Data/                      # EF Core DbContext
├── DTOs/                      # Data Transfer Objects
├── Middleware/                # Global exception handling
├── Migrations/                # EF Core migration history
├── Models/                    # Entity models
├── Repositories/             # Interfaces and implementations for data access
├── Services/                 # Interfaces and implementations for business logic
├── clientapp/                # React frontend (optional)
├── appsettings.json          # Application configuration
├── Program.cs                # Entry point and middleware setup
```

---

## Tech Stack

- Backend: ASP.NET Core 8 Web API
- Frontend: React (in `clientapp/`)
- Authentication: JWT Bearer Tokens
- Database: SQL Server + Entity Framework Core
- Tools: Swagger, Postman, Visual Studio

---

## Authentication

Uses JWT tokens to secure endpoints.

- Register: `POST /api/Auth/register`
- Login: `POST /api/Auth/login`
- Protected: Add `Authorization: Bearer {token}` header

---

## Running the Project

### Prerequisites

- .NET 8 SDK
- SQL Server (or modify connection string)
- Node.js + npm (for frontend)

### Steps

#### 1. Backend

```bash
dotnet restore
dotnet ef database update
dotnet run
```

API will run at: `https://localhost:5001` or `http://localhost:5000`

#### 2. Frontend

```bash
cd clientapp
npm install
npm start
```

Frontend will run at: `http://localhost:3000`

---

## API Endpoints

### AuthController

- `POST /api/Auth/register` — Register new user
- `POST /api/Auth/login` — Login and receive JWT

### OrdersController

- `GET /api/Orders` — Get all orders
- `POST /api/Orders` — Create order
- `PUT /api/Orders/{id}` — Update order
- `DELETE /api/Orders/{id}` — Delete order

Note: All order endpoints require JWT authorization.

---

## Configuration

Edit `appsettings.json`:

```json
{
  "JwtSettings": {
    "Secret": "your-secret-key",
    "Issuer": "OrderAPI",
    "Audience": "OrderClient",
    "ExpiresInMinutes": 60
  },
  "ConnectionStrings": {
    "DefaultConnection": "Your-SQL-Server-Connection-Here"
  }
}
```

---

## Project Highlights

- DTOs for clean API contracts (`UserLoginDto`, `OrderDto`)
- Repositories for separation of concerns and testability
- Services for business logic
- Middleware for global error handling
- Swagger for API documentation

---

## To-Do (optional)

- Add user roles (Admin/User)
- Unit tests for services and controllers
- Deploy to Azure or Docker
- Implement pagination and filtering for orders

---

## License

MIT License - feel free to use and modify.

---

## Author

Built using ASP.NET Core and React.
