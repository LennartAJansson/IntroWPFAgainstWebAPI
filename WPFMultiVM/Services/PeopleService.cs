using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using WPFMultiVM.Models;

namespace WPFMultiVM.Services
{
    public class PeopleService
    {
        private readonly ILogger<PeopleService> logger;
        private readonly HttpClient client;

        public PeopleService(ILogger<PeopleService> logger, HttpClient client)
        {
            this.logger = logger;
            this.client = client;
        }

        public async Task<IEnumerable<Person>> GetPeopleAsync()
        {
            var response = await client.GetAsync("people").ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<IEnumerable<Person>>(json);
        }
    }
}