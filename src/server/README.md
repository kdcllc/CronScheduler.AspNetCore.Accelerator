# CronScheduler.AspNetCore "Accelerator" - Server

This project is part of the CronScheduler.AspNetCore "Accelerator" solution. It provides the backend services for scheduling and running cron jobs using ASP.NET Core.

## Table of Contents

- [Getting Started](#getting-started)
- [Project Structure](#project-structure)
- [Configuration](#configuration)
- [Database](#database)
- [Services](#services)
- [Controllers](#controllers)
- [Jobs](#jobs)
- [Running the Application](#running-the-application)
- [Contributing](#contributing)
- [License](#license)

## Getting Started

To get started with this project, you will need to have the following tools installed:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQLite](https://www.sqlite.org/download.html)
- [Docker](https://www.docker.com/products/docker-desktop) (optional, for containerized deployment)

## Project Structure

The project is organized as follows:

- `Controllers/`: Contains the API controllers for handling HTTP requests.
- `Data/`: Contains the Entity Framework Core DbContext and database configurations.
- `Jobs/`: Contains the scheduled jobs and their configurations.
- `Models/`: Contains the data models used by the application.
- `Repositories/`: Contains the repository interfaces and implementations for data access.
- `Services/`: Contains the business logic and service implementations.
- `Properties/`: Contains project properties and settings.
- `wwwroot/`: Contains static files served by the application.

## Configuration

Configuration settings for the application are stored in the `appsettings.json` and `appsettings.Development.json` files. These files include settings for logging, database connections, and other application-specific configurations.

## Database

The application uses Entity Framework Core with SQLite as the database provider. The database context is defined in `Data/ApplicationDbContext.cs`. The database is automatically created and updated using EF Core migrations.

## Services

The `Services/` directory contains the business logic and service implementations. Key services include:

- `BibleService`: A service for fetching Bible verses from an external API.
- `BibleVerseStore`: A singleton store for caching Bible verses.
- `JobService`: A service for managing cron jobs and their execution.

## Controllers

The `Controllers/` directory contains the API controllers that handle HTTP requests. Key controllers include:

- `BibleController`: Handles requests related to Bible verses.
- `CronJobController`: Handles requests related to cron jobs.
- `WeatherForecastController`: A sample controller for weather forecasts.

## Jobs

The `Jobs/` directory contains the scheduled jobs and their configurations. Key jobs include:

- `BibleCronJob`: A scheduled job for fetching and storing Bible verses.
- `StartupJob`: A job that runs at application startup.

## Running the Application

To run the application, use the following command:

```bash
dotnet run --project src/server/server.csproj
```

This will start the application and make it available at `http://localhost:5055`.

To run the application in a Docker container, use the following commands:

```bash
docker build -t cronscheduler-server .
docker run -p 8080:80 cronscheduler-server
```

## License

This project is licensed under the MIT License. See the [LICENSE](../../LICENSE) file for details.
