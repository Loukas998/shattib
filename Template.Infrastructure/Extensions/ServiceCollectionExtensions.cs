using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Template.Domain.Constants;
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
        services.Configure<AzureBlobSettings>(configuration.GetSection("AzureBlobSettings"));

        //this for identity and jwt when needed
        services.AddIdentityCore<User>()
            .AddRoles<IdentityRole>()
            .AddTokenProvider<DataProtectorTokenProvider<User>>("ShattibTokenProvidor")
            .AddEntityFrameworkStores<TemplateDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            // Default Password settings.
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 8;
            options.Password.RequiredUniqueChars = 0;
        });

        services.AddScoped<ICategoriesSeeder, CategoriesSeeder>();
        services.AddScoped<ISeeder, RolesSeeder>();

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ISpecificationRepository, SpecificationRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ISeededValuesRepository, SeededValuesRepository>();
        services.AddScoped<IConsultationRepository, ConsultationRepository>();
        services.AddScoped<IStatisticsRepository, StatisticsRepository>();

        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<ICriteriaRepository, CriteriaRepository>();
        services.AddScoped<ICriteriaBillsRepository, CriteriaBillsRepository>();
        services.AddScoped<IFileService, BlobStorageFileService>();
    }
}