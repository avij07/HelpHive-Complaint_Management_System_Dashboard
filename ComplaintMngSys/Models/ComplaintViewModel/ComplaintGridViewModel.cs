using System;

namespace ComplaintMngSys.Models.ComplaintViewModel
{
    public class ComplaintGridViewModel : EntityBase
    {
        public Int64 Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string AssignTo { get; set; }
        public string ApplicationUserId { get; set; }
        public string Priority { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public string Complainant { get; set; }
        public string CurrentUserId { get; set; }
    }
}
