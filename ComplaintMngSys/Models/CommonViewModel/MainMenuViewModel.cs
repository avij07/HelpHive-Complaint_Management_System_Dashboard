namespace ComplaintMngSys.Models.CommonViewModel
{
    public class MainMenuViewModel
    {
        public bool Admin { get; set; }
        public bool Dashboard { get; set; }

        public bool UserManagement { get; set; }
        public bool UserProfile { get; set; }
        public bool ManagePageAccess { get; set; }
        public bool EmailSetting { get; set; }
        public bool IdentitySetting { get; set; }
        public bool LoginHistory { get; set; }

        //Business
        public bool MyComplaint { get; set; }
        public bool AssignToMe { get; set; }
        public bool ManageComplaint { get; set; }
        public bool ResolvedComplaint { get; set; }
        public bool ComplaintStatus { get; set; }
        public bool ComplaintCategory { get; set; }
        public bool Comment { get; set; }
        public bool Notification { get; set; }
        public bool AttachmentFile { get; set; }
        public bool Priority { get; set; }
        public bool ComplaintStatusSummary { get; set; }
        public bool AssignedToSummary { get; set; }
        public bool CompanyInfo { get; set; }
        public bool AuditLogs { get; set; }
    }
}