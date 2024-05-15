using ComplaintMngSys.Data;
using ComplaintMngSys.Models;
using ComplaintMngSys.Models.NotificationViewModel;
using ComplaintMngSys.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace ComplaintMngSys.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class NotificationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICommon _iCommon;

        public NotificationController(ApplicationDbContext context, ICommon iCommon)
        {
            _context = context;
            _iCommon = iCommon;
        }

        [Authorize(Roles = Pages.MainMenu.Notification.RoleName)]
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

                IQueryable<NotificationGridViewModel> _GetGridItem = null;
                var _IsInRole = User.IsInRole("Admin");
                if (_IsInRole)
                    _GetGridItem = GetGridItem().Where(x => x.NotificationFor == "Admin" || x.NotificationFor == HttpContext.User.Identity.Name);
                else
                    _GetGridItem = GetGridItem().Where(x => x.NotificationFor == HttpContext.User.Identity.Name);


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
                    || obj.ComplaintTitle.ToLower().Contains(searchValue)
                    || obj.Message.ToLower().Contains(searchValue)
                    || obj.IsRead.ToString().ToLower().Contains(searchValue)
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

        private IQueryable<NotificationGridViewModel> GetGridItem()
        {
            try
            {
                return (from _Notification in _context.Notification
                        join _Complaint in _context.Complaint
                        on _Notification.ComplaintId equals _Complaint.Id
                        where _Notification.IsRead == false
                        select new NotificationGridViewModel
                        {
                            Id = _Notification.Id,
                            ComplaintId = _Notification.ComplaintId,
                            ComplaintTitle = _Complaint.Name,
                            Message = _Notification.Message,
                            IsRead = _Notification.IsRead,
                            NotificationFor = _Notification.NotificationFor,
                            CreatedDateDisplay = String.Format("{0:f}", _Notification.CreatedDate),
                            ModifiedDate = _Notification.ModifiedDate,
                            CreatedBy = _Notification.CreatedBy,
                            ModifiedBy = _Notification.ModifiedBy,

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
            NotificationGridViewModel vm = await GetGridItem().Where(x => x.Id == id).SingleOrDefaultAsync();
            if (vm == null) return NotFound();
            return PartialView("_Details", vm);
        }


        [HttpPost]
        public async Task<IActionResult> MarkedAsRead(Int64 id)
        {
            try
            {
                var _Notification = await _context.Notification.FindAsync(id);
                _Notification.ModifiedDate = DateTime.Now;
                _Notification.ModifiedBy = HttpContext.User.Identity.Name;
                _Notification.IsRead = true;

                _context.Update(_Notification);
                await _context.SaveChangesAsync();
                return new JsonResult(_Notification);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> MarkedAllAsRead(Int64 id)
        {
            try
            {
                List<Notification> _Notification = new List<Notification>();
                var _IsInRole = User.IsInRole("Admin");
                var _LoginUser = HttpContext.User.Identity.Name;
                if (_IsInRole)
                {
                    _Notification = _context.Notification.Where(x => x.NotificationFor == "Admin" || x.NotificationFor == _LoginUser && x.IsRead == false).ToList();
                }
                else
                {
                    _Notification = _context.Notification.Where(x => x.NotificationFor == _LoginUser && x.IsRead == false).ToList();
                }

                foreach (var item in _Notification)
                {
                    item.ModifiedDate = DateTime.Now;
                    item.ModifiedBy = HttpContext.User.Identity.Name;
                    item.IsRead = true;
                }

                _context.UpdateRange(_Notification);
                await _context.SaveChangesAsync();
                return new JsonResult(_Notification);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool IsExists(long id)
        {
            return _context.Notification.Any(e => e.Id == id);
        }
    }
}