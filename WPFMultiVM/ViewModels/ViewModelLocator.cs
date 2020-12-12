using Microsoft.Extensions.DependencyInjection;

using System.Threading.Tasks;

namespace WPFMultiVM.ViewModels
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel
        {
            get
            {
                return GetMainViewModelAsync().GetAwaiter().GetResult();
            }
        }

        public static async Task<MainViewModel> GetMainViewModelAsync()
        {
            MainViewModel vm = App.ServiceProvider.GetRequiredService<MainViewModel>();
            await vm.InitializeAsync();
            return vm;
        }

        public AssignmentsViewModel AssignmentsViewModel
        {
            get
            {
                return GetAssignmentsViewModelAsync().GetAwaiter().GetResult();
            }
        }

        public static async Task<AssignmentsViewModel> GetAssignmentsViewModelAsync()
        {
            AssignmentsViewModel vm = App.ServiceProvider.GetRequiredService<AssignmentsViewModel>();
            await vm.InitializeAsync().ConfigureAwait(false);
            return vm;
        }

        public PeopleViewModel PeopleViewModel
        {
            get
            {
                return GetPeopleViewModelAsync().GetAwaiter().GetResult();
            }
        }

        public static async Task<PeopleViewModel> GetPeopleViewModelAsync()
        {
            PeopleViewModel vm = App.ServiceProvider.GetRequiredService<PeopleViewModel>();
            await vm.InitializeAsync().ConfigureAwait(false);
            return vm;
        }

        public WorkloadsViewModel WorkloadsViewModel
        {
            get
            {
                return GetWorkloadsViewModelAsync().GetAwaiter().GetResult();
            }
        }

        public static async Task<WorkloadsViewModel> GetWorkloadsViewModelAsync()
        {
            WorkloadsViewModel vm = App.ServiceProvider.GetRequiredService<WorkloadsViewModel>();
            await vm.InitializeAsync().ConfigureAwait(false);
            return vm;
        }
    }
}