using Domain.Commands;
using Domain.Interface;
using Domain.Service;
using Domain.Util;
using Infrastructure.Database;
using Infrastructure.Database.Repository;
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

// Dependency injection configuration
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddSingleton(new Database(connectionString));
builder.Services.AddSingleton<IDatabase>(new Database(connectionString));
builder.Services.AddScoped<IMigration, Migration>();
builder.Services.AddScoped<ISeeder, Seeder>();
builder.Services.AddScoped<Migration>();
builder.Services.AddScoped<Seeder>();
builder.Services.AddScoped<MigrationCommand>();
builder.Services.AddScoped<TestCommand>();
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<CommandUtil>();
builder.Services.AddScoped<IFactory, Factory>();
builder.Services.AddScoped<Factory>();
builder.Services.AddScoped<IGroupChatRepository, GroupChatRepository>();
builder.Services.AddScoped<GroupChatRepository>();
builder.Services.AddScoped<GroupChatService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

if (args.Length > 0)
{
    using (var scope = app.Services.CreateScope())
    {
        var commandUtil = scope.ServiceProvider.GetRequiredService<CommandUtil>();
        commandUtil.RunAsCommand(args);
        return;
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
