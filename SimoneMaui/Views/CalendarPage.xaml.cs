using SimoneMaui.ViewModels;

namespace SimoneMaui.Views;

public partial class CalendarPage : ContentPage
{
	public CalendarPage(CalendarViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}