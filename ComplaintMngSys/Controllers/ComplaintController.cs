using ComplaintMngSys.Data;
using ComplaintMngSys.Helpers;
using ComplaintMngSys.Models;
using ComplaintMngSys.Models.AttachmentFileViewModel;
using ComplaintMngSys.Models.ComplaintViewModel;
using ComplaintMngSys.Models.NotificationViewModel;
using ComplaintMngSys.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace ComplaintMngSys.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ComplaintController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICommon _iCommon;
        private readonly IWebHostEnvironment _iHostingEnvironment;

        public ComplaintController(ApplicationDbContext context, ICommon iCommon, IWebHostEnvironment iHostingEnvironment)
        {
            _context = context;
            _iCommon = iCommon;
            _iHostingEnvironment = iHostingEnvironment;
        }

        [Authorize(Roles = Pages.MainMenu.MyComplaint.RoleName)]
        public IActionResult IndexCreatedByComplaint()
        {
            return View();
        }
        [Authorize(Roles = Pages.MainMenu.AssignToMe.RoleName)]
        public IActionResult IndexAssignToComplaint()
        {
            return View();
        }

        [Authorize(Roles = Pages.MainMenu.ResolvedComplaint.RoleName)]
        public IActionResult IndexResolvedComplained(ResolvedComplainedViewModel vm)
        {
            return View();
        }


        [HttpPost]
        public IActionResult GetDataTabelData(string ComplaintStatus, string ComplaintList)
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
                if (ComplaintStatus == "Resolved")
                    _GetGridItem = _iCommon.GetComplaintViewItem().Where(x => x.Status == ComplaintStatus);
                else if (ComplaintList != "AssignTo")
                    _GetGridItem = _iCommon.GetComplaintViewItem().Where(x => x.Complainant == HttpContext.User.Identity.Name);
                else
                _GetGridItem = _iCommon.GetComplaintViewItem();

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
                    || obj.AssignTo.ToLower().Contains(searchValue)
                    || obj.Priority.ToLower().Contains(searchValue)
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

        public IActionResult DetailsOnly(long? id)
        {
            if (id == null) return NotFound();
            ComplaintManageViewModel vm = new ComplaintManageViewModel();
            vm.ComplaintGridViewModel = _iCommon.GetComplaintViewItem().Where(x => x.Id == id).SingleOrDefault();
            if (vm.ComplaintGridViewModel == null) return NotFound();
            return PartialView("_ComplaintDetails", vm);
        }

        public IActionResult Details(long? id)
        {
            ComplaintManageViewModel _ComplaintManageViewModel = new ComplaintManageViewModel();
            if (id == null) return NotFound();
            _ComplaintManageViewModel.ComplaintGridViewModel = _iCommon.GetComplaintViewItem().Where(x => x.Id == id).SingleOrDefault();
            _ComplaintManageViewModel.CommentList = _context.Comment.Where(x => x.ComplaintId == id && x.Cancelled == false).ToList();
            _ComplaintManageViewModel.AttachmentFileList = _context.AttachmentFile.Where(x => x.ComplaintId == id && x.Cancelled == false).ToList();

            _ComplaintManageViewModel.ComplaintGridViewModel.CurrentUserId = HttpContext.User.Identity.Name;

            if (_ComplaintManageViewModel == null)
                return NotFound();

            var _IsInRole = User.IsInRole("Admin");
            if (_IsInRole)
                return PartialView("_DetailsAdmin", _ComplaintManageViewModel);
            else
                return PartialView("_Details", _ComplaintManageViewModel);
        }

        public async Task<IActionResult> AddEdit(long? id)
        {
            ViewBag.ddlComplaintCategory = new SelectList(_iCommon.LoadddlComplaintCategory(), "Id", "Name");
            ViewBag.ddlComplaintStatus = new SelectList(_iCommon.LoadddlComplaintStatus(), "Id", "Name");
            ViewBag.ddlPriority = new SelectList(_iCommon.LoadddlPriority(), "Id", "Name");

            var _IsInRole = User.IsInRole("Admin");
            if (_IsInRole)
            {
                ViewBag.ddlAssignTo = new SelectList(await _iCommon.LoadddlAssignToGeneral(), "ApplicationUserId", "Name");
            }
            else
            {
                ViewBag.ddlAssignTo = new SelectList(await _iCommon.LoadddlAssignToGeneral(), "ApplicationUserId", "Name");
            }

            ComplaintManageViewModel vm = new();
            if (id > 0)
            {
                vm.ComplaintCRUDViewModel = await _context.Complaint.Where(x => x.Id == id).SingleOrDefaultAsync();

                ComplaintGridViewModel _ComplaintGridViewModel = new();
                _ComplaintGridViewModel.Id = vm.ComplaintCRUDViewModel.Id;
                vm.ComplaintGridViewModel = _ComplaintGridViewModel;
                vm.ComplaintCRUDViewModel.IsAdmin = User.IsInRole("Admin");
                vm.ComplaintCRUDViewModel.CurrentUserId = HttpContext.User.Identity.Name;

                vm.CommentList = _context.Comment.Where(x => x.ComplaintId == id && x.Cancelled == false).ToList();
                vm.AttachmentFileList = _context.AttachmentFile.Where(x => x.ComplaintId == id && x.Cancelled == false).ToList();
                return PartialView("_Edit", vm);
            }
            else
            {
                ComplaintCRUDViewModel _ComplaintCRUDViewModel = new();
                _ComplaintCRUDViewModel.IsAdmin = User.IsInRole("Admin");
                return PartialView("_Add", _ComplaintCRUDViewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddComplaint(ComplaintCRUDViewModel vm)
        {
            try
            {
                var TraceURLLoc = vm.Id;
                if (vm.Id == -1)
                    vm.Id = 0;

                Complaint _Complaint = new();
                var _Code = "C" + DateTime.Now.ToString("yyyyMMddHHmmss");
                _Complaint = vm;
                _Complaint.Complainant = HttpContext.User.Identity.Name;
                _Complaint.Status = ComplaintStatusTypes.New;
                _Complaint.Code = _Code;
                _Complaint.CreatedDate = DateTime.Now;
                _Complaint.ModifiedDate = DateTime.Now;
                _Complaint.CreatedBy = HttpContext.User.Identity.Name;
                _Complaint.ModifiedBy = HttpContext.User.Identity.Name;
                _context.Add(_Complaint);
                var result = await _context.SaveChangesAsync();

                if (vm.AttachmentFile != null)
                {
                    AttachmentFile _AttachmentFile = new();
                    _AttachmentFile.FileName = "/upload/" + _iCommon.UploadedFile(vm.AttachmentFile);
                    _AttachmentFile.ComplaintId = _Complaint.Id;
                    _AttachmentFile.CreatedBy = HttpContext.User.Identity.Name;
                    _AttachmentFile.ModifiedBy = HttpContext.User.Identity.Name;
                    await _iCommon.AddAttachmentFile(vm.AttachmentFile, _AttachmentFile);
                }

                await SendNotification(_Complaint, "New Complaint Created Successfully");
                vm.Id = _Complaint.Id;
                return new JsonResult(vm);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateComplaint(ComplaintCRUDViewModel vm)
        {
            try
            {
                Complaint _Complaint = new();
                _Complaint = await _context.Complaint.FindAsync(vm.Id);
                if (_Complaint.AssignTo != vm.AssignTo)
                    await SendNotification(_Complaint, "Complaint Assignee has been Changed");

                vm.CreatedDate = _Complaint.CreatedDate;
                vm.CreatedBy = _Complaint.CreatedBy;
                vm.ModifiedDate = DateTime.Now;
                vm.ModifiedBy = HttpContext.User.Identity.Name;
                _context.Entry(_Complaint).CurrentValues.SetValues(vm);
                await _context.SaveChangesAsync();
                await SendNotification(_Complaint, "Complaint updated by, " + HttpContext.User.Identity.Name);

                vm.Id = _Complaint.Id;
                return new JsonResult(vm);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task SendNotification(Complaint _Complaint, string _NotificationMessage)
        {
            NotificationCRUDViewModel _NotificationCRUDViewModel = _Complaint;
            UserProfile _UserProfile = new UserProfile();
            if (_Complaint.AssignTo != null)
            {
                _UserProfile = _context.UserProfile.Where(x => x.ApplicationUserId == _Complaint.AssignTo).SingleOrDefault();
                _NotificationCRUDViewModel.NotificationFor = _UserProfile.Email;
            }
            else
            {
                var _IsInRole = User.IsInRole("Admin");
                _NotificationCRUDViewModel.NotificationFor = _IsInRole == true ? _Complaint.Complainant : "Admin";
            }
            _NotificationCRUDViewModel.CreatedBy = HttpContext.User.Identity.Name;
            _NotificationCRUDViewModel.ModifiedBy = HttpContext.User.Identity.Name;
            _NotificationCRUDViewModel.Message = _NotificationMessage;

            await _iCommon.AddNotification(_NotificationCRUDViewModel);

            //Add notification for Complainant Creator
            if (_Complaint.CreatedBy != _UserProfile.Email)
            {
                _NotificationCRUDViewModel.NotificationFor = _Complaint.CreatedBy;
                await _iCommon.AddNotification(_NotificationCRUDViewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewComment(ComplaintManageViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var _IsInRole = User.IsInRole("Admin");
                    await _iCommon.AddComment(vm.StatusUpdateViewModel.ComplaintId, vm.StatusUpdateViewModel.NewComment, _IsInRole, HttpContext.User.Identity.Name);
                    var _Complaint = await _context.Complaint.FindAsync(vm.StatusUpdateViewModel.ComplaintId);

                    await SendNotification(_Complaint, vm.StatusUpdateViewModel.NewComment);

                    return new JsonResult(vm.StatusUpdateViewModel);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            TempData["errorAlert"] = "Operation failed.";
            return View("IndexAllComplained");
        }


        [HttpPost]
        public async Task<JsonResult> AddNewAttachmentFile(AddNewFileViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var _IsInRole = User.IsInRole("Admin");
                    IFormFile _IFormFile = null;
                    foreach (var item in vm.AttachmentFile)
                    {
                        _IFormFile = item;
                    }

                    AttachmentFile _AttachmentFile = new AttachmentFile();
                    _AttachmentFile.IsAdmin = _IsInRole;
                    _AttachmentFile.FileName = "/upload/" + _iCommon.UploadedFile(_IFormFile);
                    _AttachmentFile.ComplaintId = vm.ComplaintId;
                    _AttachmentFile.CreatedBy = HttpContext.User.Identity.Name;
                    _AttachmentFile.ModifiedBy = HttpContext.User.Identity.Name;

                    await _iCommon.AddAttachmentFile(_IFormFile, _AttachmentFile);
                    return new JsonResult(vm);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            TempData["errorAlert"] = "Operation failed.";
            return new JsonResult(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Submit(Int64 id)
        {
            try
            {
                var _Complaint = await _context.Complaint.FindAsync(id);
                _Complaint.ModifiedDate = DateTime.Now;
                _Complaint.ModifiedBy = HttpContext.User.Identity.Name;
                _Complaint.Status = ComplaintStatusTypes.Submited;

                _context.Update(_Complaint);
                await _context.SaveChangesAsync();
                return new JsonResult(_Complaint);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Int64 id)
        {
            try
            {
                var _Complaint = await _context.Complaint.FindAsync(id);
                _Complaint.ModifiedDate = DateTime.Now;
                _Complaint.ModifiedBy = HttpContext.User.Identity.Name;
                _Complaint.Cancelled = true;

                _context.Update(_Complaint);
                await _context.SaveChangesAsync();
                return new JsonResult(_Complaint);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public FileResult DownloadFile(Int64 id)
        {
            try
            {
                var _GetDownloadDetails = _iCommon.GetDownloadDetails(id);
                return File(_GetDownloadDetails.Item1, "application/octet-stream", _GetDownloadDetails.Item2);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAttachmentFile(Int64 id)
        {
            try
            {
                var _AttachmentFile = await _context.AttachmentFile.FindAsync(id);
                _AttachmentFile.ModifiedDate = DateTime.Now;
                _AttachmentFile.ModifiedBy = HttpContext.User.Identity.Name;
                _AttachmentFile.Cancelled = true;
                _AttachmentFile.IsDeleted = true;

                _context.Update(_AttachmentFile);
                await _context.SaveChangesAsync();
                return new JsonResult(_AttachmentFile);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool IsExists(long id)
        {
            return _context.Complaint.Any(e => e.Id == id);
        }
    }
}