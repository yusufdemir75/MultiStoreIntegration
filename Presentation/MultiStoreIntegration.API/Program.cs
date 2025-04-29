using MultiStoreIntegration.Application.Features.Commands.Stock.CreateStock;
using MultiStoreIntegration.Persistence;
using MultiStoreIntegration.Persistence.Migrations.Mongo;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ MediatR config - doğru şekilde
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateStockCommandHandler).Assembly);
});

// ✅ Persistence servislerini yükle
builder.Services.AddPersistenceServices(builder.Configuration);

var app = builder.Build();

// ✅ Migration'ları çalıştır
using (var scope = app.Services.CreateScope())
{
    var migrationRunner = scope.ServiceProvider.GetRequiredService<MongoMigrationRunner>();
    await migrationRunner.RunMigrationsAsync();
}

// ✅ Swagger config
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
