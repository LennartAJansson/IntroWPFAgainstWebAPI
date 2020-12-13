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

        public async Task<IEnumerable<Workload>> GetWorkloadsAsync(int personId = 0, int assignmentId = 0, bool onlyOpen = true)
        {
            string subPath = $"workloads";

            if (onlyOpen)
                subPath += $"/{personId}&{assignmentId}";

            var response = await client.GetAsync(subPath).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<IEnumerable<Workload>>(json, options);
        }
    }
}