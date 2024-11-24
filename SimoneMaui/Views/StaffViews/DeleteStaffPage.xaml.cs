using SimoneMaui.ViewModels.StaffViewModels;

namespace SimoneMaui.Views;

public partial class DeleteStaffPage : ContentPage
{
	public DeleteStaffPage(DeleteStaffViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
		vm.StaffDeleted += OnStaffDeleted;
    }

	public void OnStaffDeleted(string message) 
	{
        DisplayAlert("Besked", message, "OK");
    }
}