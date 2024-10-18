using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimoneMaui.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimoneMaui.ViewModels
{
    public partial class FirstViewModel: ObservableObject, IQueryAttributable
    {
        public INavigationService NavigationService { get; set; }

        [ObservableProperty]
        new AsyncRelayCommand workWithDancerCommand;

        [ObservableProperty]
        new AsyncRelayCommand workWithTeamCommand;


        public FirstViewModel()
        {
            WorkWithDancerCommand = new AsyncRelayCommand(WorkWithDancer);
            WorkWithTeamCommand = new AsyncRelayCommand(WorkWithTeam);
        }
        private async Task WorkWithDancer()
        {
            await NavigationService.GoToSearchDancer();
        }

        private async Task WorkWithTeam()
        {
            await NavigationService.GoToSearchTeam();
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            throw new NotImplementedException();
        }
    }
}
