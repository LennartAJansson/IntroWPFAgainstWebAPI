using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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
            logger.LogWarning("Getting people");
            var response = await client.GetAsync("people").ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<IEnumerable<Person>>(json, options);
        }

        public async Task<Person> AddPersonAsync(Person person)
        {
            string subPath = $"people";
            string json = JsonSerializer.Serialize(person);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(subPath, content).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<Person>(json, options);
        }

        public async Task UpdatePersonAsync(Person person)
        {
            string subPath = $"people";
            string json = JsonSerializer.Serialize(person);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(subPath, content).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
        }
    }
}