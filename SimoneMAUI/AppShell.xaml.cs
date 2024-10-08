using SimoneMaui.Navigation;

namespace SimoneMaui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            NavigationService.ConfigureRouter();

        }
    }
}
