using GalaSoft.MvvmLight;

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

        internal async Task InitializeAsync()
        {
            //TODO Initialize AssignmentsViewModel
            logger.LogWarning("Initializing");
            Assignments = new List<Assignment>(await service.GetAssignmentsAsync().ConfigureAwait(false));
        }
    }
}