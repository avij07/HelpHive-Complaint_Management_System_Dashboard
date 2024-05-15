using ComplaintMngSys.Data;
using ComplaintMngSys.Helpers;
using ComplaintMngSys.Models;
using ComplaintMngSys.Models.UserAccountViewModel;
using Microsoft.AspNetCore.Identity;

namespace ComplaintMngSys.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICommon _iCommon;
        public AccountService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ICommon iCommon)
        {
            _context = context;
            _userManager = userManager;
            _iCommon = iCommon;
        }
        public async Task<Tuple<ApplicationUser, string>> CreateUserProfile(UserProfileCRUDViewModel vm, string LoginUser)
        {
            UserProfile _UserProfile = new UserProfile();
            string errorMessage = string.Empty;
            try
            {
                IdentityResult _IdentityResult = null;
                ApplicationUser _ApplicationUser = new ApplicationUser()
                {
                    UserName = vm.Email,
                    PhoneNumber = vm.PhoneNumber,
                    PhoneNumberConfirmed = true,
                    Email = vm.Email,
                    EmailConfirmed = true
                };
                if (vm.PasswordHash.Equals(vm.ConfirmPassword))
                {
                    _IdentityResult = await _userManager.CreateAsync(_ApplicationUser, vm.PasswordHash);
                }

                if (_IdentityResult.Succeeded)
                {
                    vm.ApplicationUserId = _ApplicationUser.Id;
                    vm.PasswordHash = _ApplicationUser.PasswordHash;
                    vm.ConfirmPassword = _ApplicationUser.PasswordHash;
                    vm.CreatedDate = DateTime.Now;
                    vm.ModifiedDate = DateTime.Now;
                    vm.CreatedBy = LoginUser;
                    vm.ModifiedBy = LoginUser;

                    _UserProfile = vm;
                    if (vm.ProfilePicture == null)
                        _UserProfile.ProfilePicture = vm.ProfilePicturePath;
                    else
                        _UserProfile.ProfilePicture = "/upload/" + _iCommon.UploadedFile(vm.ProfilePicture);

                    await _context.UserProfile.AddAsync(_UserProfile);
                    var result = await _context.SaveChangesAsync();

                    for (int i = 0; i < DefaultUserPage.PageCollection.Length; i++)
                    {
                        await _userManager.AddToRoleAsync(_ApplicationUser, DefaultUserPage.PageCollection[i]);
                    }
                }
                else
                {
                    foreach (var item in _IdentityResult.Errors)
                    {
                        errorMessage = errorMessage + " " + item.Description;
                    }
                    return Tuple.Create(_ApplicationUser, errorMessage);
                }
                return Tuple.Create(_ApplicationUser, "Success");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
