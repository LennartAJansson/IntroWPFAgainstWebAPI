using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.Threading.Tasks;

using WPFMultiVM.Models;
using WPFMultiVM.Services;

namespace WPFMultiVM.ViewModels
{
    public class WorkloadsViewModel : ViewModelBase
    {
        private readonly ILogger<WorkloadsViewModel> logger;
        private readonly WorkloadsService service;
        private readonly PeopleViewModel peopleVm;
        private readonly AssignmentsViewModel assignmentsVm;

        public WorkloadsViewModel(ILogger<WorkloadsViewModel> logger, WorkloadsService service, PeopleViewModel peopleVm, AssignmentsViewModel assignmentsVm)
        {
            this.logger = logger;
            this.service = service;
            this.peopleVm = peopleVm;
            this.assignmentsVm = assignmentsVm;
            PeopleCommand = new RelayCommand<string>(async (string p) => await PeopleCommandAsync(p));
            AssignmentCommand = new RelayCommand<string>(async (string p) => await AssignmentCommandAsync(p));
        }

        public List<Workload> Workloads
        {
            get => workloads;
            set => Set(nameof(Workloads), ref workloads, value, true);
        }

        private List<Workload> workloads;

        public Workload SelectedWorkload
        {
            get => selectedWorkload;
            set => Set(nameof(SelectedWorkload), ref selectedWorkload, value, true);
        }

        private Workload selectedWorkload;

        public bool Visible
        {
            get => visible;
            set => Set(nameof(Visible), ref visible, value, true);
        }

        private bool visible;

        public RelayCommand<string> PeopleCommand { get; }
        public RelayCommand<string> AssignmentCommand { get; }

        internal async Task InitializeAsync()
        {
            //TODO Initialize WorkloadsViewModel
            logger.LogWarning("Initializing");
            Visible = true;
            Workloads = new List<Workload>(await service.GetWorkloadsAsync().ConfigureAwait(false));
        }

        private Task PeopleCommandAsync(string parameter)
        {
            if (parameter == "new")
                peopleVm.SelectedPerson = new Person();
            else if (parameter == "edit")
                peopleVm.SelectedPerson = SelectedWorkload.Person;

            peopleVm.Visible = true;
            return Task.CompletedTask;
        }

        private Task AssignmentCommandAsync(string parameter)
        {
            if (parameter == "new")
                assignmentsVm.SelectedAssignment = new Assignment();
            else if (parameter == "edit")
                assignmentsVm.SelectedAssignment = SelectedWorkload.Assignment;

            assignmentsVm.Visible = true;
            return Task.CompletedTask;
        }
    }
}