using SeniorConnect.DataAccess;
using SeniorConnect.Util;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSingleton(new Database(connectionString));

WebApplication app = builder.Build();

ServiceLocator.SetLocatorProvider(app.Services);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

if (args.Length > 0)
{
    CommandUtil commandUtil = new CommandUtil();
    commandUtil.RunAsCommand(args);
    return;
}

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
