using System.Collections.Generic;

namespace WPFMultiVM.Models
{
    public class Assignment
    {
        public int AssignmentId { get; set; }
        public string Customer { get; set; }
        public string Description { get; set; }

        //Browsing property:
        public virtual ICollection<Workload> Workloads { get; set; } = new HashSet<Workload>();
    }
}