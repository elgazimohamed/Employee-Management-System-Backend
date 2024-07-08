# Employee Management System - Backend API

## Overview

This project is an Employee Management System backend API built using ASP.NET Core 8 Web API. It provides endpoints for managing employees, including CRUD operations and validation.

## Features

- CRUD operations for managing employees.
- Validation for email uniqueness.
- Error handling and logging.

## Technologies Used

- ASP.NET Core Web API 8
- Entity Framework Core 8
- SQLite
  
## Getting Started

To get a local copy up and running, follow these simple steps.

### Prerequisites

- .NET SDK 8
- Visual Studio or Visual Studio Code (optional)

### Installation

1. **Clone the repository**

   ```sh
   git clone https://github.com/elgazimohamed/Employee-Management-System-Backend.git
3. **Navigate to the project directory**

   ```sh
   cd Employee-Management-System-Backend
5. **Install dependencies**

   ```sh
   dotnet restore
7. **Database setup**

   ```sh
   dotnet ef database update

### Usage

1. **Run the application**

   ```sh
   dotnet run
2. **API Endpoints**

- `GET /api/employee`: Get all employees.
- `GET /api/employee/{id}`: Get an employee by ID.
- `POST /api/employee`: Add a new employee.
- `PUT /api/employee/{id}`: Update an existing employee.
- `DELETE /api/employee/{id}`: Delete an employee by ID.
   
