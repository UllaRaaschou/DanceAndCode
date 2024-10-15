using SimoneMaui.ViewModels;

namespace SimoneMaui.Views;

public partial class PostTeamPage : ContentPage
{
	public PostTeamPage(PostTeamViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}