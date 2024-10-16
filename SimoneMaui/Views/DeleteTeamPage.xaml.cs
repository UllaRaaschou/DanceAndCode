using SimoneMaui.ViewModels;

namespace SimoneMaui.Views;

public partial class DeleteTeamPage : ContentPage
{
	public DeleteTeamPage(DeleteTeamViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}