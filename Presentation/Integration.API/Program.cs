using MultiStoreIntegration.Persistence.Migrations.Mongo;
using MultiStoreIntegration.Persistence;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MultiStoreIntegration.Application.Features.Commands.Stock.Create.Store3CreateStock;
using MultiStoreIntegration.Infrastructure.Events.Store1;
using MultiStoreIntegration.Application.Features.Commands.Sale.Create.Store1CreateSale;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// MediatR config
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Store3CreateStockCommandHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(Store1CreateSaleCommandHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(WarehouseSyncAfterStore1StockCreatedEventHandler).Assembly);
});

// Persistence servislerini yükle
builder.Services.AddPersistenceServices(builder.Configuration);

var app = builder.Build();
BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

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
