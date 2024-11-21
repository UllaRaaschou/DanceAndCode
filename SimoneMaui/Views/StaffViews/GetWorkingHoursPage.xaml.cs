using SimoneMaui.ViewModels.StaffViewModels;

namespace SimoneMaui.Views;

public partial class GetWorkingHoursPage : ContentPage
{
	public GetWorkingHoursPage(GetWorkingHoursViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}