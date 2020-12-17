using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.Threading.Tasks;

using WPFMultiVM.Models;
using WPFMultiVM.Services;

namespace WPFMultiVM.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ILogger<MainViewModel> logger;
        private readonly WorkloadsService service;
        private readonly WorkloadsViewModel workloadsVm;

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

        public RelayCommand<int> StopWorkloadCommand { get; }
        public RelayCommand NewWorkloadCommand { get; }

        public MainViewModel(ILogger<MainViewModel> logger, WorkloadsService service, WorkloadsViewModel workloadsVm)
        {
            this.logger = logger;
            this.service = service;
            this.workloadsVm = workloadsVm;
            StopWorkloadCommand = new RelayCommand<int>(async (int p) => await StopWorkloadCommandAsync(p));
            NewWorkloadCommand = new RelayCommand(async () => await NewWorkloadCommandAsync());
        }

        internal async Task InitializeAsync()
        {
            logger.LogWarning("Initializing");
            Visible = true;
            Workloads = new List<Workload>(await service.GetWorkloadsAsync().ConfigureAwait(false));
        }

        private async Task StopWorkloadCommandAsync(int workloadId)
        {
            //Skicka till service att denna workload är uppdaterad
            await service.UpdateWorkloadAsync(Workloads.Find(w => w.WorkloadId == workloadId));
            await InitializeAsync();
        }

        private Task NewWorkloadCommandAsync()
        {
            workloadsVm.SelectedWorkload = new Workload();
            workloadsVm.Visible = true;
            return Task.CompletedTask;
        }
    }
}