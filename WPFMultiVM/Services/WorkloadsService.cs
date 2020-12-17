using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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
            logger.LogWarning("Getting workloads");
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

        public async Task<Workload> AddWorkloadAsync(Workload workload)
        {
            if (workload.Person != null)
                if (workload.PersonId == 0)
                    workload.PersonId = workload.Person.PersonId;

            if (workload.Assignment != null)
                if (workload.AssignmentId == 0)
                    workload.AssignmentId = workload.Assignment.AssignmentId;

            string subPath = $"workloads";
            string json = JsonSerializer.Serialize(workload);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(subPath, content).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<Workload>(json, options);
        }

        public async Task UpdateWorkloadAsync(Workload workload)
        {
            string subPath = $"workloads";
            string json = JsonSerializer.Serialize(workload);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(subPath, content).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
        }
    }
}