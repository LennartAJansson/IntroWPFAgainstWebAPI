using Microsoft.Extensions.DependencyInjection;

using System.Threading.Tasks;

namespace WPFMultiVM.ViewModels
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel => GetMainViewModelAsync().GetAwaiter().GetResult();
        public AssignmentsViewModel AssignmentsViewModel => GetAssignmentsViewModelAsync().GetAwaiter().GetResult();
        public PeopleViewModel PeopleViewModel => GetPeopleViewModelAsync().GetAwaiter().GetResult();
        public WorkloadsViewModel WorkloadsViewModel => GetWorkloadsViewModelAsync().GetAwaiter().GetResult();

        public static async Task<MainViewModel> GetMainViewModelAsync()
        {
            MainViewModel vm = App.ServiceProvider.GetRequiredService<MainViewModel>();
            await vm.InitializeAsync().ConfigureAwait(false);
            return vm;
        }

        public static async Task<AssignmentsViewModel> GetAssignmentsViewModelAsync()
        {
            AssignmentsViewModel vm = App.ServiceProvider.GetRequiredService<AssignmentsViewModel>();
            await vm.InitializeAsync().ConfigureAwait(false);
            return vm;
        }

        public static async Task<PeopleViewModel> GetPeopleViewModelAsync()
        {
            PeopleViewModel vm = App.ServiceProvider.GetRequiredService<PeopleViewModel>();
            await vm.InitializeAsync().ConfigureAwait(false);
            return vm;
        }

        public static async Task<WorkloadsViewModel> GetWorkloadsViewModelAsync()
        {
            WorkloadsViewModel vm = App.ServiceProvider.GetRequiredService<WorkloadsViewModel>();
            await vm.InitializeAsync().ConfigureAwait(false);
            return vm;
        }
    }
}