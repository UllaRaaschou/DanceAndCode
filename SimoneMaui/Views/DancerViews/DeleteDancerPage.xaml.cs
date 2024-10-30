using SimoneMaui.ViewModels;

namespace SimoneMaui.Views;

public partial class DeleteDancerPage : ContentPage
{
	public DeleteDancerPage(DeleteDancerViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		vm.DancerDeleted += OnDancerDeleted;
	}


	public void OnDancerDeleted(string message) 
	{
		DisplayAlert("Besked", message, "OK");
	}
	
}