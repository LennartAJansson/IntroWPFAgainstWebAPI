using GalaSoft.MvvmLight;

using Microsoft.Extensions.Logging;

using System.Threading.Tasks;

namespace WPFMultiVM.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ILogger<MainViewModel> logger;

        public MainViewModel(ILogger<MainViewModel> logger)
        {
            this.logger = logger;
        }

        internal Task InitializeAsync()
        {
            //TODO Initialize MainViewModel
            logger.LogWarning("Initializing");
            return Task.CompletedTask;
        }
    }
}