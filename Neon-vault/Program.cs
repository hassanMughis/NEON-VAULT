using Microsoft.EntityFrameworkCore;
using Neon_vault.Data;
using Neon_vault.Helpers;
using Neon_vault.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register Entity Framework Core with SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add session support (used for anonymous cart tracking + admin auth)
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(2);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddSignalR();

var app = builder.Build();

// Auto-apply migrations and seed database on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
    await db.Database.ExecuteSqlRawAsync(@"IF COL_LENGTH('ChatUsers', 'ProfileImageUrl') IS NULL ALTER TABLE ChatUsers ADD ProfileImageUrl nvarchar(500) NULL;");

    if (!await db.ChatUsers.AnyAsync(u => u.IsAdmin))
    {
        db.ChatUsers.Add(new ChatUser
        {
            Username = "admin",
            DisplayName = "Administrator",
            Email = "admin@neonvault.com",
            PasswordHash = SecurityHelper.HashPassword("admin123"),
            IsAdmin = true,
            IsTemporaryGuest = false,
            SessionId = "seed-admin",
            AvatarColorHex = "#DDB7FF"
        });
        await db.SaveChangesAsync();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapStaticAssets();

// Default route: Home redirects to Store
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapHub<Neon_vault.Hubs.ChatHub>("/chatHub");

app.Run();
