using SimoneMaui.Navigation;
using SimoneMaui.ViewModels;

namespace SimoneMaui.Views;

public partial class SearchDancerPage : ContentPage
{
	public SearchDancerPage(SearchDancerViewmodel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
		viewmodel.NoDancerFoundInDb += OnDancerSearched;
	}

	private void OnDancerSearched(string message) 
	{
		DisplayAlert("Besked", message, "OK");
	}
}

