using GalaSoft.MvvmLight;

using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.Threading.Tasks;

using WPFMultiVM.Models;
using WPFMultiVM.Services;

namespace WPFMultiVM.ViewModels
{
    public class PeopleViewModel : ViewModelBase
    {
        private readonly ILogger<PeopleViewModel> logger;
        private readonly PeopleService service;

        public PeopleViewModel(ILogger<PeopleViewModel> logger, PeopleService service)
        {
            this.logger = logger;
            this.service = service;
        }

        public List<Person> People
        {
            get => people;
            set => Set(nameof(People), ref people, value, true);
        }

        private List<Person> people;

        public Person SelectedPerson
        {
            get => selectedPerson;
            set => Set(nameof(SelectedPerson), ref selectedPerson, value, true);
        }

        private Person selectedPerson;

        public bool Visible
        {
            get => visible;
            set => Set(nameof(Visible), ref visible, value, true);
        }

        private bool visible;

        internal async Task InitializeAsync()
        {
            //TODO Initialize PeopleViewModel
            logger.LogWarning("Initializing");
            People = new List<Person>(await service.GetPeopleAsync().ConfigureAwait(false));
        }
    }
}