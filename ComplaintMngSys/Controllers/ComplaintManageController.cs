using ComplaintMngSys.Data;
using ComplaintMngSys.Models;
using ComplaintMngSys.Models.ComplaintViewModel;
using ComplaintMngSys.Models.NotificationViewModel;
using ComplaintMngSys.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace ComplaintMngSys.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ComplaintManageController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICommon _iCommon;


        public ComplaintManageController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ICommon iCommon)
        {
            _context = context;
            _userManager = userManager;
            _iCommon = iCommon;
        }

        [Authorize(Roles = Pages.MainMenu.ManageComplaint.RoleName)]
        public IActionResult IndexAllComplained()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetDataTabelData(string ComplaintList)
        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();

                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnAscDesc = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                int resultTotal = 0;

                IQueryable<ComplaintGridViewModel> _GetGridItem = null;
                var _ApplicationUserId = _context.UserProfile.Where(x => x.Email == HttpContext.User.Identity.Name).SingleOrDefault().ApplicationUserId;
                if (ComplaintList == "AssignTo")
                    _GetGridItem = _iCommon.GetComplaintViewItem().Where(x => x.ApplicationUserId.Trim() == _ApplicationUserId.Trim());
                else
                    _GetGridItem = _iCommon.GetComplaintViewItem().Where(x => x.Complainant == HttpContext.User.Identity.Name);

                //Sorting
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnAscDesc)))
                {
                    _GetGridItem = _GetGridItem.OrderBy(sortColumn + " " + sortColumnAscDesc);
                }


                //Sorting
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnAscDesc)))
                {
                    _GetGridItem = _GetGridItem.OrderBy(sortColumn + " " + sortColumnAscDesc);
                }

                //Search
                if (!string.IsNullOrEmpty(searchValue))
                {
                    searchValue = searchValue.ToLower();
                    _GetGridItem = _GetGridItem.Where(obj => obj.Id.ToString().Contains(searchValue)
                    || obj.Code.ToLower().Contains(searchValue)
                    || obj.Name.ToLower().Contains(searchValue)
                    || obj.Description.ToLower().Contains(searchValue)
                    || obj.CategoryName.ToLower().Contains(searchValue)
                    || obj.Status.ToLower().Contains(searchValue)
                    || obj.Remarks.ToLower().Contains(searchValue)

                    || obj.CreatedDate.ToString().Contains(searchValue));
                }

                resultTotal = _GetGridItem.Count();

                var result = _GetGridItem.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = resultTotal, recordsTotal = resultTotal, data = result });

            }
            catch (Exception)
            {
                throw;
            }

        }
        public IActionResult PrintComplaint(long? id)
        {
            ComplaintManageViewModel _ComplaintManageViewModel = new ComplaintManageViewModel();
            _ComplaintManageViewModel.ComplaintGridViewModel = _iCommon.GetComplaintViewItem().Where(x => x.Id == id).SingleOrDefault();
            _ComplaintManageViewModel.CommentList = _context.Comment.Where(x => x.ComplaintId == id && x.Cancelled == false).ToList();
            _ComplaintManageViewModel.AttachmentFileList = _context.AttachmentFile.Where(x => x.ComplaintId == id && x.Cancelled == false).ToList();
            _ComplaintManageViewModel.CompanyInfoCRUDViewModel = _iCommon.GetCompanyInfo();
            return View(_ComplaintManageViewModel);
        }

        public async Task<IActionResult> StatusUpdate(int id)
        {
            StatusManageViewModel vm = new StatusManageViewModel();
            if (id > 0)
            {
                ViewBag.ddlComplaintStatus = new SelectList(_iCommon.LoadddlComplaintStatus(), "Id", "Name");
                ViewBag.ddlAssignTo = new SelectList(await _iCommon.LoadddlAssignToGeneral(), "ApplicationUserId", "Name");

                vm.StatusUpdateViewModel = await _context.Complaint.Where(x => x.Id == id).SingleOrDefaultAsync();

                vm.CommentList = await _context.Comment.Where(x => x.ComplaintId == id && x.Cancelled == false).ToListAsync();
            }
            return PartialView("_StatusUpdate", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StatusUpdate(StatusManageViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var _Complaint = await _context.Complaint.FindAsync(vm.StatusUpdateViewModel.Id);
                    _Complaint.Status = vm.StatusUpdateViewModel.Status;
                    _Complaint.AssignTo = vm.StatusUpdateViewModel.AssignTo;
                    _Complaint.ModifiedDate = DateTime.Now;
                    _Complaint.ModifiedBy = HttpContext.User.Identity.Name;
                    _context.Update(_Complaint);
                    await _context.SaveChangesAsync();

                    var _IsInRole = User.IsInRole("Admin");
                    NotificationCRUDViewModel _NotificationCRUDViewModel = new NotificationCRUDViewModel();
                    _NotificationCRUDViewModel = _Complaint;
                    _NotificationCRUDViewModel.CreatedBy = HttpContext.User.Identity.Name;
                    _NotificationCRUDViewModel.ModifiedBy = HttpContext.User.Identity.Name;
                    _NotificationCRUDViewModel.Message = "Status updated";

                    //Complaint Creator Notification: Status Update
                    _NotificationCRUDViewModel.NotificationFor = _Complaint.CreatedBy;
                    await _iCommon.AddNotification(_NotificationCRUDViewModel);

                    //Complaint Assigned to Notificaton
                    var _UserProfile = _context.UserProfile.Where(x => x.ApplicationUserId == _Complaint.AssignTo).SingleOrDefault();
                    if (_UserProfile != null && _UserProfile.Email != _Complaint.CreatedBy)
                    {
                        _NotificationCRUDViewModel.NotificationFor = _UserProfile.Email;
                        await _iCommon.AddNotification(_NotificationCRUDViewModel);
                    }

                    //New Comment Notification
                    if (vm.StatusUpdateViewModel.NewComment != null)
                    {
                        await _iCommon.AddComment(_Complaint.Id, vm.StatusUpdateViewModel.NewComment, _IsInRole, HttpContext.User.Identity.Name);

                        _NotificationCRUDViewModel.Message = vm.StatusUpdateViewModel.NewComment;
                        _NotificationCRUDViewModel.NotificationFor = _Complaint.CreatedBy;
                        await _iCommon.AddNotification(_NotificationCRUDViewModel);

                        //Complaint Assigned to Notificaton                        
                        if (_UserProfile != null && _UserProfile.Email != _Complaint.CreatedBy)
                        {
                            _NotificationCRUDViewModel.Message = vm.StatusUpdateViewModel.NewComment;
                            _NotificationCRUDViewModel.NotificationFor = _UserProfile.Email;
                            await _iCommon.AddNotification(_NotificationCRUDViewModel);
                        }
                    }

                   

                    TempData["successAlert"] = "Complaint Status Updated Successfully. ID: " + _Complaint.Id;
                    return RedirectToAction(nameof(IndexAllComplained));
                }
                catch (Exception)
                {
                    throw;
                }
            }
            TempData["errorAlert"] = "Operation failed.";
            return View("IndexAllComplained");
        }

        private bool IsExists(long id)
        {
            return _context.Complaint.Any(e => e.Id == id);
        }
    }
}