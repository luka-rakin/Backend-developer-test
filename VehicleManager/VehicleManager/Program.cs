using Microsoft.EntityFrameworkCore;
using Ninject;
using Ninject.Extensions.DependencyInjection;
using Ninject.Web.Common;
using VehicleManager;
using VehicleManager.Repository;
using VehicleManager.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Host.UseServiceProviderFactory(new NinjectServiceProviderFactory(kernel =>
{

    kernel.Bind<IVehicleMakeRepository>().To<VehicleMakeRepository>().InRequestScope();
    kernel.Bind<IVehicleMakeService>().To<VehicleMakeService>().InRequestScope();
}));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=VehicleMake}/{id?}");

app.Run();
