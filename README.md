# Introduction
**Eshop Product API**
This project is a **REST API service** for an e-commerce shop, providing a set of endpoints to manage product data. The purpose of the project is to showcase production-grade practices in API design, adhering to principles like **SOLID**, **layered architecture**, and **best practices for REST API development**.
This API service provides functionality to:
- Retrieve all available products, with support for **pagination**.
- Retrieve details of a specific product by its ID.
- Partially update the description of a single product.

The API is fully documented using **Swagger** and comes with unit tests to ensure endpoint functionality. The solution leverages a **SQL Server database** for data storage, while also supporting the ability to switch easily between mock and database implementations.

## Features
- Full support for **REST API best practices**.
- **Versioned API** for product retrieval, offering paginated results in version `v2`.
- Secure, maintainable, and production-grade code structure.
- API documentation available via **Swagger**.
- Complete test coverage of endpoint functionality using **unit tests**.
- **Mock data** support for testing purposes.
- Includes a **SQL Server database** with a seeded initial dataset.

## Tech Stack
- **Framework**: ASP.NET Core 8.0
- **Language**: C# 12.0
- **Database**: SQL Server (with optional mock data for testing)
- **ORM**: Entity Framework Core
- **Documentation**: Swagger UI
- **Testing**: xUnit

## Prerequisites
Ensure the following tools are installed:
- .NET SDK 8.0 or newer
- SQL Server
- An IDE (e.g., JetBrains Rider or Visual Studio)
- Postman or equivalent for testing APIs (optional)

## Getting Started
### 1. Clone the Repository
Clone the repository and navigate to the project directory:

git clone <<repository-url>>
cd <<project-folder>>

### 2. Configure the Connection String
Update the connection string in `appsettings.json` to connect to your SQL Server instance:

"ConnectionStrings": {
"SqlServerConnection": "Server=<your-server>;Database=<your-database>;User Id=<your-username>;Password=<your-password>;TrustServerCertificate=true"
}

Replace `<your-server>`, `<your-database>`, `<your-username>`, and `<your-password>` with the correct values for your environment.
