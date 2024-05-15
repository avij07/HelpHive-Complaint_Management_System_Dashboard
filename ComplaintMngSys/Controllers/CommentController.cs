using ComplaintMngSys.Data;
using ComplaintMngSys.Models;
using ComplaintMngSys.Models.CommentViewModel;
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
    public class CommentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICommon _iCommon;

        public CommentController(ApplicationDbContext context, ICommon iCommon)
        {
            _context = context;
            _iCommon = iCommon;
        }

        [Authorize(Roles = Pages.MainMenu.Comment.RoleName)]
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
                    || obj.Message.ToLower().Contains(searchValue)
                    || obj.IsDeleted.ToString().ToLower().Contains(searchValue)
                    || obj.CreatedDate.ToString().ToLower().Contains(searchValue)
                    || obj.ModifiedDate.ToString().ToLower().Contains(searchValue)
                    || obj.CreatedBy.ToLower().Contains(searchValue)

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

        private IQueryable<CommentGridViewModel> GetGridItem()
        {
            try
            {
                return (from _Comment in _context.Comment
                        join _Complaint in _context.Complaint
                        on _Comment.ComplaintId equals _Complaint.Id
                        select new CommentGridViewModel
                        {
                            Id = _Comment.Id,
                            ComplaintId = _Comment.ComplaintId,
                            ComplaintTitle = _Complaint.Name,
                            Message = _Comment.Message,
                            IsDeleted = _Comment.IsDeleted,
                            CreatedDate = _Comment.CreatedDate,
                            ModifiedDate = _Comment.ModifiedDate,
                            CreatedBy = _Comment.CreatedBy,

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
            CommentCRUDViewModel vm = await _context.Comment.FirstOrDefaultAsync(m => m.Id == id);
            if (vm == null) return NotFound();
            return PartialView("_Details", vm);
        }

        public async Task<IActionResult> AddEdit(int id)
        {
            CommentCRUDViewModel vm = new CommentCRUDViewModel();
            if (id > 0) vm = await _context.Comment.Where(x => x.Id == id).SingleOrDefaultAsync();
            return PartialView("_AddEdit", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEdit(CommentCRUDViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        Comment _Comment = new Comment();
                        if (vm.Id > 0)
                        {
                            _Comment = await _context.Comment.FindAsync(vm.Id);

                            vm.CreatedDate = _Comment.CreatedDate;
                            vm.CreatedBy = _Comment.CreatedBy;
                            vm.ModifiedDate = DateTime.Now;
                            vm.ModifiedBy = HttpContext.User.Identity.Name;
                            _context.Entry(_Comment).CurrentValues.SetValues(vm);
                            await _context.SaveChangesAsync();
                            TempData["successAlert"] = "Comment Updated Successfully. ID: " + _Comment.Id;
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            _Comment = vm;
                            _Comment.CreatedDate = DateTime.Now;
                            _Comment.ModifiedDate = DateTime.Now;
                            _Comment.CreatedBy = HttpContext.User.Identity.Name;
                            _Comment.ModifiedBy = HttpContext.User.Identity.Name;
                            _context.Add(_Comment);
                            await _context.SaveChangesAsync();
                            TempData["successAlert"] = "Comment Created Successfully. ID: " + _Comment.Id;
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
                var _Comment = await _context.Comment.FindAsync(id);
                _Comment.ModifiedDate = DateTime.Now;
                _Comment.ModifiedBy = HttpContext.User.Identity.Name;
                _Comment.Cancelled = true;
                _Comment.IsDeleted = true;
                _context.Update(_Comment);
                await _context.SaveChangesAsync();

                return new JsonResult(_Comment);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool IsExists(long id)
        {
            return _context.Comment.Any(e => e.Id == id);
        }
    }
}
