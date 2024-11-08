using SimoneMaui.ViewModels.StaffViewModels;

namespace SimoneMaui.Views;

public partial class SearchStaffPage : ContentPage
{
    //public SearchStaffPage()
    //{
    //    InitializeComponent();
    //}
    public SearchStaffPage(SearchStaffViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}