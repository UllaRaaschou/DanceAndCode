using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using SimoneMaui.Navigation;
using SimoneMaui.ViewModels;
using SimoneMaui.ViewModels.StaffViewModels;
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

            builder.Services.AddTransient<PostDancerViewModel>();
            builder.Services.AddTransient<PostDancerPage>();

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

            builder.Services.AddTransient<FirstViewModel>();
            builder.Services.AddTransient<FirstPage>();

            builder.Services.AddTransient<CalendarViewModel>();
            builder.Services.AddTransient<CalendarPage>();

            builder.Services.AddTransient<PostStaffViewModel>();
            builder.Services.AddTransient<PostStaffPage>();

            builder.Services.AddTransient<UpdateStaffViewModel>();
            builder.Services.AddTransient<UpdateStaffPage>();

            builder.Services.AddTransient<SearchStaffViewModel>();
            builder.Services.AddTransient<SearchStaffPage>();

            builder.Services.AddTransient<DeleteStaffViewModel>();
            builder.Services.AddTransient<DeleteStaffPage>();

            builder.Services.AddTransient<GetWorkingHoursViewModel>();
            builder.Services.AddTransient<GetWorkingHoursPage>();

            


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
