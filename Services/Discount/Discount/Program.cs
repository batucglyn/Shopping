using Discount.Extensions;
using Discount.Handlers;
using Discount.Repositories;
using Discount.Services;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var cs = builder.Configuration.GetValue<string>("DatabaseSettings:ConnectionString");
Console.WriteLine($"[DISCOUNT] Using Connection String: {cs?.Replace("Password=Password@1", "Password=****")}");
//Mediatr
var assemblies = new Assembly[]
{

   Assembly.GetExecutingAssembly(), typeof(CreateDiscountCommandHandler).Assembly
};

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddGrpc();

var app = builder.Build();

//Migrate the Database
app.MigrateDatabase<Program>();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<DiscountService>();
});


app.Run();