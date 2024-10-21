using SeniorConnect.Infrastructure;
using SeniorConnect.Domain.Util;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews()
    .AddRazorOptions(options =>
    {
        options.ViewLocationFormats.Insert(0, "/Presentation/Views/{1}/{0}.cshtml"); // Views per Controller
        options.ViewLocationFormats.Insert(1, "/Presentation/Views/Shared/{0}.cshtml"); // Shared Views
    });

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
