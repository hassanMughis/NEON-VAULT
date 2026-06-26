# Neon Vault - Team Contribution Report

## Overview
This document outlines the division of labor for the **Neon Vault** web development project. The application was built using ASP.NET Core MVC, Entity Framework Core, and SQL Server. The workload was evenly distributed among 4 team members, allowing each student to touch different parts of the MVC architecture (Models, Views, and Controllers).

---
### Hamza Nizamani: E-Commerce Logic & Session Management (Full Stack)
**Focus:** Implementing the core shopping experience, cart logic, and state management.
*   **Shopping Cart:** Developed the `CartController.cs` to handle the logic of adding, viewing, and removing items from the user's cart.
*   **Session Management:** Implemented ASP.NET Core Session State and Distributed Memory Cache, allowing users to browse and add items to their cart anonymously.
*   **Checkout Process:** Programmed the `CheckoutController.cs` to calculate total prices, validate the cart, and convert temporary session-based cart items into permanent `Order` and `OrderItem` records in the database.
*   **Cart Views:** Built the user interface for the shopping cart page and checkout confirmation screens.

### Hassan Mughis : Admin Systems & User Integration (Full Stack)
**Focus:** Building the secure administrative backend (CRUD operations) and the user's digital library.
*   **Admin Dashboard:** Created the `AdminController.cs` to handle all administrative routes and protect them via session-based authentication.
*   **Inventory Management (CRUD):** Implemented the full Create, Read, Update, and Delete logic, allowing store managers to add new games, edit prices/details, and remove old stock from the database.
*   **Admin Views:** Designed the secure data-entry forms and inventory data tables (`Views/Admin/Games.cshtml`, `Views/Admin/Dashboard.cshtml`).
*   **User Library:** Developed the `LibraryController.cs` and `Library/Index.cshtml` to fetch and display games that a user has successfully purchased and added to their account.

### Muhammad Abdullah: Database & Backend Architecture (Lead Backend)
**Focus:** Establishing the foundational data structures, application configuration, and database connections.
*   **Database Setup:** Configured Microsoft SQL Server and integrated Entity Framework Core into the application.
*   **Data Models:** Designed and programmed the core C# model classes representing the business logic (`Game.cs`, `Order.cs`, `OrderItem.cs`, `CartItem.cs`, `LibraryItem.cs`).
*   **AppDbContext:** Created the database context class, configured the precision for financial data, and wrote the logic to automatically apply database migrations on startup.
*   **Data Seeding:** Generated the initial seed data for the storefront games (Titles, Descriptions, Prices, Image URLs) to ensure the store had populated content upon launch.
*   **App Configuration:** Managed `Program.cs` and `appsettings.json` to configure database connection strings, services, and middleware.

### Bilawal Feroze: UI/UX Design & Frontend Development (Lead Frontend)
**Focus:** Crafting the visual identity, responsive layouts, and user-facing views using Razor syntax.
*   **Theme & Styling:** Developed the custom "cyberpunk" visual aesthetic using HTML5, CSS3, and responsive design principles.
*   **Layouts:** Built the foundational Razor layouts (`_Layout.cshtml` for the main site and `_AdminLayout.cshtml` for the backend).
*   **Store Views:** Implemented the main storefront views (`Store/Index.cshtml`, `Store/AllGames.cshtml`), including the responsive grid system for displaying game cards.
*   **Navigation:** Designed the persistent navigation structures (Store sidebar, Admin sidebar, and top navigation bars) to ensure a smooth user experience across different screen sizes.


