# InsightLink - URL Shortener & Analytics Platform 🚀

InsightLink is a modern, scalable URL shortening service built with .NET and the Microsoft Azure cloud platform. This project serves as a portfolio piece to demonstrate best practices in cloud-native application development, including API design, asynchronous processing, and secure infrastructure management.

## Features ✨

-   [x] **Core API:** A .NET Minimal API for creating and managing shortened URLs.
-   [x] **High-Speed Redirects:** A redirect service that forwards users from the short link to the original URL.
-   [ ] **Asynchronous Analytics:** A decoupled, event-driven backend using Azure Functions and Queue Storage to track click analytics without slowing down redirects.
-   [ ] **User Dashboard:** A Blazor front-end for registered users to view their links and click-through statistics.

---

## Tech Stack 💻

This project is built using a modern, cloud-native stack:

-   **Backend:** C# with .NET 9 Minimal API
-   **Data Persistence:**
    -   Entity Framework Core 9
    -   Azure SQL Database for primary link storage
-   **Cloud Platform:** Microsoft Azure
    -   **Azure App Service:** For hosting the core API.
    -   **Azure Functions:** For serverless, event-driven analytics processing.
    -   **Azure Queue Storage:** For decoupling the API from the analytics backend.
-   **Infrastructure & Tooling:**
    -   Azure CLI
    -   Git & GitHub

---

## Getting Started ⚙️

### Prerequisites

-   [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
-   [Azure CLI](https://docs.microsoft.com/cli/azure/install-azure-cli)
-   An active Azure Subscription

### Setup & Configuration

1.  **Clone the repository:**
    ```bash
    git clone <your-repo-url>
    cd InsightLink
    ```

2.  **Create Azure Resources:**
    This project requires an Azure Resource Group and an Azure SQL Server instance. You can create them via the Azure Portal or by using the Azure CLI.

3.  **Configure Local Secrets:**
    The application uses `appsettings.Development.json` for local development. Create this file inside the `InsightLink.Api` project folder and add your database connection string:
    ```json
    {
      "ConnectionStrings": {
        "Database": "<your-azure-sql-connection-string>"
      }
    }
    ```

4.  **Run Database Migrations:**
    Navigate into the API project folder and run the Entity Framework migrations to create the database schema.
    ```bash
    cd InsightLink.Api
    dotnet ef database update
    ```

---

## Running the Application

Once the setup is complete, you can run the API locally from the `InsightLink.Api` directory:

```bash
dotnet run