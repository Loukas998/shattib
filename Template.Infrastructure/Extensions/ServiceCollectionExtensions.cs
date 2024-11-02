using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Template.Domain.Entities;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;
using Template.Infrastructure.Repositories;
using Template.Infrastructure.Seeders;
using Template.Infrastructure.Services;

namespace Template.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("TemplateDb");
        var version = new MySqlServerVersion(new Version(8, 0, 2));
        services.AddDbContext<TemplateDbContext>(
            options => options.UseMySql(connectionString, version).EnableSensitiveDataLogging()
        );

        //this for identity and jwt when needed
        services.AddIdentityCore<User>()
            .AddRoles<IdentityRole>()
            .AddTokenProvider<DataProtectorTokenProvider<User>>("ShattibTokenProvidor")
            .AddEntityFrameworkStores<TemplateDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<ICategoriesSeeder, CategoriesSeeder>();
        services.AddScoped<ISeeder, RolesSeeder>();

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ISpecificationRepository, SpecificationRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ISeededValuesRepository, SeededValuesRepository>();

        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IFileService, FileService>();
    }
}