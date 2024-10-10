namespace SimoneMaui.Views;
using SimoneMaui.ViewModels;

public partial class SearchTeamPage : ContentPage
{
	public SearchTeamPage(SearchTeamViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}