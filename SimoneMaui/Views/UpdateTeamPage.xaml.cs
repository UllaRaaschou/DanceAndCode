using SimoneMaui.ViewModels;

namespace SimoneMaui.Views;

public partial class UpdateTeamPage : ContentPage
{
    public UpdateTeamPage(UpdateTeamViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}