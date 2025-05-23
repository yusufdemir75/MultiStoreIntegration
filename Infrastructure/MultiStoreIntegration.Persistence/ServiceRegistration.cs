﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MultiStoreIntegration.Persistence.Contexts;
using MultiStoreIntegration.Persistence.Migrations.Mongo;
using MultiStoreIntegration.Application.Repositories.Store1.Store1Stock;
using MultiStoreIntegration.Persistence.Repositories.Store1.Store1Stock;
using MultiStoreIntegration.Application.Repositories.Store1.Store1Return;
using MultiStoreIntegration.Persistence.Repositories.Store1.Store1Return;
using MultiStoreIntegration.Application.Repositories.Store1.Store1Sale;
using MultiStoreIntegration.Persistence.Repositories.Store1.Store1Sale;
using MultiStoreIntegration.Persistence.Repositories.Store2.Store2Stock;
using MultiStoreIntegration.Application.Repositories.Store2.Store2Stock;
using MultiStoreIntegration.Application.Repositories.Store2.Store2Return;
using MultiStoreIntegration.Persistence.Repositories.Store2.Store2Return;
using MultiStoreIntegration.Application.Repositories.Store2.Store2Sale;
using MultiStoreIntegration.Persistence.Repositories.Store2.Store2Sale;
using MultiStoreIntegration.Application.Repositories.Store3;
using MultiStoreIntegration.Domain.MongoDocuments;
using MultiStoreIntegration.Persistence.Repositories.Store3;
using MultiStoreIntegration.Application.Repositories.Store3.Store3Stock;
using MultiStoreIntegration.Persistence.Repositories.Store3.Store3Stock;
using MultiStoreIntegration.Application.Repositories.Store3.Store3Sale;
using MultiStoreIntegration.Persistence.Repositories.Store3.Store3Sale;
using MultiStoreIntegration.Domain.MongoDocuments.Store3MongoDocuments;
using MultiStoreIntegration.Application.Repositories.Store3.Store3Return;
using MultiStoreIntegration.Persistence.Repositories.Store3.Store3Return;

namespace MultiStoreIntegration.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Store1 ve Store2 için PostgreSQL bağlantılarını ekliyoruz
            services.AddDbContext<Store1DbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Store1Db")));

            services.AddDbContext<Store2DbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Store2Db")));

            services.AddKeyedSingleton<IMongoClient>("Store3MongoClient", (sp, _) =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var connectionString = configuration["MongoDb:ConnectionString"];
                return new MongoClient(connectionString);
            });

            services.AddKeyedSingleton<IMongoClient>("WarehouseMongoClient", (sp, _) =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var warehouseConnectionString = configuration["WareHouse:ConnectionString"];
                return new MongoClient(warehouseConnectionString);
            });


            // MongoMigrationRunner ve WareHouseMongoMigrationRunner'ı scoped olarak kaydediyoruz
            services.AddScoped<MongoMigrationRunner>();
            services.AddScoped<WareHouseMongoMigrationRunner>();

            // Store3 ve Warehouse için MongoDB context servislerini scoped olarak kaydediyoruz
            services.AddScoped<Store3MongoContext>(sp =>
            {
                var mongoClient = sp.GetRequiredKeyedService<IMongoClient>("Store3MongoClient");
                return new Store3MongoContext(mongoClient, configuration);
            });

            services.AddScoped<WarehouseMongoDbContext>(sp =>
            {
                var mongoClient = sp.GetRequiredKeyedService<IMongoClient>("WarehouseMongoClient");
                return new WarehouseMongoDbContext(mongoClient, configuration);
            });

            // Store3 Repository Servislerini ekliyoruz

            services.AddScoped<Store3IStockReadRepository, Store3StockReadRepository>();
            services.AddScoped<Store3IStockWriteRepository, Store3StockWriteRepository>();
            services.AddScoped<Store3ISaleWriteRepository, Store3SaleWriteRepository>();
            services.AddScoped<Store3ISaleReadRepository, Store3SaleReadRepository>();
            services.AddScoped<Store3IReturnReadRepository, Store3ReturnReadRepository>();
            services.AddScoped<Store3IReturnWriteRepository, Store3ReturnWriteRepository>();
            services.AddScoped<Store3IWriteRepository<Store3StockDocument>, Store3WriteRepository<Store3StockDocument>>();

            // Store1 Repository Servislerini ekliyoruz
            services.AddScoped<Store1IStockReadRepository, Store1StockReadRepository>();
            services.AddScoped<Store1IStockWriteRepository, Store1StockWriteRepository>();
            services.AddScoped<Store1IReturnReadRepository, Store1ReturnReadRepository>();
            services.AddScoped<Store1IReturnWriteRepository, Store1ReturnWriteRepository>();
            services.AddScoped<Store1ISaleReadRepository, Store1SaleReadRepository>();
            services.AddScoped<Store1ISaleWriteRepository, Store1SaleWriteRepository>();

            // Store2 Repository Servislerini ekliyoruz
            services.AddScoped<Store2IStockReadRepository, Store2StockReadRepository>();
            services.AddScoped<Store2IStockWriteRepository, Store2StockWriteRepository>();
            services.AddScoped<Store2IReturnReadRepository, Store2ReturnReadRepository>();
            services.AddScoped<Store2IReturnWriteRepository, Store2ReturnWriteRepository>();
            services.AddScoped<Store2ISaleReadRepository, Store2SaleReadRepository>();
            services.AddScoped<Store2ISaleWriteRepository, Store2SaleWriteRepository>();
        }
    }
}
