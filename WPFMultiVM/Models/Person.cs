using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WPFMultiVM.Models
{
    public class Person
    {
        public int PersonId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Postalcode { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }

        //Browsing property:
        [JsonIgnore]
        public virtual ICollection<Workload> Workloads { get; set; } = new HashSet<Workload>();

        public override string ToString() => $"{PersonId} - {Firstname} {Lastname}";
    }
}