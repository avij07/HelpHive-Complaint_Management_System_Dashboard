using System;

namespace ComplaintMngSys.Models.DashboardViewModel
{
    public class DashboardSummaryViewModel
    {
        public Int64 TotalComplaint { get; set; }
        public Int64 TotalComplaintResolved { get; set; }
        public Int64 TotalUser { get; set; }
    }
}
