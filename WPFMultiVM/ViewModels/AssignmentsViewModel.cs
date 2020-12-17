using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.Threading.Tasks;

using WPFMultiVM.Models;
using WPFMultiVM.Services;

namespace WPFMultiVM.ViewModels
{
    public class AssignmentsViewModel : ViewModelBase
    {
        private readonly ILogger<AssignmentsViewModel> logger;
        private readonly AssignmentsService service;

        public AssignmentsViewModel(ILogger<AssignmentsViewModel> logger, AssignmentsService service)
        {
            this.logger = logger;
            this.service = service;
            OkCommand = new RelayCommand(async () => await OkCommandAsync());
            CancelCommand = new RelayCommand(async () => await CancelCommandAsync());
        }

        public List<Assignment> Assignments
        {
            get => assignments;
            set => Set(nameof(Assignments), ref assignments, value, true);
        }

        private List<Assignment> assignments;

        public Assignment SelectedAssignment
        {
            get => selectedAssignment;
            set => Set(nameof(SelectedAssignment), ref selectedAssignment, value, true);
        }

        private Assignment selectedAssignment;

        public bool Visible
        {
            get => visible;
            set => Set(nameof(Visible), ref visible, value, true);
        }

        private bool visible;

        public RelayCommand OkCommand { get; }
        public RelayCommand CancelCommand { get; }

        internal async Task InitializeAsync()
        {
            //TODO Initialize AssignmentsViewModel
            logger.LogWarning("Initializing");
            Visible = false;
            Assignments = new List<Assignment>(await service.GetAssignmentsAsync().ConfigureAwait(false));
        }

        private async Task OkCommandAsync()
        {
            if (SelectedAssignment.AssignmentId == 0)
            {
                SelectedAssignment = await service.AddAssignmentAsync(SelectedAssignment).ConfigureAwait(false);
                Assignments.Add(SelectedAssignment);
            }
            else
            {
                await service.UpdateAssignmentAsync(SelectedAssignment).ConfigureAwait(false);
            }
            Visible = false;
        }

        private Task CancelCommandAsync()
        {
            Visible = false;
            SelectedAssignment = null;
            return Task.CompletedTask;
        }
    }
}