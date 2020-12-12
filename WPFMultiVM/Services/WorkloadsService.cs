using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using WPFMultiVM.Models;

namespace WPFMultiVM.Services
{
    public class WorkloadsService
    {
        private readonly ILogger<WorkloadsService> logger;
        private readonly HttpClient client;

        public WorkloadsService(ILogger<WorkloadsService> logger, HttpClient client)
        {
            this.logger = logger;
            this.client = client;
        }

        public async Task<IEnumerable<Workload>> GetOpenWorkloadsAsync(int personId = 0, int assignmentId = 0)
        {
            var response = await client.GetAsync($"workloads/{personId}&{assignmentId}").ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<IEnumerable<Workload>>(json);
        }
    }
}