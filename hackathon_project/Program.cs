using hackathon_project.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// DbContext'i sisteme tan�t�yoruz
builder.Services.AddDbContext<HavuzYemekDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Veritabanı dizinini oluştur ve tabloları hazırla
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<HavuzYemekDbContext>();
    var connStr = app.Configuration.GetConnectionString("DefaultConnection") ?? "";
    var dataSource = connStr.Replace("Data Source=", "").Trim();
    var dir = Path.GetDirectoryName(dataSource);
    if (!string.IsNullOrEmpty(dir))
        Directory.CreateDirectory(dir);
    db.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Sehirler}/{action=Index}/{id?}"); // "Home" yerine "Sehirler" yazd�k

app.Run();