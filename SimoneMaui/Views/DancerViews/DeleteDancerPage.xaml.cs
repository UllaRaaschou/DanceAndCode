using SimoneMaui.ViewModels;

namespace SimoneMaui.Views;

public partial class DeleteDancerPage : ContentPage
{
	public DeleteDancerPage(DeleteDancerViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}