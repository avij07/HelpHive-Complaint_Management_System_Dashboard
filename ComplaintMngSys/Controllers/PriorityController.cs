using ComplaintMngSys.Data;
using ComplaintMngSys.Models;
using ComplaintMngSys.Models.PriorityViewModel;
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
    public class PriorityController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICommon _iCommon;

        public PriorityController(ApplicationDbContext context, ICommon iCommon)
        {
            _context = context;
            _iCommon = iCommon;
        }

        [Authorize(Roles = Pages.MainMenu.Priority.RoleName)]
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

        private IQueryable<PriorityGridViewModel> GetGridItem()
        {
            try
            {
                return (from _Priority in _context.Priority
                        where _Priority.Cancelled == false
                        select new PriorityGridViewModel
                        {
                            Id = _Priority.Id,
                            Name = _Priority.Name,
                            Description = _Priority.Description,
                            CreatedDateDisplay = String.Format("{0:f}", _Priority.CreatedDate),
                            ModifiedDateDisplay = String.Format("{0:f}", _Priority.CreatedDate),
                            CreatedBy = _Priority.CreatedBy,
                            ModifiedBy = _Priority.ModifiedBy,
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
            PriorityCRUDViewModel vm = await _context.Priority.FirstOrDefaultAsync(m => m.Id == id);
            if (vm == null) return NotFound();
            return PartialView("_Details", vm);
        }

        public async Task<IActionResult> AddEdit(int id)
        {
            PriorityCRUDViewModel vm = new PriorityCRUDViewModel();
            if (id > 0) vm = await _context.Priority.Where(x => x.Id == id).SingleOrDefaultAsync();
            return PartialView("_AddEdit", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEdit(PriorityCRUDViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        Priority _Priority = new Priority();
                        if (vm.Id > 0)
                        {
                            _Priority = await _context.Priority.FindAsync(vm.Id);

                            vm.CreatedDate = _Priority.CreatedDate;
                            vm.CreatedBy = _Priority.CreatedBy;
                            vm.ModifiedDate = DateTime.Now;
                            vm.ModifiedBy = HttpContext.User.Identity.Name;
                            _context.Entry(_Priority).CurrentValues.SetValues(vm);
                            await _context.SaveChangesAsync();
                            TempData["successAlert"] = "Priority Updated Successfully. ID: " + _Priority.Id;
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            _Priority = vm;
                            _Priority.CreatedDate = DateTime.Now;
                            _Priority.ModifiedDate = DateTime.Now;
                            _Priority.CreatedBy = HttpContext.User.Identity.Name;
                            _Priority.ModifiedBy = HttpContext.User.Identity.Name;
                            _context.Add(_Priority);
                            await _context.SaveChangesAsync();
                            TempData["successAlert"] = "Priority Created Successfully. ID: " + _Priority.Id;
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
                var _Priority = await _context.Priority.FindAsync(id);
                _Priority.ModifiedDate = DateTime.Now;
                _Priority.ModifiedBy = HttpContext.User.Identity.Name;
                _Priority.Cancelled = true;

                _context.Update(_Priority);
                await _context.SaveChangesAsync();
                return new JsonResult(_Priority);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool IsExists(long id)
        {
            return _context.Priority.Any(e => e.Id == id);
        }
    }
}
