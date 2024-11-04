using SimoneMaui.ViewModels;

namespace SimoneMaui.Views;

public partial class PostDancerPage : ContentPage
{
	public PostDancerPage(PostDancerViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		vm.DancerPosted += OnDancerPosted;
    }

	public void OnDancerPosted(string message) 
	{
		DisplayAlert("Besked", message, "OK");

	}
}