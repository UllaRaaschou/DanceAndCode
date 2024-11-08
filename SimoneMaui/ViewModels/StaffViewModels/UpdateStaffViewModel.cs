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
    public class UpdateStaffViewModel
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(UpdateStaffCommand))]
        private MauiJobRoleEnum role = MauiJobRoleEnum.None;

        public AsyncRelayCommand UpdateStaffCommand { get; }

        public IReadOnlyList<string> Jobroles { get; } = Enum.GetNames(typeof(MauiJobRoleEnum));
    }
}
