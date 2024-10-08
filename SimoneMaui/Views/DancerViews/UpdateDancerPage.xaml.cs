using SimoneMaui.ViewModels;

namespace SimoneMaui.Views;

public partial class UpdateDancerPage : ContentPage
{
    public UpdateDancerPage(UpdateDancerViewmodel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
	}
}