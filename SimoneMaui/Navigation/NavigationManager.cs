using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimoneMaui.Navigation
{
    public class NavigationManager
    {
        private readonly INavigationService _navigationService;
        private readonly Stack<string> _backNavigationHistory;
        private readonly Stack<string> _forwardNavigationHistory;

        public NavigationManager(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _backNavigationHistory = new Stack<string>();
            _forwardNavigationHistory = new Stack<string>();
        }

        public async Task NavigateToFirstPage()
        {
            await Shell.Current.GoToAsync("///firstPage");
        }

        public async Task NavigateBack()
        {
            var currentLocation = Shell.Current.CurrentState.Location.ToString();
            _backNavigationHistory.Push(currentLocation);

            if (_backNavigationHistory.Count > 1)
            {
                _backNavigationHistory.Pop(); // Remove current location
                var previousPage = _backNavigationHistory.Peek(); // Get the previous page
                await Shell.Current.GoToAsync($"///{previousPage}");
            }
            else
            {
                await Shell.Current.GoToAsync("///firstPage");
            }

            _forwardNavigationHistory.Push(currentLocation);
        }

        public async Task NavigateForward()
        {
            if (_forwardNavigationHistory.Count > 0)
            {
                var nextPage = _forwardNavigationHistory.Pop();
                var currentLocation = Shell.Current.CurrentState.Location.ToString();
                _backNavigationHistory.Push(currentLocation);

                await Shell.Current.GoToAsync($"///{nextPage}");

                _backNavigationHistory.Push(currentLocation);
            }
        }

        public bool CanNavigateBack()
        {
            return _backNavigationHistory.Count > 0;
        }

        public bool CanNavigateForward()
        {
            return _forwardNavigationHistory.Count > 0;
        }
    }
}

