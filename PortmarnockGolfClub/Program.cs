using Microsoft.EntityFrameworkCore;
using PortmarnockGolfClub.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register DbContext with SQLite connection string from appsettings.json
builder.Services.AddDbContext<GolfClubDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure middleware pipeline...
app.Run();
