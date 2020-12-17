using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using WPFMultiVM.Models;

namespace WPFMultiVM.Services
{
    public class AssignmentsService
    {
        private readonly ILogger<AssignmentsService> logger;
        private readonly HttpClient client;

        public AssignmentsService(ILogger<AssignmentsService> logger, HttpClient client)
        {
            this.logger = logger;
            this.client = client;
        }

        public async Task<IEnumerable<Assignment>> GetAssignmentsAsync()
        {
            logger.LogWarning("Getting assignments");
            var response = await client.GetAsync("assignments").ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<IEnumerable<Assignment>>(json, options);
        }

        public async Task<Assignment> AddAssignmentAsync(Assignment assignment)
        {
            string subPath = $"assignments";
            string json = JsonSerializer.Serialize(assignment);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(subPath, content).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<Assignment>(json, options);
        }

        public async Task UpdateAssignmentAsync(Assignment assignment)
        {
            string subPath = $"assignments";
            string json = JsonSerializer.Serialize(assignment);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(subPath, content).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
        }
    }
}