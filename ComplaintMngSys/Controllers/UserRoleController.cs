using ComplaintMngSys.Models;
using ComplaintMngSys.Models.CommonViewModel;
using ComplaintMngSys.Models.UserAccountViewModel;
using ComplaintMngSys.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMngSys.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class UserRoleController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ICommon _iCommon;

        public UserRoleController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ICommon iCommon)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _iCommon = iCommon;
        }

        [Authorize(Roles = Pages.MainMenu.UserManagement.RoleName)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var _ApplicationUser = await _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
            var _GetRoleByUser = await _iCommon.GetRoleByUser(_ApplicationUser.Id, _userManager, _roleManager);
            return View("Index", _GetRoleByUser);
        }

        [HttpGet]
        public async Task<IActionResult> ManageRoleGeneral(string _ApplicationUserId)
        {
            var _GetRoleByUser = await _iCommon.GetRoleByUser(_ApplicationUserId, _userManager, _roleManager);
            return PartialView("_ManageRole", _GetRoleByUser);
        }

        [HttpGet]
        public async Task<IActionResult> ManageRoleAdmin(Int64 id)
        {
            UserProfile _UserProfile = _iCommon.GetByUserProfile(id);
            var _GetRoleByUser = await _iCommon.GetRoleByUser(_UserProfile.ApplicationUserId, _userManager, _roleManager);
            return PartialView("_ManageRole", _GetRoleByUser);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(UpdateRoleViewModel _UpdateRoleViewModel)
        {
            JsonResultViewModel _JsonResultViewModel = new JsonResultViewModel();
            try
            {
                var _ApplicationUser = await _userManager.FindByIdAsync(_UpdateRoleViewModel.ApplicationUserId);
                if (_ApplicationUser == null)
                {
                    return new JsonResult("User not found");
                }
                var roles = await _userManager.GetRolesAsync(_ApplicationUser);
                var result = await _userManager.RemoveFromRolesAsync(_ApplicationUser, roles);
                if (!result.Succeeded)
                {
                    return new JsonResult("Cannot remove user existing roles");
                }
                result = await _userManager.AddToRolesAsync(_ApplicationUser, _UpdateRoleViewModel.listManageUserRolesViewModel.Where(x => x.Selected).Select(y => y.RoleName));
                if (!result.Succeeded)
                {
                    return new JsonResult("Cannot add selected roles to user");
                }
                _JsonResultViewModel.AlertMessage = "Role update Successfully. User Name: " + _ApplicationUser.Email;
                _JsonResultViewModel.CurrentURL = _UpdateRoleViewModel.CurrentURL;
                _JsonResultViewModel.IsSuccess = true;
                return new JsonResult(_JsonResultViewModel);
            }
            catch (Exception ex)
            {
                _JsonResultViewModel.IsSuccess = false;
                return new JsonResult(ex.Message);
                throw;
            }
        }
    }
}