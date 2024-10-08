using Microsoft.Extensions.DependencyModel;
using SimoneMaui.Navigation;
using SimoneMaui.ViewModels;
using SimoneMaui.Views;
using System.Diagnostics;

namespace SimoneMaui
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
            
    }
}
