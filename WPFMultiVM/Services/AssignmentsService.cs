using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.Net.Http;
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
            var response = await client.GetAsync("assignments").ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<IEnumerable<Assignment>>(json);
        }
    }
}