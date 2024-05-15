using ComplaintMngSys.Pages;

namespace ComplaintMngSys.Helpers
{
    public static class DefaultUserPage
    {
        public static readonly string[] PageCollection =
            {
                MainMenu.Dashboard.PageName,
                MainMenu.UserProfile.PageName,
                MainMenu.MyComplaint.PageName,
                MainMenu.AssignToMe.PageName,
                MainMenu.Notification.PageName
            };
    }
}
