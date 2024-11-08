using SimoneMaui.ViewModels.StaffViewModels;

namespace SimoneMaui.Views;

public partial class UpdateStaffPage : ContentPage
{
	public UpdateStaffPage(UpdateStaffViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}