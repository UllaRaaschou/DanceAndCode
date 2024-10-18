namespace SimoneMaui.Views;
using SimoneMaui.ViewModels;

public partial class FirstPage : ContentPage
{
	public FirstPage(FirstViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}