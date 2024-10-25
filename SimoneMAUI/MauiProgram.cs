using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using SimoneMaui.Navigation;
using SimoneMaui.ViewModels;
using SimoneMaui.ViewModels.TeamViewModels;
using SimoneMaui.Views;
using SimoneMaui.Views.TeamViews;

namespace SimoneMaui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<INavigationService, NavigationService>();

            builder.Services.AddTransient<MainPage>();

            builder.Services.AddTransient<SearchDancerViewmodel>();            
            builder.Services.AddTransient<SearchDancerPage>();

            builder.Services.AddTransient<UpdateDancerViewModel>(); 
            builder.Services.AddTransient<UpdateDancerPage>();

            builder.Services.AddTransient<DeleteDancerViewModel>();
            builder.Services.AddTransient<DeleteDancerPage>();


            builder.Services.AddTransient<SearchTeamViewModel>();
            builder.Services.AddTransient<SearchTeamPage>();

            builder.Services.AddTransient<PostTeamViewModel>();
            builder.Services.AddTransient<PostTeamPage>();

            builder.Services.AddTransient<DeleteTeamViewModel>();
            builder.Services.AddTransient<DeleteTeamPage>();

            builder.Services.AddTransient<UpdateTeamViewModel>();
            builder.Services.AddTransient<UpdateTeamPage>();

            builder.Services.AddTransient<DeleteDancerFromTeamViewModel>();
            builder.Services.AddTransient<DeleteDancerFromTeamPage>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
