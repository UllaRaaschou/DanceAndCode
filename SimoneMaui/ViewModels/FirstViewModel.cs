using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json.Linq;
using SimoneMaui.Navigation;

namespace SimoneMaui.ViewModels
{
    public partial class FirstViewModel: ObservableObject, IQueryAttributable
    {
        public INavigationService NavigationService { get; set; }
        

        [ObservableProperty]
        new AsyncRelayCommand workWithDancerCommand;

        [ObservableProperty]
        new AsyncRelayCommand workWithTeamCommand;

        [ObservableProperty]
        new AsyncRelayCommand workWithStaffCommand;

        [ObservableProperty]
        new AsyncRelayCommand postDancerThemeCommand;

        [ObservableProperty]
        new AsyncRelayCommand updateOrDeleteDancerCommand;

        [ObservableProperty]
        new AsyncRelayCommand postTeamThemeCommand;

        [ObservableProperty]
        new AsyncRelayCommand updateOrDeleteTeamCommand;

        [ObservableProperty]
        new AsyncRelayCommand postStaffThemeCommand;

        [ObservableProperty]
        new AsyncRelayCommand updateOrDeleteStaffCommand;

        [ObservableProperty]
        new AsyncRelayCommand getWorkingHoursCommand;

        [ObservableProperty]
        public bool workingWithDancer = false;

        [ObservableProperty]
        public bool workingWithTeam = false;

        [ObservableProperty]
        private bool workingWithStaff = false;

        [ObservableProperty]
        private bool workingThemeIsToBeDecided = true;

        private bool WorkingWithWorkingHours = false;


        public FirstViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            WorkWithDancerCommand = new AsyncRelayCommand(WorkWithDancer);
            WorkWithTeamCommand = new AsyncRelayCommand(WorkWithTeam);
            WorkWithStaffCommand = new AsyncRelayCommand(WorkWithStaff);
            PostDancerThemeCommand = new AsyncRelayCommand(ToPostDancerTheme);
            UpdateOrDeleteDancerCommand = new AsyncRelayCommand(ToUpdateOrDeleteDancer);
            PostTeamThemeCommand = new AsyncRelayCommand(ToPostTeamTheme);
            UpdateOrDeleteTeamCommand = new AsyncRelayCommand(ToUpdateOrDeleteTeam);
            PostStaffThemeCommand = new AsyncRelayCommand(ToPostStaffTheme);
            UpdateOrDeleteStaffCommand = new AsyncRelayCommand(ToUpdateOrDeleteStaff);
            GetWorkingHoursCommand = new AsyncRelayCommand(ToGetWorkingHours);

        }

        private async Task ToUpdateOrDeleteStaff()
        {
            WorkingThemeIsToBeDecided = false;
            await NavigationService.GoToSearchStaff();
        }

        private async Task ToGetWorkingHours()
        {
            WorkingWithWorkingHours = true;
            WorkingThemeIsToBeDecided = false;
            await NavigationService.GoToSearchStaff();
        }

        private async Task ToPostStaffTheme()
        {
            WorkingThemeIsToBeDecided = false;
            await NavigationService.GoToPostStaff();
        }


        private async Task WorkWithStaff()
        {
            WorkingThemeIsToBeDecided = false;
            WorkingWithStaff = true;
        }

        private async Task ToUpdateOrDeleteTeam()
        {
            WorkingThemeIsToBeDecided = false;
            await NavigationService.GoToSearchTeam();
        }

        private async Task ToPostTeamTheme()
        {
            WorkingThemeIsToBeDecided = false;
            await NavigationService.GoToPostTeam();
        }

        private async Task ToUpdateOrDeleteDancer()
        {
            WorkingThemeIsToBeDecided = false;
            await NavigationService.GoToSearchDancer();
        }

        private async Task ToPostDancerTheme()
        {
            WorkingThemeIsToBeDecided = false;
            await NavigationService.GoToPostDancer();
        }

        private async Task WorkWithDancer()
        {
            WorkingThemeIsToBeDecided = false;
            WorkingWithDancer = true;            
        }

        private async Task WorkWithTeam()
        {
            WorkingThemeIsToBeDecided = false;
            WorkingWithTeam = true;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("unique"))
            {
                var uniqueValue = query["unique"] as string;
                WorkingThemeIsToBeDecided = true;
                WorkingWithDancer = false;
                WorkingWithStaff = false;
                WorkingWithTeam = false;
                
                // Du kan tilføje logik her, hvis du har brug for at håndtere den unikke parameter
            }
        }



       
    }
}
