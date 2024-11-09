using Infrastructure.Util;
using Domain.Util;
using Infrastructure.Database;
using ServiceLocator = Infrastructure.Util.ServiceLocator;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSingleton(new Database(connectionString));

var app = builder.Build();

ServiceLocator.SetLocatorProvider(app.Services);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

if (args.Length > 0)
{
    CommandUtil commandUtil = new CommandUtil();
    commandUtil.RunAsCommand(args);
    return;
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
