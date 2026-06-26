# Neon Vault 🎮

Welcome to **Neon Vault**, a digital video game storefront featuring a modern cyberpunk aesthetic. This web application provides a full e-commerce experience, allowing users to browse a catalog of games, manage a shopping cart, process simulated checkouts, and view their purchased games in a personal digital library. It also includes a secure administrative backend for inventory management.

## 🌟 Key Features

*   **Dynamic Storefront:** Browse an inventory of games dynamically loaded from the database, styled with a responsive, neon-infused cyberpunk theme.
*   **Shopping Cart System:** Add games to a session-based shopping cart, view your current total, and proceed to checkout without needing a pre-registered account.
*   **User Library:** After checkout, games are securely assigned to the user's personal digital library for viewing.
*   **Admin Dashboard:** A protected administrative panel where store managers can perform full CRUD (Create, Read, Update, Delete) operations on the store's game inventory.
*   **State Persistence:** Powered by Entity Framework Core and SQL Server, ensuring all inventory, orders, and user libraries are securely persisted.

## 🛠️ Technology Stack

*   **Framework:** ASP.NET Core MVC (.NET 10)
*   **Language:** C#
*   **Database:** Microsoft SQL Server
*   **ORM:** Entity Framework Core 9.0 (Code-First approach)
*   **Frontend:** HTML5, CSS3 (Custom Styling), Razor Pages
*   **State Management:** ASP.NET Core Session State & Distributed Memory Cache

## 🚀 Getting Started

To get a local copy up and running, follow these simple steps:

### Prerequisites
*   [.NET 10.0 SDK](https://dotnet.microsoft.com/download)
*   Visual Studio 2022 (or VS Code with C# Dev Kit)
*   SQL Server Express or LocalDB (Installed with Visual Studio)

### Installation

1. **Clone the repository:**
   ```bash
   git clone https://github.com/Rokxany/Neon-vault.git
   ```
2. **Navigate to the project directory:**
   ```bash
   cd Neon-vault
   ```
3. **Restore dependencies & run the project:**
   The project is configured to automatically apply database migrations and seed initial data upon the first run.
   ```bash
   dotnet build
   dotnet run
   ```
4. **Open in browser:**
   Navigate to `https://localhost:7193` (or the port specified in your terminal) to view the storefront.

## 👥 Team & Contributors

This project was built collaboratively. Below are the team members and their primary areas of focus:

*   **[Rokxany] Hamza Nizamani:** E-Commerce Logic & Session Management (Full Stack)
*   **Hassan Mughis:** Admin Systems & User Integration (Full Stack)
*   **Muhammad Abdullah:** Database & Backend Architecture (Lead Backend)
*   **Bilawal Feroze:** UI/UX Design & Frontend Development (Lead Frontend)

---
*Built with ❤️ for our Web Development Course.*
