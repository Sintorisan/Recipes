using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;
using Recipes.Application.Interfaces;
using Recipes.Application.Services;
using Recipes.Infrastructure.Data;
using Recipes.Infrastructure.Interfaces;
using Recipes.Infrastructure.Repositories;
using RecipeStorage.Application.Services;

namespace Recipes.API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddDbContext<RecipeDb>(opt =>
        {
            opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        });

        services.AddCors(opt =>
        {
            opt.AddPolicy("CorsPolicy", policy =>
            {
                policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
            });
        });

        var kernel = Kernel.CreateBuilder().AddOpenAIChatCompletion(config["DEPLOYMENT_MODEL"]!, config["OPEN_AI_KEY"]!).Build();

        services.AddSingleton(kernel);

        services.AddScoped<ISemanticKernelService, SemanticKernelService>();
        services.AddScoped<IRecipeRepository, RecipeRepository>();
        services.AddScoped<IHtmlService, HtmlService>();

        return services;
    }
}