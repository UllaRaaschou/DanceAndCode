using SimoneMaui.ViewModels.TeamViewModels;

namespace SimoneMaui.Views.TeamViews;

public partial class DeleteDancerFromTeamPage : ContentPage
{
	public DeleteDancerFromTeamPage(DeleteDancerFromTeamViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}