using SimoneMaui.ViewModels;

namespace SimoneMaui.Navigation;
public interface INavigationService
{
    Task GoToSearchDancer();
    Task GoToUpdateDancer(DancerDto dancerDto);
}

