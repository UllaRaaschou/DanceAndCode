using SimoneMaui.ViewModels;

namespace SimoneMaui.Views;

public partial class UpdateDancerPage : ContentPage
{
    public UpdateDancerPage(UpdateDancerViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
		viewmodel.TellUserToChoseTeam += OnTellUserToChoseTeamChosen;
		viewmodel.ErrorInDate += OnDateError;
	}

	public void OnTellUserToChoseTeamChosen(string message) 
	{
		DisplayAlert("Besked", message, "OK");
	}

    public void OnDateError(string message)
    {
        DisplayAlert("Besked", message, "OK");
    }
}