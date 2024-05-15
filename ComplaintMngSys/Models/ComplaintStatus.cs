using System;

namespace ComplaintMngSys.Models
{
    public class ComplaintStatus : EntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
