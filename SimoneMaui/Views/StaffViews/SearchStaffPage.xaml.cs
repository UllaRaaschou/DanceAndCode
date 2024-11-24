using SimoneMaui.ViewModels.StaffViewModels;

namespace SimoneMaui.Views;

public partial class SearchStaffPage : ContentPage
{    public SearchStaffPage(SearchStaffViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}