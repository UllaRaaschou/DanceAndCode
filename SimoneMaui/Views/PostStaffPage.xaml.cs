using SimoneMaui.ViewModels;

namespace SimoneMaui.Views;

public partial class PostStaffPage : ContentPage
{
	public PostStaffPage(PostStaffViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}