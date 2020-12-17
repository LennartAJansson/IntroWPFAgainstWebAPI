using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WPFMultiVM.Models
{
    public class Assignment
    {
        public int AssignmentId { get; set; }
        public string Customer { get; set; }
        public string Description { get; set; }

        //Browsing property:
        [JsonIgnore]
        public virtual ICollection<Workload> Workloads { get; set; } = new HashSet<Workload>();

        public override string ToString() => $"{AssignmentId} - {Customer} {Description}";
    }
}