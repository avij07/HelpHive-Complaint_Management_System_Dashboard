using ComplaintMngSys.Models;
using ComplaintMngSys.Models.CommonViewModel;
using ComplaintMngSys.Models.CompanyInfoViewModel;
using ComplaintMngSys.Models.ComplaintViewModel;
using ComplaintMngSys.Models.NotificationViewModel;
using ComplaintMngSys.Models.UserAccountViewModel;
using Microsoft.AspNetCore.Identity;
using UAParser;

namespace ComplaintMngSys.Services
{
    public interface ICommon
    {
        string UploadedFile(IFormFile ProfilePicture);
        Task<SMTPEmailSetting> GetSMTPEmailSetting();
        Task<SendGridSetting> GetSendGridEmailSetting();
        UserProfile GetByUserProfile(Int64 id);
        UserProfileCRUDViewModel GetByUserProfileInfo(Int64 id);
        Task<bool> InsertLoginHistory(LoginHistory _LoginHistory, ClientInfo _ClientInfo);
        Task<UpdateRoleViewModel> GetRoleByUser(string _ApplicationUserId, UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager);
        IQueryable<ItemDropdownListViewModel> LoadddlComplaintCategory();
        IQueryable<ItemDropdownListViewModel> LoadddlComplaintStatus();
        IQueryable<ItemDropdownListViewModel> LoadddlPriority();
        
        IQueryable<ItemDropdownListViewModel> LoadddlAssignTo();
        Task<IQueryable<ItemDropdownListViewModel>> LoadddlAssignToGeneral();
        IQueryable<ComplaintGridViewModel> GetComplaintViewItem();
        Task<string> AddNotification(NotificationCRUDViewModel vm);
        Task<string> AddComment(Int64 _ComplaintId, string _Message, bool _IsInRole, string strUserName);
        Task<string> AddAttachmentFile(IFormFile _IFormFile, AttachmentFile _AttachmentFile);
        Tuple<byte[], string> GetDownloadDetails(Int64 id);
        CompanyInfoCRUDViewModel GetCompanyInfo();
    }
}
