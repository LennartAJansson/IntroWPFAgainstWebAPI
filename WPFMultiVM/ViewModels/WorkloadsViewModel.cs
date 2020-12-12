using GalaSoft.MvvmLight;

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

        public WorkloadsViewModel(ILogger<WorkloadsViewModel> logger, WorkloadsService service)
        {
            this.logger = logger;
            this.service = service;
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

        internal async Task InitializeAsync()
        {
            //TODO Initialize WorkloadsViewModel
            logger.LogWarning("Initializing");
            Visible = true;
            Workloads = new List<Workload>(await service.GetOpenWorkloadsAsync().ConfigureAwait(false));
        }
    }
}