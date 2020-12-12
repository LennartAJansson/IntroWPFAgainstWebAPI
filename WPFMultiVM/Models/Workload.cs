using System;

namespace WPFMultiVM.Models
{
    public class Workload
    {
        public int WorkloadId { get; set; }
        public virtual Person Person { get; set; }//Browsing property
        public int PersonId { get; set; }
        public virtual Assignment Assignment { get; set; }//Browsing property
        public int AssignmentId { get; set; }
        public string Comment { get; set; }
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset? Stop { get; set; }
    }
}