namespace ComplaintMngSys.Pages
{
    public static class MainMenu
    {
        public static class Admin
        {
            public const string RoleName = "Admin";
        }

        public static class Dashboard
        {
            public const string PageName = "Dashboard";
            public const string RoleName = "Dashboard";
            public const string Path = "/Dashboard/Index";
            public const string ControllerName = "Dashboard";
            public const string ActionName = "Index";
        }

        public static class UserManagement
        {
            public const string PageName = "User Management";
            public const string RoleName = "User Management";
            public const string Path = "/UserManagement/Index";
            public const string ControllerName = "UserManagement";
            public const string ActionName = "Index";
        }
        public static class UserProfile
        {
            public const string PageName = "User Profile";
            public const string RoleName = "User Profile";
            public const string Path = "/UserProfile/Index";
            public const string ControllerName = "UserProfile";
            public const string ActionName = "Index";
        }
        public static class ManagePageAccess
        {
            public const string PageName = "Manage Page Access";
            public const string RoleName = "Manage Page Access";
            public const string Path = "/UserRole/Index";
            public const string ControllerName = "UserRole";
            public const string ActionName = "Index";
        }
        public static class EmailSetting
        {
            public const string PageName = "Email Setting";
            public const string RoleName = "Email Setting";
            public const string Path = "/EmailSetting/Index";
            public const string ControllerName = "EmailSetting";
            public const string ActionName = "Index";
        }

        public static class IdentitySetting
        {
            public const string PageName = "Identity Setting";
            public const string RoleName = "Identity Setting";
            public const string Path = "/IdentitySetting/Index";
            public const string ControllerName = "IdentitySetting";
            public const string ActionName = "Index";
        }

        public static class LoginHistory
        {
            public const string PageName = "Login History";
            public const string RoleName = "Login History";
            public const string Path = "/LoginHistory/Index";
            public const string ControllerName = "LoginHistory";
            public const string ActionName = "Index";
        }



        //Business
        public static class MyComplaint
        {
            public const string PageName = "My Complaint";
            public const string RoleName = "My Complaint";
            public const string Path = "/Complaint/IndexCreatedByComplaint";
            public const string ControllerName = "Complaint";
            public const string ActionName = "IndexCreatedByComplaint";
        }
        public static class AssignToMe
        {
            public const string PageName = "All Complaints";
            public const string RoleName = "Assign To Me";
            public const string Path = "/Complaint/IndexAssignToComplaint";
            public const string ControllerName = "Complaint";
            public const string ActionName = "IndexAssignToComplaint";
        }


        public static class ManageComplaint
        {
            public const string PageName = "Manage Complaint";
            public const string RoleName = "Manage Complaint";
            public const string Path = "/ComplaintManage/IndexAllComplained";
            public const string ControllerName = "ComplaintManage";
            public const string ActionName = "IndexAllComplained";
        }
        public static class ResolvedComplaint
        {
            public const string PageName = "Resolved Complaint";
            public const string RoleName = "Resolved Complaint";
            public const string Path = "/Complaint/IndexResolvedComplained";
            public const string ControllerName = "Complaint";
            public const string ActionName = "IndexResolvedComplained";
        }

        public static class ComplaintStatus
        {
            public const string PageName = "Complaint Status";
            public const string RoleName = "Complaint Status";
            public const string Path = "/ComplaintStatus/Index";
            public const string ControllerName = "ComplaintStatus";
            public const string ActionName = "Index";
        }
        public static class ComplaintCategory
        {
            public const string PageName = "Complaint Category";
            public const string RoleName = "Complaint Category";
            public const string Path = "/ComplaintCategory/Index";
            public const string ControllerName = "ComplaintCategory";
            public const string ActionName = "Index";
        }

        public static class Comment
        {
            public const string PageName = "Comment";
            public const string RoleName = "Comment";
            public const string Path = "/Comment/Index";
            public const string ControllerName = "Comment";
            public const string ActionName = "Index";
        }
        public static class Notification
        {
            public const string PageName = "Notification";
            public const string RoleName = "Notification";
            public const string Path = "/Notification/Index";
            public const string ControllerName = "Notification";
            public const string ActionName = "Index";
        }
        public static class AttachmentFile
        {
            public const string PageName = "Attachment File";
            public const string RoleName = "Attachment File";
            public const string Path = "/AttachmentFile/Index";
            public const string ControllerName = "AttachmentFile";
            public const string ActionName = "Index";
        }
        public static class Priority
        {
            public const string PageName = "Priority";
            public const string RoleName = "Priority";
            public const string Path = "/Priority/Index";
            public const string ControllerName = "Priority";
            public const string ActionName = "Index";
        }
        public static class ComplaintStatusSummaryReport
        {
            public const string PageName = "Complaint Status Summary";
            public const string RoleName = "Complaint Status Summary";
            public const string Path = "/Report/ComplaintStatusSummaryReport";
            public const string ControllerName = "Report";
            public const string ActionName = "ComplaintStatusSummaryReport";
        }
        public static class AssignedToSummaryReport
        {
            public const string PageName = "Assigned To Summary";
            public const string RoleName = "Assigned To Summary";
            public const string Path = "/Report/AssignedToSummaryReport";
            public const string ControllerName = "Report";
            public const string ActionName = "AssignedToSummaryReport";
        }

        public static class CompanyInfo
        {
            public const string PageName = "Company Info";
            public const string RoleName = "Company Info";
            public const string Path = "/CompanyInfo/Index";
            public const string ControllerName = "CompanyInfo";
            public const string ActionName = "Index";
        }
        public static class AuditLogs
        {
            public const string PageName = "Audit Logs";
            public const string RoleName = "Audit Logs";
            public const string Path = "/AuditLogs/Index";
        }
    }
}
