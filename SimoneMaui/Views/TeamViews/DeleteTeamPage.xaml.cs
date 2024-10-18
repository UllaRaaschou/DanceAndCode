using SimoneMaui.ViewModels;

namespace SimoneMaui.Views;

public partial class DeleteTeamPage : ContentPage
{
	public DeleteTeamPage(DeleteTeamViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		vm.TeamDeleted += OnTeamDeleted;
	}    
    private void OnTeamDeleted(string message)
    {
        DisplayAlert("Besked", message, "OK"); // Vis besked
    }

}