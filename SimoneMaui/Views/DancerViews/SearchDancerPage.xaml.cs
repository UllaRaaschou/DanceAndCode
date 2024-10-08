using SimoneMaui.Navigation;
using SimoneMaui.ViewModels;

namespace SimoneMaui.Views;

public partial class SearchDancerPage : ContentPage
{
	public SearchDancerPage(SearchDancerViewmodel viewmodel)
	{
		InitializeComponent();

		//var viewmodel = new SearchDancerViewmodel( DependencyService.Get<INavigationService>());
		BindingContext = viewmodel;
	}
}