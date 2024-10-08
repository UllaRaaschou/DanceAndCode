﻿using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using SimoneMaui.Navigation;
using SimoneMaui.ViewModels;
using SimoneMaui.Views;

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

            builder.Services.AddTransient<UpdateDancerViewmodel>(); 
            builder.Services.AddTransient<UpdateDancerPage>();
            

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
