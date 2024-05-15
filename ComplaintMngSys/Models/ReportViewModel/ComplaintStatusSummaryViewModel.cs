using ComplaintMngSys.Models.CompanyInfoViewModel;

namespace ComplaintMngSys.Models.ReportViewModel
{
    public class ComplaintStatusSummaryViewModel
    {
        public int New { get; set; }
        public int Submited { get; set; }
        public int InProgress { get; set; }
        public int Pending { get; set; }
        public int Resolved { get; set; }
        public int Rejected { get; set; }
        public int Blocker { get; set; }
        public int Closed { get; set; }
        public int ToDo { get; set; }
        public int Total { get; set; }
    }
}
