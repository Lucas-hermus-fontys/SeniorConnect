using Domain.Commands;
using Domain.Service;
using Domain.Util;
using Infrastructure.Database;
using Infrastructure.Database.Repository;
using Infrastructure.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//some delicious home baked cookies by grandma herself.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
        options.LoginPath = "/auth/login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

builder.Services.AddAuthorization();

// dependency injection configuration
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddSingleton(new Database(connectionString));
builder.Services.AddSingleton<IDatabase>(new Database(connectionString));


builder.Services.AddScoped<Migration>();
builder.Services.AddScoped<Seeder>();
builder.Services.AddScoped<MigrationCommand>();
builder.Services.AddScoped<TestCommand>();
builder.Services.AddScoped<CommandUtil>();
builder.Services.AddScoped<AuthenticationService>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

if (args.Length > 0)
{
    CommandUtil commandUtil = app.Services.GetRequiredService<CommandUtil>();
    commandUtil.RunAsCommand(args);
    return;
}


// new MigrationCommand().MigrateDatabase(new string[] { "", "seed" });

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();