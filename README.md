# Neon Vault 🎮 - Developer Documentation & Guide

Welcome to **Neon Vault**, a digital video game and hardware storefront designed with a responsive, modern cyberpunk aesthetic. This guide documents the visual identity, layouts, MVC pattern, and core .NET Core concepts implemented in this project.

---

## 🎨 Visual Identity & Layouts

### 1. Theme & Styling (Cyberpunk Aesthetic)
- **Visual Palette**: The site features a premium dark color theme: Deep Navy (`#060E20` / `#0B1326`), Slate borders (`#1E293B`), and vibrant Neon Accent Purple (`#DDB7FF` / `#b17be8`).
- **Styling Elements**: Built with a hybrid of custom CSS3 animations and **Tailwind CSS**. Includes premium visual states:
  - Glow and drop shadows for cards (`shadow-[0_0_15px_rgba(221,183,255,0.15)]`).
  - Smooth card-scale transformations (`transition: transform 0.3s cubic-bezier`).
  - Active-thumbnail border glows and active sidebar link highlights.

### 2. Layout Architecture (Razor Layouts)
- **`_Layout.cshtml`**: The base layout for public storefront pages. It includes:
  - Persistent header with brand logo.
  - Profile dropdown menu with session state checks for username and avatar color badges.
  - Active shopping cart badge displaying the count of cart items.
  - Custom styles block using `@await RenderSectionAsync("Styles", false)`.
- **`_AdminLayout.cshtml`**: The dedicated layout for the administrative control panel. It sets up a two-column structure (sidebar + main viewport) tailored for content-heavy grids and management views.

### 3. Storefront Views
- **`Store/Index.cshtml`**: The storefront catalog layout featuring:
  - Collapsible storefront sidebar with nested category accordions.
  - Search filter input and dynamic sorting selector.
  - Responsive Grid System (`grid grid-cols-2 sm:grid-cols-3 xl:grid-cols-4`) rendering products with interactive cards, prices, and quick "Add to Cart" submissions.
- **`Store/ProductDetails.cshtml`**: The product details page hosting a multi-image product gallery:
  - Large main viewport for the active preview image.
  - Thumbnails row underneath. Clicking a thumbnail updates the viewport image source and transfers the active outline/neon glow wrapper using client-side JavaScript.

### 4. Navigation Systems
- **Collapsible Store Sidebar**: Fully collapsible using a floating toggle button. State (collapsed vs. expanded) is written to `localStorage` so it persists across page reloads. Text labels disappear when collapsed, leaving clean, centered navigation icons.
- **Nested Accordions**: Sections like "Browse", "Games by Genre", and "Hardware by Type" collapse and expand smoothly without page reloads using a custom CSS class `.section-collapsed` (`display: none !important`) to bypass Tailwind flex conflicts.
- **Admin Sidebar (`_AdminSidebar.cshtml`)**: Houses links to the Dashboard, Orders, Games, Users, and the newly implemented "Complaints" list.

---

## 🛠️ ASP.NET Core MVC Architecture Guide

This project is built using the **ASP.NET Core MVC (Model-View-Controller)** pattern. Below is a breakdown of how the key modules interact.

```
       ┌───────────┐
       │  Browser  │
       └─────┬─────┘
    GET /   │   ▲ View HTML
    POST    │   │
            ▼   │
       ┌────────┴──┐
       │Controller │◄────── Dependency Injection ─── AppDbContext
       └─────┬─────┘
   Updates   │   ▲ Fetches
    Data     ▼   │  Data
       ┌────────┴──┐
       │   Model   │
       └───────────┘
```

### 1. HTTP GET vs. HTTP POST
- **HTTP GET**: Used to request and retrieve data. GET requests do not modify server state and are bookmarkable.
  - *Example*: Loading the Store catalog page (`/Store`) or the Contact page (`/Home/Contact`).
- **HTTP POST**: Used to submit data to the server to perform side-effects (creating, updating, or deleting resources).
  - *Example*: Submitting a complaint from the contact page form, adding a game to the cart, or deleting a game in the admin panel.

### 2. Controllers & Routing
- Controllers act as the glue between Models and Views. They inherit from `Microsoft.AspNetCore.Mvc.Controller` and contain **Actions** (methods that return `IActionResult`).
- **Dependency Injection (DI)**: App controllers automatically receive a reference to `AppDbContext` via constructor injection:
  ```csharp
  private readonly AppDbContext _db;
  public HomeController(AppDbContext db) {
      _db = db;
  }
  ```
- **Routing**: Matches URL requests (e.g. `/Store/ProductDetails/id`) to specific controllers and actions.

### 3. Models & Database (Entity Framework Core)
- **Models**: Plain Old C# Objects (POCOs) that represent the structure of application data.
  - *Example*: `Complaint.cs` defines:
    ```csharp
    public class Complaint {
        [Key] public Guid Id { get; set; }
        [Required, MaxLength(100)] public string Name { get; set; }
        [Required, EmailAddress] public string Email { get; set; }
        [Required] public string Subject { get; set; }
        [Required] public string Message { get; set; }
        public DateTime SubmittedAt { get; set; }
    }
    ```
- **EF Core Database Context (`AppDbContext.cs`)**: Maps C# entity models to database tables using `DbSet<T>` properties.
- **Migrations**: Incremental database updates. A migration specifies how to update database schemas (e.g., adding the `Complaints` table) using C# definitions.
  - Code-first command: `dotnet ef migrations add AddComplaintsTable`

### 4. Views & Razor Engine
- **Razor Engine**: Renders dynamic views using C# mixed inside HTML. Dynamic Razor tags start with `@`.
  - `@model List<Neon_vault.Models.Complaint>`: Declares the type of data model passed to the view.
  - `@foreach (var item in Model)`: Loops over database rows to generate HTML.
- **Forms and Tokens**: Razor forms include `@Html.AntiForgeryToken()` to prevent Cross-Site Request Forgery (CSRF) attacks.
