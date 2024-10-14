using Blazored.LocalStorage;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.Extensions.Logging;
using Recipes.Components.Pages;
using Recipes.Components.UIComponents;
using Recipes.Services;

namespace Recipes
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddBlazoredLocalStorage();

            builder.Services
                .AddBlazorise()
                .AddBootstrapProviders()
                .AddFontAwesomeIcons();

            builder.Services.AddHttpClient<ApiService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5172/");
            });

            builder.Services.AddScoped<AddRecipeLogic>();
            builder.Services.AddScoped<RecipeOverviewLogic>();
            builder.Services.AddScoped<RecipePageLogic>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}