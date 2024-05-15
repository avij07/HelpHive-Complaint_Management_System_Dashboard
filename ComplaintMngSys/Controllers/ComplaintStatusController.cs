using ComplaintMngSys.Data;
using ComplaintMngSys.Models;
using ComplaintMngSys.Models.ComplaintStatusViewModel;
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
    public class ComplaintStatusController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICommon _iCommon;

        public ComplaintStatusController(ApplicationDbContext context, ICommon iCommon)
        {
            _context = context;
            _iCommon = iCommon;
        }

        [Authorize(Roles = Pages.MainMenu.ComplaintStatus.RoleName)]
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
                    || obj.Name.ToLower().Contains(searchValue)
                    || obj.Description.ToLower().Contains(searchValue)
                    || obj.CreatedDate.ToString().ToLower().Contains(searchValue)
                    || obj.ModifiedDate.ToString().ToLower().Contains(searchValue)
                    || obj.CreatedBy.ToLower().Contains(searchValue)
                    || obj.ModifiedBy.ToLower().Contains(searchValue)
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

        private IQueryable<ComplaintStatusGridViewModel> GetGridItem()
        {
            try
            {
                return (from _ComplaintStatus in _context.ComplaintStatus
                        where _ComplaintStatus.Cancelled == false
                        select new ComplaintStatusGridViewModel
                        {
                            Id = _ComplaintStatus.Id,
                            Name = _ComplaintStatus.Name,
                            Description = _ComplaintStatus.Description,
                            CreatedDateDisplay = String.Format("{0:f}", _ComplaintStatus.CreatedDate),
                            ModifiedDateDisplay = String.Format("{0:f}", _ComplaintStatus.CreatedDate),
                            CreatedBy = _ComplaintStatus.CreatedBy,
                            ModifiedBy = _ComplaintStatus.ModifiedBy,
                        }).OrderBy(x => x.Id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null) return NotFound();
            ComplaintStatusCRUDViewModel vm = await _context.ComplaintStatus.FirstOrDefaultAsync(m => m.Id == id);
            if (vm == null) return NotFound();
            return PartialView("_Details", vm);
        }

        public async Task<IActionResult> AddEdit(int id)
        {
            ComplaintStatusCRUDViewModel vm = new ComplaintStatusCRUDViewModel();
            if (id > 0) vm = await _context.ComplaintStatus.Where(x => x.Id == id).SingleOrDefaultAsync();
            return PartialView("_AddEdit", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEdit(ComplaintStatusCRUDViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        ComplaintStatus _ComplaintStatus = new ComplaintStatus();
                        if (vm.Id > 0)
                        {
                            _ComplaintStatus = await _context.ComplaintStatus.FindAsync(vm.Id);

                            vm.CreatedDate = _ComplaintStatus.CreatedDate;
                            vm.CreatedBy = _ComplaintStatus.CreatedBy;
                            vm.ModifiedDate = DateTime.Now;
                            vm.ModifiedBy = HttpContext.User.Identity.Name;
                            _context.Entry(_ComplaintStatus).CurrentValues.SetValues(vm);
                            await _context.SaveChangesAsync();
                            TempData["successAlert"] = "Complaint Status Updated Successfully. ID: " + _ComplaintStatus.Id;
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            _ComplaintStatus = vm;
                            _ComplaintStatus.CreatedDate = DateTime.Now;
                            _ComplaintStatus.ModifiedDate = DateTime.Now;
                            _ComplaintStatus.CreatedBy = HttpContext.User.Identity.Name;
                            _ComplaintStatus.ModifiedBy = HttpContext.User.Identity.Name;
                            _context.Add(_ComplaintStatus);
                            await _context.SaveChangesAsync();
                            TempData["successAlert"] = "Complaint Status Created Successfully. ID: " + _ComplaintStatus.Id;
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
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var _ComplaintStatus = await _context.ComplaintStatus.FindAsync(id);
                _ComplaintStatus.ModifiedDate = DateTime.Now;
                _ComplaintStatus.ModifiedBy = HttpContext.User.Identity.Name;
                _ComplaintStatus.Cancelled = true;

                _context.Update(_ComplaintStatus);
                await _context.SaveChangesAsync();
                return new JsonResult(_ComplaintStatus);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool IsExists(long id)
        {
            return _context.ComplaintStatus.Any(e => e.Id == id);
        }
    }
}
