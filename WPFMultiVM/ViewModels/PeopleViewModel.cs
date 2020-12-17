using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

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
            OkCommand = new RelayCommand(async () => await OkCommandAsync());
            CancelCommand = new RelayCommand(async () => await CancelCommandAsync());
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

        public RelayCommand OkCommand { get; }
        public RelayCommand CancelCommand { get; }

        internal async Task InitializeAsync()
        {
            logger.LogWarning("Initializing");
            Visible = false;
            People = new List<Person>(await service.GetPeopleAsync().ConfigureAwait(false));
        }

        private async Task OkCommandAsync()
        {
            if (SelectedPerson.PersonId == 0)
            {
                SelectedPerson = await service.AddPersonAsync(SelectedPerson).ConfigureAwait(false);
                People.Add(SelectedPerson);
            }
            else
            {
                await service.UpdatePersonAsync(SelectedPerson).ConfigureAwait(false);
            }
            Visible = false;
        }

        private Task CancelCommandAsync()
        {
            Visible = false;
            SelectedPerson = null;
            return Task.CompletedTask;
        }
    }
}