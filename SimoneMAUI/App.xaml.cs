using SimoneMaui.Views;

namespace SimoneMaui
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new UpdateDancerPage();
        }
    }
}
