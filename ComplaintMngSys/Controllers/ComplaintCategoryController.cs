using ComplaintMngSys.Data;
using ComplaintMngSys.Models;
using ComplaintMngSys.Models.ComplaintCategoryViewModel;
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
    public class ComplaintCategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICommon _iCommon;

        public ComplaintCategoryController(ApplicationDbContext context, ICommon iCommon)
        {
            _context = context;
            _iCommon = iCommon;
        }

        [Authorize(Roles = Pages.MainMenu.ComplaintCategory.RoleName)]
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

        private IQueryable<ComplaintCategoryGridViewModel> GetGridItem()
        {
            try
            {
                return (from _ComplaintCategory in _context.ComplaintCategory
                        where _ComplaintCategory.Cancelled == false
                        select new ComplaintCategoryGridViewModel
                        {
                            Id = _ComplaintCategory.Id,
                            Name = _ComplaintCategory.Name,
                            Description = _ComplaintCategory.Description,
                            CreatedDateDisplay = String.Format("{0:f}", _ComplaintCategory.CreatedDate),
                            ModifiedDateDisplay = String.Format("{0:f}", _ComplaintCategory.CreatedDate),
                            CreatedBy = _ComplaintCategory.CreatedBy,
                            ModifiedBy = _ComplaintCategory.ModifiedBy,
                        }).OrderBy(x => x.Id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> Details(long? id)
        {
            try
            {
                if (id == null) return NotFound();
                ComplaintCategoryCRUDViewModel vm = await _context.ComplaintCategory.FirstOrDefaultAsync(m => m.Id == id);
                if (vm == null) return NotFound();
                return PartialView("_Details", vm);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> AddEdit(int id)
        {
            ComplaintCategoryCRUDViewModel vm = new ComplaintCategoryCRUDViewModel();
            if (id > 0) vm = await _context.ComplaintCategory.Where(x => x.Id == id).SingleOrDefaultAsync();
            return PartialView("_AddEdit", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEdit(ComplaintCategoryCRUDViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        ComplaintCategory _ComplaintCategory = new ComplaintCategory();
                        if (vm.Id > 0)
                        {
                            _ComplaintCategory = await _context.ComplaintCategory.FindAsync(vm.Id);

                            vm.CreatedDate = _ComplaintCategory.CreatedDate;
                            vm.CreatedBy = _ComplaintCategory.CreatedBy;
                            vm.ModifiedDate = DateTime.Now;
                            vm.ModifiedBy = HttpContext.User.Identity.Name;
                            _context.Entry(_ComplaintCategory).CurrentValues.SetValues(vm);
                            await _context.SaveChangesAsync();
                            TempData["successAlert"] = "Complaint Category Updated Successfully. ID: " + _ComplaintCategory.Id;
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            _ComplaintCategory = vm;
                            _ComplaintCategory.CreatedDate = DateTime.Now;
                            _ComplaintCategory.ModifiedDate = DateTime.Now;
                            _ComplaintCategory.CreatedBy = HttpContext.User.Identity.Name;
                            _ComplaintCategory.ModifiedBy = HttpContext.User.Identity.Name;
                            _context.Add(_ComplaintCategory);
                            await _context.SaveChangesAsync();
                            TempData["successAlert"] = "Complaint Category Created Successfully. ID: " + _ComplaintCategory.Id;
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
                var _ComplaintCategory = await _context.ComplaintCategory.FindAsync(id);
                _ComplaintCategory.ModifiedDate = DateTime.Now;
                _ComplaintCategory.ModifiedBy = HttpContext.User.Identity.Name;
                _ComplaintCategory.Cancelled = true;

                _context.Update(_ComplaintCategory);
                await _context.SaveChangesAsync();
                return new JsonResult(_ComplaintCategory);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool IsExists(long id)
        {
            return _context.ComplaintCategory.Any(e => e.Id == id);
        }
    }
}
