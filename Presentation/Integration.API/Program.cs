using MultiStoreIntegration.Persistence.Migrations.Mongo;
using MultiStoreIntegration.Persistence;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MultiStoreIntegration.Application.Features.Commands.Stock.Create.Store3CreateStock;
using MultiStoreIntegration.Infrastructure.Events.Store1;
using MultiStoreIntegration.Application.Features.Commands.Sale.Create.Store1CreateSale;
using MultiStoreIntegration.Application.Features.Commands.Sale.Create.Store3CreateSale;
using MultiStoreIntegration.Application.Abstractions.Token;
using MultiStoreIntegration.Application.Common.Settings;
using MultiStoreIntegration.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MultiStoreIntegration.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// MediatR config
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Store3CreateSaleCommandHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(Store3CreateStockCommandHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(Store1CreateSaleCommandHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(WarehouseSyncAfterStore1StockCreatedEventHandler).Assembly);
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder => builder
            .WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddScoped<ITokenService, TokenService>();

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidateAudience = true,
        ValidAudience = jwtSettings.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

builder.Services.AddInfrastructureServices();

builder.Services.AddPersistenceServices(builder.Configuration);

var app = builder.Build();
BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
app.UseCors("AllowReactApp");

using (var scope = app.Services.CreateScope())
{

    var migrationRunner = scope.ServiceProvider.GetRequiredService<MongoMigrationRunner>();
    await migrationRunner.RunMigrationsAsync();
}

using (var scope = app.Services.CreateScope())
{
    var wareHouseMongoMigrationRunner = scope.ServiceProvider.GetRequiredService<WareHouseMongoMigrationRunner>();
    await wareHouseMongoMigrationRunner.RunWareHouseMigrationsAsync();
}

// Swagger config
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
