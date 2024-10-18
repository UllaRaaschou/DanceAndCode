namespace SimoneMaui.Views;
using SimoneMaui.ViewModels;

public partial class SearchTeamPage : ContentPage
{
	public SearchTeamPage(SearchTeamViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		viewModel.TeamNotFound += OnTeamNotFound;        
    }

    private void OnTeamNotFound(string message)
    {
        DisplayAlert("Besked", message, "OK"); // Vis besked
    }
}