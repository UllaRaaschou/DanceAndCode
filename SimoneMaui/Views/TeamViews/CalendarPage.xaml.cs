using SimoneMaui.ViewModels;

namespace SimoneMaui.Views.TeamViews;

public partial class CalendarPage : ContentPage
{
	public CalendarPage(CalendarViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}