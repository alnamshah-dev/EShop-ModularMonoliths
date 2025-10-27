var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) =>
    config.ReadFrom.Configuration(context.Configuration));

// Add the services to the container

// Common services : MediatR, Carter, FluentValidation
var catalogAssembly = typeof(CatalogModule).Assembly;
var basketAssembly = typeof(BasketModule).Assembly;

builder.Services.AddCarterWithAssemblies(catalogAssembly,
    basketAssembly);

builder.Services.AddMediatRWithAssemblies(catalogAssembly,
    basketAssembly);

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});
// Module services : Catalog, Basket, Ordering
builder.Services
   .AddCatalogModule(builder.Configuration)
   .AddBasketModule(builder.Configuration)
   .AddOrderingModule(builder.Configuration);

builder.Services.AddExceptionHandler<CustomExceptionHandler>();


var app = builder.Build();

// Configure the HTTP request pipeline
app.MapCarter();
app.UseSerilogRequestLogging();
app.UseExceptionHandler(options => { });

app 
    .UseCatalogModule()
    .UseBasketModule()
    .UseOrderingModule();


app.Run();
