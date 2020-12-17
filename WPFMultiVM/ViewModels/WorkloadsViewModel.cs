using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using Microsoft.Extensions.Logging;

using System;
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
        private readonly PeopleService peopleService;
        private readonly AssignmentsService assignmentsService;
        private readonly PeopleViewModel peopleVm;
        private readonly AssignmentsViewModel assignmentsVm;

        public bool Visible
        {
            get => visible;
            set => Set(nameof(Visible), ref visible, value, true);
        }

        private bool visible;

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

        public List<Person> People
        {
            get => people;
            set => Set(nameof(People), ref people, value, true);
        }

        private List<Person> people;

        public List<Assignment> Assignments
        {
            get => assignments;
            set => Set(nameof(Assignments), ref assignments, value, true);
        }

        private List<Assignment> assignments;

        public RelayCommand AddPersonCommand { get; }
        public RelayCommand AddAssignmentCommand { get; }
        public RelayCommand EditPersonCommand { get; }
        public RelayCommand EditAssignmentCommand { get; }
        public RelayCommand AddCurrentDateTimeCommand { get; }
        public RelayCommand OkCommand { get; }
        public RelayCommand CancelCommand { get; }

        public WorkloadsViewModel(ILogger<WorkloadsViewModel> logger,
                                  WorkloadsService service, PeopleService peopleService, AssignmentsService assignmentsService,
                                  PeopleViewModel peopleVm, AssignmentsViewModel assignmentsVm)
        {
            this.logger = logger;
            this.service = service;
            this.peopleService = peopleService;
            this.assignmentsService = assignmentsService;
            this.peopleVm = peopleVm;
            this.assignmentsVm = assignmentsVm;
            AddPersonCommand = new RelayCommand(async () => await AddPersonCommandAsync());
            AddAssignmentCommand = new RelayCommand(async () => await AddAssignmentCommandAsync());
            EditPersonCommand = new RelayCommand(async () => await EditPersonCommandAsync());
            EditAssignmentCommand = new RelayCommand(async () => await EditAssignmentCommandAsync());
            AddCurrentDateTimeCommand = new RelayCommand(async () => await AddCurrentDateTimeCommandAsync());
            OkCommand = new RelayCommand(async () => await OkCommandAsync());
            CancelCommand = new RelayCommand(async () => await CancelCommandAsync());
        }

        internal async Task InitializeAsync()
        {
            logger.LogWarning("Initializing");
            Visible = false;
            People = new List<Person>(await peopleService.GetPeopleAsync().ConfigureAwait(false));
            Assignments = new List<Assignment>(await assignmentsService.GetAssignmentsAsync().ConfigureAwait(false));
        }

        private async Task AddPersonCommandAsync()
        {
            SelectedWorkload.Person = new Person();
            await EditPersonCommandAsync();
        }

        private async Task AddAssignmentCommandAsync()
        {
            SelectedWorkload.Assignment = new Assignment();
            await EditAssignmentCommandAsync();
        }

        private Task EditPersonCommandAsync()
        {
            peopleVm.SelectedPerson = SelectedWorkload.Person;
            peopleVm.Visible = true;
            return Task.CompletedTask;
        }

        private Task EditAssignmentCommandAsync()
        {
            assignmentsVm.SelectedAssignment = SelectedWorkload.Assignment;
            assignmentsVm.Visible = true;
            return Task.CompletedTask;
        }

        private Task AddCurrentDateTimeCommandAsync()
        {
            SelectedWorkload.Start = DateTime.Now;
            RaisePropertyChanged(nameof(SelectedWorkload));
            return Task.CompletedTask;
        }

        private async Task OkCommandAsync()
        {
            if (SelectedWorkload.WorkloadId == 0)
            {
                SelectedWorkload = await service.AddWorkloadAsync(SelectedWorkload).ConfigureAwait(false);
                Workloads.Add(SelectedWorkload);
            }
            else
            {
                await service.UpdateWorkloadAsync(SelectedWorkload).ConfigureAwait(false);
            }
            Visible = false;
        }

        private Task CancelCommandAsync()
        {
            Visible = false;
            SelectedWorkload = null;
            return Task.CompletedTask;
        }
    }
}