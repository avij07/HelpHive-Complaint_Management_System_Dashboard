using ComplaintMngSys.Data;
using ComplaintMngSys.Models;
using ComplaintMngSys.Models.CommonViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ComplaintMngSys.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class EmailSettingController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EmailSettingController(ApplicationDbContext context)
        {
            _context = context;
        }


        [Authorize(Roles = Pages.MainMenu.EmailSetting.RoleName)]
        public async Task<IActionResult> Index()
        {
            EmailSettingViewModel vm = new EmailSettingViewModel();
            var _SMTPEmailSetting = await _context.SMTPEmailSetting.FindAsync(1);
            var _SendGridSetting = await _context.SendGridSetting.FindAsync(1);

            vm.SMTPEmailSettingViewModel = _SMTPEmailSetting;
            vm.SendGridSettingViewModel = _SendGridSetting;
            return View(vm);
        }


        public async Task<IActionResult> SMTPEmailSettingDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SMTPEmailSettingViewModel vm = await _context.SMTPEmailSetting.FirstOrDefaultAsync(m => m.Id == id);
            if (vm == null)
            {
                return NotFound();
            }

            return PartialView("_SMTPEmailSettingDetails", vm);
        }

        public async Task<IActionResult> SMTPEmailSettingAddEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SMTPEmailSettingViewModel vm = await _context.SMTPEmailSetting.FindAsync(id);
            if (vm == null)
            {
                return NotFound();
            }
            return PartialView("_SMTPEmailSettingAddEdit", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SMTPEmailSettingAddEdit(SMTPEmailSettingViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.Id > 0)
                {
                    SMTPEmailSetting _SMTPEmailSetting = await _context.SMTPEmailSetting.FindAsync(vm.Id);

                    vm.CreatedDate = _SMTPEmailSetting.CreatedDate;
                    vm.CreatedBy = _SMTPEmailSetting.CreatedBy;
                    vm.ModifiedDate = DateTime.Now;
                    vm.ModifiedBy = HttpContext.User.Identity.Name;
                    _context.Entry(_SMTPEmailSetting).CurrentValues.SetValues(vm);
                    await _context.SaveChangesAsync();
                    TempData["successAlert"] = "SMTP Info Updated Successfully. User Name: " + _SMTPEmailSetting.UserName;

                    SendGridSetting _SendGridSetting = await _context.SendGridSetting.FindAsync(vm.Id);
                    if (vm.IsDefault)
                        _SendGridSetting.IsDefault = false;
                    else
                        _SendGridSetting.IsDefault = true;

                    _SendGridSetting.ModifiedDate = DateTime.Now;
                    _SendGridSetting.ModifiedBy = HttpContext.User.Identity.Name;
                    _context.SendGridSetting.Update(_SendGridSetting);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    vm.CreatedDate = DateTime.Now;
                    vm.ModifiedDate = DateTime.Now;
                    vm.CreatedBy = HttpContext.User.Identity.Name;
                    vm.ModifiedBy = HttpContext.User.Identity.Name;
                    _context.Add(vm);
                    await _context.SaveChangesAsync();
                    TempData["successAlert"] = "SMTP Info Created Successfully. User Name: " + vm.UserName;
                    return RedirectToAction(nameof(Index));
                }
            }
            TempData["errorAlert"] = "Operation failed.";
            return View("Index");
        }


        public async Task<IActionResult> SendGridSettingDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SendGridSettingViewModel vm = await _context.SendGridSetting.FirstOrDefaultAsync(m => m.Id == id);
            if (vm == null)
            {
                return NotFound();
            }

            return PartialView("_SendGridSettingDetails", vm);
        }

        public async Task<IActionResult> SendGridSettingAddEdit(int? id)
        {
            if (id == null) return NotFound();

            SendGridSettingViewModel vm = await _context.SendGridSetting.FindAsync(id);
            if (vm == null)
            {
                return NotFound();
            }
            return PartialView("_SendGridSettingAddEdit", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendGridSettingAddEdit(SendGridSettingViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.Id > 0)
                {
                    SendGridSetting _SendGridSetting = await _context.SendGridSetting.FindAsync(vm.Id);

                    vm.CreatedDate = _SendGridSetting.CreatedDate;
                    vm.CreatedBy = _SendGridSetting.CreatedBy;
                    vm.ModifiedDate = DateTime.Now;
                    vm.ModifiedBy = HttpContext.User.Identity.Name;
                    _context.Entry(_SendGridSetting).CurrentValues.SetValues(vm);
                    await _context.SaveChangesAsync();
                    TempData["successAlert"] = "SendGrid Info Updated Successfully. User Name: " + _SendGridSetting.FromEmail;

                    SMTPEmailSetting _SMTPEmailSetting = await _context.SMTPEmailSetting.FindAsync(vm.Id);
                    if (vm.IsDefault)
                        _SMTPEmailSetting.IsDefault = false;
                    else
                        _SMTPEmailSetting.IsDefault = true;

                    _SMTPEmailSetting.ModifiedDate = DateTime.Now;
                    _SMTPEmailSetting.ModifiedBy = HttpContext.User.Identity.Name;
                    _context.SMTPEmailSetting.Update(_SMTPEmailSetting);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    vm.CreatedDate = DateTime.Now;
                    vm.ModifiedDate = DateTime.Now;
                    vm.CreatedBy = HttpContext.User.Identity.Name;
                    vm.ModifiedBy = HttpContext.User.Identity.Name;
                    _context.Add(vm);
                    await _context.SaveChangesAsync();
                    TempData["successAlert"] = "SendGrid Info Created Successfully. User Name: " + vm.FromEmail;
                    return RedirectToAction(nameof(Index));
                }
            }
            TempData["errorAlert"] = "Operation failed.";
            return View("Index");
        }
    }
}