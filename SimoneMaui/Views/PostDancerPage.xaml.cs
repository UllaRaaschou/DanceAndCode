using SimoneMaui.ViewModels;

namespace SimoneMaui.Views;

public partial class PostDancerPage : ContentPage
{
	public PostDancerPage()
	{
		InitializeComponent();
        BindingContext = new PostDancerViewModel();
    }
}