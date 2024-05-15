using ComplaintMngSys.Models.CompanyInfoViewModel;

namespace ComplaintMngSys.Models.ReportViewModel
{
    public class AssignToSummaryViewModel
    {
        public string ApplicationUserId { get; set; }
        public string UserName { get; set; }
        public int TotalAssigned { get; set; }
    }
}
