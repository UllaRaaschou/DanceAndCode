using SimoneMaui.Navigation;
using System.Diagnostics.Tracing;

namespace SimoneMaui
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        private INavigationService NavigationService;

        public MainPage(INavigationService navigationService )
        {
            InitializeComponent();
            NavigationService = navigationService;
        }

        private void OnSearchDancerClicked(object sender, EventArgs e)
        {
            this.NavigationService.GoToSearchDancer();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
