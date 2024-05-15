using System;

namespace ComplaintMngSys.Models
{
    public class Complaint : EntityBase
    {
        public Int64 Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Category { get; set; }
        public string AssignTo { get; set; }
        public int? Priority { get; set; }
        public DateTime DueDate { get; set; }
        public int Status { get; set; }
        public string Remarks { get; set; }
        public string Complainant { get; set; }
    }
}
