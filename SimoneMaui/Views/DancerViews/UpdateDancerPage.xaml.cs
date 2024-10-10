using SimoneMaui.ViewModels;

namespace SimoneMaui.Views;

public partial class UpdateDancerPage : ContentPage
{
    public UpdateDancerPage(UpdateDancerViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
	}
}