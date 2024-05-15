using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using ComplaintMngSys.Data;
using ComplaintMngSys.Models;
using ComplaintMngSys.Models.CommonViewModel;
using ComplaintMngSys.Services;

namespace ComplaintMngSys.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class IdentitySettingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICommon _iCommon;
        private readonly IdentityOptions _IdentityOptions;
        private readonly IHttpContextAccessor _accessor;



        public IdentitySettingController(ApplicationDbContext context, ICommon iCommon, IOptions<IdentityOptions> iOptions, IHttpContextAccessor accessor)
        {
            _context = context;
            _iCommon = iCommon;
            _IdentityOptions = iOptions.Value;
            _accessor = accessor;
        }

        [Authorize(Roles = Pages.MainMenu.IdentitySetting.RoleName)]
        public async Task<IActionResult> Index()
        {
            DefaultIdentityOptionsCRUDViewModel vm = await _context.DefaultIdentityOptions.FirstOrDefaultAsync(m => m.Id == 1);
            return View(vm);
        }


        public async Task<IActionResult> Edit()
        {
            DefaultIdentityOptionsCRUDViewModel vm = await _context.DefaultIdentityOptions.FirstOrDefaultAsync(m => m.Id == 1);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DefaultIdentityOptionsCRUDViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        DefaultIdentityOptions _DefaultIdentityOptions = new DefaultIdentityOptions();
                        _DefaultIdentityOptions = await _context.DefaultIdentityOptions.FindAsync(vm.Id);

                        vm.ModifiedDate = DateTime.Now;
                        vm.ModifiedBy = HttpContext.User.Identity.Name;
                        _context.Entry(_DefaultIdentityOptions).CurrentValues.SetValues(vm);
                        await _context.SaveChangesAsync();

                        IdentityOptionUpdate(_DefaultIdentityOptions);

                        TempData["successAlert"] = "Default Identity Options Updated Successfully. Id: " + _DefaultIdentityOptions.Id;
                        return RedirectToAction(nameof(Index));
                    }
                    TempData["errorAlert"] = "Operation failed.";
                    return View("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IsExists(vm.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(vm);
        }

        private void IdentityOptionUpdate(DefaultIdentityOptions _DefaultIdentityOptions)
        {
            _IdentityOptions.Password.RequireDigit = _DefaultIdentityOptions.PasswordRequireDigit;
            _IdentityOptions.Password.RequiredLength = _DefaultIdentityOptions.PasswordRequiredLength;
            _IdentityOptions.Password.RequireNonAlphanumeric = _DefaultIdentityOptions.PasswordRequireNonAlphanumeric;
            _IdentityOptions.Password.RequireUppercase = _DefaultIdentityOptions.PasswordRequireUppercase;
            _IdentityOptions.Password.RequireLowercase = _DefaultIdentityOptions.PasswordRequireLowercase;
            _IdentityOptions.Password.RequiredUniqueChars = _DefaultIdentityOptions.PasswordRequiredUniqueChars;

            _IdentityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(_DefaultIdentityOptions.LockoutDefaultLockoutTimeSpanInMinutes);
            _IdentityOptions.Lockout.MaxFailedAccessAttempts = _DefaultIdentityOptions.LockoutMaxFailedAccessAttempts;
            _IdentityOptions.Lockout.AllowedForNewUsers = _DefaultIdentityOptions.LockoutAllowedForNewUsers;

            _IdentityOptions.User.RequireUniqueEmail = _DefaultIdentityOptions.UserRequireUniqueEmail;
            _IdentityOptions.SignIn.RequireConfirmedEmail = _DefaultIdentityOptions.SignInRequireConfirmedEmail;

            //_IdentityOptions.Cookie.HttpOnly = _DefaultIdentityOptions.CookieHttpOnly;
            //_IdentityOptions.Cookie.Expiration = TimeSpan.FromDays(_DefaultIdentityOptions.CookieExpiration);
            //_IdentityOptions.ExpireTimeSpan = TimeSpan.FromMinutes(_DefaultIdentityOptions.CookieExpireTimeSpan);
        }

        private bool IsExists(long id)
        {
            return _context.DefaultIdentityOptions.Any(e => e.Id == id);
        }
    }
}

