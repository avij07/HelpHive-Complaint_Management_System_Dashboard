using ComplaintMngSys.Data;
using ComplaintMngSys.Models;
using ComplaintMngSys.Models.AttachmentFileViewModel;
using ComplaintMngSys.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace ComplaintMngSys.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class AttachmentFileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICommon _iCommon;

        public AttachmentFileController(ApplicationDbContext context, ICommon iCommon)
        {
            _context = context;
            _iCommon = iCommon;
        }

        [Authorize(Roles = Pages.MainMenu.AttachmentFile.RoleName)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetDataTabelData()
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

                var _GetGridItem = GetGridItem();
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
                    || obj.ComplaintId.ToString().ToLower().Contains(searchValue)
                    || obj.FilePath.ToLower().Contains(searchValue)
                    || obj.IsDeleted.ToString().ToLower().Contains(searchValue)
                    || obj.IsAdmin.ToString().ToLower().Contains(searchValue)
                    || obj.CreatedDate.ToString().ToLower().Contains(searchValue)
                    || obj.ModifiedDate.ToString().ToLower().Contains(searchValue)

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

        private IQueryable<AttachmentFileGridViewModel> GetGridItem()
        {
            try
            {
                return (from _AttachmentFile in _context.AttachmentFile
                        join _Complaint in _context.Complaint
                        on _AttachmentFile.ComplaintId equals _Complaint.Id
                        where _AttachmentFile.Cancelled == false
                        select new AttachmentFileGridViewModel
                        {
                            Id = _AttachmentFile.Id,
                            ComplaintId = _AttachmentFile.ComplaintId,
                            ComplaintName = _Complaint.Name,
                            ContentType = _AttachmentFile.ContentType,
                            FileName = _AttachmentFile.FileName,
                            Length = _AttachmentFile.Length,
                            FilePath = _AttachmentFile.FilePath,
                            IsDeleted = _AttachmentFile.IsDeleted,
                            IsAdmin = _AttachmentFile.IsAdmin,
                            CreatedDate = _AttachmentFile.CreatedDate,
                            ModifiedDate = _AttachmentFile.ModifiedDate,

                        }).OrderByDescending(x => x.Id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null) return NotFound();
            AttachmentFileCRUDViewModel vm = await _context.AttachmentFile.FirstOrDefaultAsync(m => m.Id == id);
            if (vm == null) return NotFound();
            return PartialView("_Details", vm);
        }

        public async Task<IActionResult> AddEdit(int id)
        {
            AttachmentFileCRUDViewModel vm = new AttachmentFileCRUDViewModel();
            if (id > 0) vm = await _context.AttachmentFile.Where(x => x.Id == id).SingleOrDefaultAsync();
            return PartialView("_AddEdit", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEdit(AttachmentFileCRUDViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        AttachmentFile _AttachmentFile = new AttachmentFile();
                        if (vm.Id > 0)
                        {
                            _AttachmentFile = await _context.AttachmentFile.FindAsync(vm.Id);

                            vm.CreatedDate = _AttachmentFile.CreatedDate;
                            vm.CreatedBy = _AttachmentFile.CreatedBy;
                            vm.ModifiedDate = DateTime.Now;
                            vm.ModifiedBy = HttpContext.User.Identity.Name;
                            _context.Entry(_AttachmentFile).CurrentValues.SetValues(vm);
                            await _context.SaveChangesAsync();
                            TempData["successAlert"] = "AttachmentFile Updated Successfully. ID: " + _AttachmentFile.Id;
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            _AttachmentFile = vm;
                            _AttachmentFile.CreatedDate = DateTime.Now;
                            _AttachmentFile.ModifiedDate = DateTime.Now;
                            _AttachmentFile.CreatedBy = HttpContext.User.Identity.Name;
                            _AttachmentFile.ModifiedBy = HttpContext.User.Identity.Name;
                            _context.Add(_AttachmentFile);
                            await _context.SaveChangesAsync();
                            TempData["successAlert"] = "AttachmentFile Created Successfully. ID: " + _AttachmentFile.Id;
                            return RedirectToAction(nameof(Index));
                        }
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

        [HttpPost]
        public async Task<IActionResult> Delete(Int64 id)
        {
            try
            {
                var _AttachmentFile = await _context.AttachmentFile.FindAsync(id);
                _AttachmentFile.ModifiedDate = DateTime.Now;
                _AttachmentFile.ModifiedBy = HttpContext.User.Identity.Name;
                _AttachmentFile.Cancelled = true;

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
            return _context.AttachmentFile.Any(e => e.Id == id);
        }
    }
}
