using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimoneMaui.ViewModels.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimoneMaui.ViewModels.StaffViewModels
{
    public partial class UpdateStaffViewModel: ObservableObject, IQueryAttributable
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(UpdateStaffCommand))]
        private MauiJobRoleEnum role = MauiJobRoleEnum.None;

        public AsyncRelayCommand UpdateStaffCommand { get; }

        public IReadOnlyList<string> Jobroles { get; } = Enum.GetNames(typeof(MauiJobRoleEnum));

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private DateOnly timeOfBirth;

        [ObservableProperty]
        private StaffDto selectedStaff;

        public UpdateStaffViewModel() 
        {
            UpdateStaffCommand = new AsyncRelayCommand(UpdateStaff, CanUpdateStaff);
        }

        private bool CanUpdateStaff()
        {
           return selectedStaff != null;
        }

        private async Task UpdateStaff()
        {
            throw new NotImplementedException();
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("staffDto") && query["staffDto"] is StaffDto staffDto)

            {
                SelectedStaff = staffDto;
                Name = staffDto.Name;
                TimeOfBirth = staffDto.TimeOfBirth;
            }
        }
    }
}
