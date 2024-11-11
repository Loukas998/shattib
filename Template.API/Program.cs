using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using Template.API.Extensions;
using Template.API.Middlewares;
using Template.Application.Extensions;
using Template.Infrastructure.Extensions;
using Template.Infrastructure.Seeders;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.AddPresentation();
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);

    builder.Host.UseSerilog(
        (context, configuration) => { configuration.ReadFrom.Configuration(context.Configuration); });

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll",
            b => b.AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowAnyMethod());
    });

    var app = builder.Build();

    var scope = app.Services.CreateScope(); //for seeders
    // example: var govSeeder = scope.ServiceProvider.GetRequiredService<IGovernorateSeeder>();
    var seeder = scope.ServiceProvider.GetRequiredService<ISeeder>();
    await seeder.Seed();
    var catSedder = scope.ServiceProvider.GetRequiredService<ICategoriesSeeder>();
    await catSedder.Seed();
    app.UseMiddleware<ErrorHandlerMiddleware>();
    app.UseMiddleware<TimeLoggingMiddleware>();
    app.UseMiddleware<TranslationMiddleware>();
	// Configure the HTTP request pipeline.
	if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseSerilogRequestLogging();

    app.UseHttpsRedirection();

    //app.MapGroup("api/identity").WithTags("Identity").MapIdentityApi<User>();


    //app.UseStaticFiles(new StaticFileOptions
    //{
    //	FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Images")),
    //	RequestPath = "/Images"
    //});

    app.UseStaticFiles();
    app.UseCors("AllowAll");
    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();
    app.MapFallbackToFile("index.html");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application startup failed");
}
finally
{
    Log.CloseAndFlush();
}