using ComplaintMngSys.Data;
using ComplaintMngSys.Models;
using ComplaintMngSys.Models.UserAccountViewModel;
using ComplaintMngSys.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace ComplaintMngSys.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class UserProfileController : Controller
    {
        private readonly IRoles _roles;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ICommon _iCommon;

        public UserProfileController(UserManager<ApplicationUser> userManager,
            ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager,
            IRoles roles,
            ICommon iCommon)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _roles = roles;
            _iCommon = iCommon;
        }

        [Authorize(Roles = Pages.MainMenu.UserProfile.RoleName)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var _ApplicationUser = await _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
            UserProfileCRUDViewModel _UserProfileCRUDViewModel = new UserProfileCRUDViewModel();
            if (_ApplicationUser.Id != null)
            {
                _UserProfileCRUDViewModel = _context.UserProfile.Where(x => x.ApplicationUserId == _ApplicationUser.Id).SingleOrDefault();
            }
            return View(_UserProfileCRUDViewModel);
        }

        [HttpGet]
        public IActionResult EditUserProfile(int id)
        {
            return PartialView("_EditUserProfile", _iCommon.GetByUserProfile(id));
        }

        [HttpPost]
        public async Task<IActionResult> EditUserProfile(UserProfileCRUDViewModel _UserProfileCRUDViewModel)
        {
            try
            {
                UserProfile _UserProfile = _iCommon.GetByUserProfile(_UserProfileCRUDViewModel.UserProfileId);

                _UserProfile.FirstName = _UserProfileCRUDViewModel.FirstName;
                _UserProfile.LastName = _UserProfileCRUDViewModel.LastName;
                _UserProfile.PhoneNumber = _UserProfileCRUDViewModel.PhoneNumber;
                _UserProfile.Address = _UserProfileCRUDViewModel.Address;
                _UserProfile.Country = _UserProfileCRUDViewModel.Country;

                if (_UserProfileCRUDViewModel.ProfilePicture != null)
                    _UserProfile.ProfilePicture = "/upload/" + _iCommon.UploadedFile(_UserProfileCRUDViewModel.ProfilePicture);

                _UserProfile.ModifiedDate = DateTime.Now;
                _UserProfile.ModifiedBy = HttpContext.User.Identity.Name;
                var result2 = _context.UserProfile.Update(_UserProfile);
                await _context.SaveChangesAsync();
                TempData["successAlert"] = "User info Updated Successfully. User Name: " + _UserProfileCRUDViewModel.Email;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> ResetPassword(string id)
        {
            var _ApplicationUser = await _userManager.FindByIdAsync(id);
            ResetPasswordVM _ResetPasswordVM = new ResetPasswordVM();
            _ResetPasswordVM.ApplicationUserId = _ApplicationUser.Id;
            return PartialView("_ResetPassword", _ResetPasswordVM);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM vm)
        {
            var _ApplicationUser = await _userManager.FindByIdAsync(vm.ApplicationUserId);
            if (vm.NewPassword.Equals(vm.ConfirmPassword))
            {
                var result = await _userManager.ChangePasswordAsync(_ApplicationUser, vm.OldPassword, vm.NewPassword);
                if (result.Succeeded)
                    TempData["successAlert"] = "Change Password Succeeded. User name: " + _ApplicationUser.Email;
                else
                {
                    string errorMessage = string.Empty;
                    foreach (var item in result.Errors)
                    {
                        errorMessage = errorMessage + " " + item.Description;
                    }
                    TempData["errorAlert"] = errorMessage;
                }
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
