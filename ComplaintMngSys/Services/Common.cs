using ComplaintMngSys.Data;
using ComplaintMngSys.Models;
using ComplaintMngSys.Models.CommonViewModel;
using ComplaintMngSys.Models.CompanyInfoViewModel;
using ComplaintMngSys.Models.ComplaintViewModel;
using ComplaintMngSys.Models.NotificationViewModel;
using ComplaintMngSys.Models.UserAccountViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UAParser;

namespace ComplaintMngSys.Services
{
    public class Common : ICommon
    {
        private readonly IWebHostEnvironment _iHostingEnvironment;
        private readonly ApplicationDbContext _context;
        public Common(IWebHostEnvironment iHostingEnvironment, ApplicationDbContext context)
        {
            _iHostingEnvironment = iHostingEnvironment;
            _context = context;
        }
        public string UploadedFile(IFormFile ProfilePicture)
        {
            string ProfilePictureFileName = null;

            if (ProfilePicture != null)
            {
                string uploadsFolder = Path.Combine(_iHostingEnvironment.ContentRootPath, "wwwroot\\upload");

                if (ProfilePicture.FileName == null)
                    ProfilePictureFileName = Guid.NewGuid().ToString() + "_" + "blank-person.png";
                else
                    ProfilePictureFileName = Guid.NewGuid().ToString() + "_" + ProfilePicture.FileName;
                string filePath = Path.Combine(uploadsFolder, ProfilePictureFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    ProfilePicture.CopyTo(fileStream);
                }
            }
            return ProfilePictureFileName;
        }

        public async Task<SMTPEmailSetting> GetSMTPEmailSetting()
        {
            return await _context.Set<SMTPEmailSetting>().Where(x => x.Id == 1).SingleOrDefaultAsync();
        }
        public async Task<SendGridSetting> GetSendGridEmailSetting()
        {
            return await _context.Set<SendGridSetting>().Where(x => x.Id == 1).SingleOrDefaultAsync();
        }

        public UserProfile GetByUserProfile(Int64 id)
        {
            return _context.UserProfile.Where(x => x.UserProfileId == id).SingleOrDefault();
        }
        public UserProfileCRUDViewModel GetByUserProfileInfo(Int64 id)
        {
            UserProfileCRUDViewModel _UserProfileCRUDViewModel = _context.UserProfile.Where(x => x.UserProfileId == id).SingleOrDefault();
            return _UserProfileCRUDViewModel;
        }
        public async Task<bool> InsertLoginHistory(LoginHistory _LoginHistory, ClientInfo _ClientInfo)
        {
            try
            {
                _LoginHistory.PublicIP = await GetPublicIP();
                _LoginHistory.CreatedDate = DateTime.Now;
                _LoginHistory.ModifiedDate = DateTime.Now;

                _context.Add(_LoginHistory);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception) { throw; }
        }
        public async Task<UpdateRoleViewModel> GetRoleByUser(string _ApplicationUserId, UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager)
        {
            UpdateRoleViewModel _UpdateRoleViewModel = new UpdateRoleViewModel();
            List<ManageUserRolesViewModel> list = new List<ManageUserRolesViewModel>();

            _UpdateRoleViewModel.ApplicationUserId = _ApplicationUserId;
            var user = await _userManager.FindByIdAsync(_ApplicationUserId);
            if (user != null)
            {
                foreach (var role in _roleManager.Roles.ToList())
                {
                    var userRolesViewModel = new ManageUserRolesViewModel
                    {
                        RoleId = role.Id,
                        RoleName = role.Name
                    };
                    if (await _userManager.IsInRoleAsync(user, role.Name))
                    {
                        userRolesViewModel.Selected = true;
                    }
                    else
                    {
                        userRolesViewModel.Selected = false;
                    }
                    list.Add(userRolesViewModel);
                }
            }

            _UpdateRoleViewModel.listManageUserRolesViewModel = list.OrderBy(x => x.RoleName).ToList();
            return _UpdateRoleViewModel;
        }

        public static async Task<string> GetPublicIP()
        {
            try
            {
                string url = "http://checkip.dyndns.org/";
                var _HttpClient = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true });
                var _GetAsync = await _HttpClient.GetAsync(url);
                var _Stream = await _GetAsync.Content.ReadAsStreamAsync();
                StreamReader _StreamReader = new StreamReader(_Stream);
                string result = _StreamReader.ReadToEnd();

                string[] a = result.Split(':');
                string a2 = a[1].Substring(1);
                string[] a3 = a2.Split('<');
                string a4 = a3[0];
                return a4;
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw new Exception("No network adapters with an IPv4 address in the system!");
            }
        }

        public IQueryable<ItemDropdownListViewModel> LoadddlComplaintCategory()
        {
            return (from _ComplaintCategory in _context.ComplaintCategory.Where(x => x.Cancelled == false).OrderBy(x => x.Id)
                    select new ItemDropdownListViewModel
                    {
                        Id = _ComplaintCategory.Id,
                        Name = _ComplaintCategory.Name,
                    });
        }
        public IQueryable<ItemDropdownListViewModel> LoadddlComplaintStatus()
        {
            return (from _ComplaintStatus in _context.ComplaintStatus.Where(x => x.Cancelled == false).OrderBy(x => x.Id)
                    select new ItemDropdownListViewModel
                    {
                        Id = _ComplaintStatus.Id,
                        Name = _ComplaintStatus.Name,
                    });
        }

        public IQueryable<ItemDropdownListViewModel> LoadddlPriority()
        {
            return (from _Priority in _context.Priority.Where(x => x.Cancelled == false).OrderBy(x => x.Id)
                    select new ItemDropdownListViewModel
                    {
                        Id = _Priority.Id,
                        Name = _Priority.Name,
                    });
        }
        public IQueryable<ItemDropdownListViewModel> LoadddlAssignTo()
        {
            return (from _UserProfile in _context.UserProfile.Where(x => x.Cancelled == false).OrderBy(x => x.CreatedDate)
                    select new ItemDropdownListViewModel
                    {
                        ApplicationUserId = _UserProfile.ApplicationUserId,
                        Name = _UserProfile.FirstName + " " + _UserProfile.LastName,
                    });
        }

        public async Task<IQueryable<ItemDropdownListViewModel>> LoadddlAssignToGeneral()
        {
            //Get Admin Roles
            var _Roles = await _context.Roles.Where(x => x.Name == "Admin").SingleOrDefaultAsync();

            var listUserRoles = await _context.UserRoles.Where(x => x.RoleId == _Roles.Id).ToListAsync();
            var listUserProfile = await _context.UserProfile.Where(x => x.Cancelled == false).ToListAsync();

            var _AdminUserOnly = (from _listUserProfile in listUserProfile
                                  join _listUserRoles in listUserRoles on _listUserProfile.ApplicationUserId equals _listUserRoles.UserId
                                  select _listUserProfile).ToList();


            var result = from _UserProfile in _AdminUserOnly.AsQueryable()
                         select new ItemDropdownListViewModel
                         {
                             ApplicationUserId = _UserProfile.ApplicationUserId,
                             Name = _UserProfile.FirstName + " " + _UserProfile.LastName,
                         };

            return result;
        }

        public IQueryable<ComplaintGridViewModel> GetComplaintViewItem()
        {
            try
            {
                return (from _Complaint in _context.Complaint
                        join _ComplaintCategory in _context.ComplaintCategory on _Complaint.Category equals _ComplaintCategory.Id
                        into listComplaint1
                        from listComplaintCategory in listComplaint1.DefaultIfEmpty()

                        join _ComplaintStatus in _context.ComplaintStatus on _Complaint.Status equals _ComplaintStatus.Id
                        into listComplaint2
                        from listComplaintStatus in listComplaint2.DefaultIfEmpty()

                        join _Priority in _context.Priority on _Complaint.Priority equals _Priority.Id
                        into listComplaint3
                        from listPriority in listComplaint3.DefaultIfEmpty()

                        join _UserProfile in _context.UserProfile on _Complaint.AssignTo equals _UserProfile.ApplicationUserId
                        into listComplaint4
                        from listUserProfile in listComplaint4.DefaultIfEmpty()
                        where _Complaint.Cancelled == false
                        select new ComplaintGridViewModel
                        {
                            Id = _Complaint.Id,
                            Code = _Complaint.Code,
                            Name = _Complaint.Name,
                            Description = _Complaint.Description,
                            CategoryName = listComplaintCategory.Name,
                            Priority = listPriority.Name,
                            AssignTo = listUserProfile.FirstName,
                            ApplicationUserId = listUserProfile.ApplicationUserId,
                            Status = listComplaintStatus.Name,
                            Remarks = _Complaint.Remarks,
                            Complainant = _Complaint.Complainant,

                            CreatedDate = _Complaint.CreatedDate,
                            DueDate = _Complaint.DueDate,
                            ModifiedDate = _Complaint.ModifiedDate,
                            CreatedBy = _Complaint.CreatedBy,
                            ModifiedBy = _Complaint.ModifiedBy,
                            Cancelled = _Complaint.Cancelled,
                        }).OrderByDescending(x => x.Id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> AddNotification(NotificationCRUDViewModel vm)
        {
            try
            {
                Notification _Notification = new Notification();
                _Notification = vm;
                _Notification.IsRead = false;
                _Notification.CreatedDate = DateTime.Now;
                _Notification.ModifiedDate = DateTime.Now;
                _context.Add(_Notification);
                await _context.SaveChangesAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> AddComment(Int64 _ComplaintId, string _Message, bool _IsInRole, string strUserName)
        {
            try
            {
                Comment _Comment = new Comment
                {
                    ComplaintId = _ComplaintId,
                    Message = _Message,
                    IsAdmin = _IsInRole,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    CreatedBy = strUserName,
                    ModifiedBy = strUserName,
                };
                _context.Comment.Add(_Comment);
                await _context.SaveChangesAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> AddAttachmentFile(IFormFile _IFormFile, AttachmentFile _AttachmentFile)
        {
            try
            {
                string uploadsFolder = Path.Combine(_iHostingEnvironment.ContentRootPath, "wwwroot\\upload");
                string _FilePath = Path.Combine(uploadsFolder, _AttachmentFile.FileName);
                AttachmentFile vm = new AttachmentFile
                {
                    ComplaintId = _AttachmentFile.ComplaintId,
                    FilePath = _FilePath,
                    ContentType = _IFormFile.ContentType,
                    FileName = _IFormFile.FileName,
                    Length = _IFormFile.Length,
                    IsAdmin = _AttachmentFile.IsAdmin,

                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    CreatedBy = _AttachmentFile.CreatedBy,
                    ModifiedBy = _AttachmentFile.ModifiedBy,
                };
                _context.AttachmentFile.Add(vm);
                await _context.SaveChangesAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public Tuple<byte[], string> GetDownloadDetails(Int64 id)
        {
            byte[] bytes = null;
            try
            {
                var _AttachmentFile = _context.AttachmentFile.Where(x => x.Id == id).SingleOrDefault();
                string _WebRootPath = _iHostingEnvironment.WebRootPath + _AttachmentFile.FilePath;
                bytes = File.ReadAllBytes(_WebRootPath);

                var _Tuple = new Tuple<byte[], string>(bytes, _AttachmentFile.FileName);
                return _Tuple;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public CompanyInfoCRUDViewModel GetCompanyInfo()
        {
            CompanyInfoCRUDViewModel vm = _context.CompanyInfo.FirstOrDefault(m => m.Id == 1);
            return vm;
        }
    }
}