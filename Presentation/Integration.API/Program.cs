using MultiStoreIntegration.Application.Features.Commands.Sale.CreateSale;
using MultiStoreIntegration.Application.Features.Commands.Stock.Store3CreateStock;
using MultiStoreIntegration.Persistence.Migrations.Mongo;
using MultiStoreIntegration.Persistence;
using MultiStoreIntegration.Application.Features.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// MediatR config
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Store3CreateStockCommandHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(CreateSaleCommandHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(WarehouseSyncAfterStockCreatedEventHandler).Assembly);
});

// Persistence servislerini yükle
builder.Services.AddPersistenceServices(builder.Configuration);

var app = builder.Build();

// Migration'larý çalýþtýr
using (var scope = app.Services.CreateScope())
{
    var migrationRunner = scope.ServiceProvider.GetRequiredService<MongoMigrationRunner>();
    await migrationRunner.RunMigrationsAsync();
}

using (var scope = app.Services.CreateScope())
{
    var wareHouseMongoMigrationRunner = scope.ServiceProvider.GetRequiredService<WareHouseMongoMigrationRunner>();
    await wareHouseMongoMigrationRunner.RunMigrationsAsync();
}

// Swagger config
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
