using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using ComplaintMngSys.Data;
using ComplaintMngSys.Models.LoginHistoryViewModel;
using ComplaintMngSys.Services;

namespace ComplaintMngSys.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class LoginHistoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICommon _iCommon;
        private readonly IWebHostEnvironment _iWebHostEnvironment;
        private IHttpContextAccessor _accessor;

        public LoginHistoryController(ApplicationDbContext context, ICommon iCommon, IWebHostEnvironment iWebHostEnvironment, IHttpContextAccessor accessor)
        {
            _context = context;
            _iCommon = iCommon;
            _iWebHostEnvironment = iWebHostEnvironment;
            _accessor = accessor;
        }

        [Authorize(Roles = Pages.MainMenu.LoginHistory.RoleName)]
        public IActionResult Index()
        {
            var userAgent = HttpContext.Request.Headers["User-Agent"];
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
                    _GetGridItem = GetGridItem().OrderBy(sortColumn + " " + sortColumnAscDesc);
                }

                //Search
                if (!string.IsNullOrEmpty(searchValue))
                {
                    searchValue = searchValue.ToLower();
                    _GetGridItem = _GetGridItem.Where(obj => obj.Id.ToString().Contains(searchValue)
                    || obj.UserName.ToLower().Contains(searchValue)
                    || obj.Duration.ToString().ToLower().Contains(searchValue)
                    || obj.PublicIP.ToLower().Contains(searchValue)

                    || obj.Latitude.ToLower().Contains(searchValue)
                    || obj.Longitude.ToLower().Contains(searchValue)
                    || obj.Browser.ToLower().Contains(searchValue)
                    || obj.OperatingSystem.ToLower().Contains(searchValue)
                    || obj.Device.ToLower().Contains(searchValue)
                    || obj.Action.ToLower().Contains(searchValue)
                    || obj.ActionStatus.ToLower().Contains(searchValue)
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

        private IQueryable<LoginHistoryGridViewModel> GetGridItem()
        {
            try
            {
                var result = (from _LoginHistory in _context.LoginHistory
                              where _LoginHistory.Cancelled == false
                              select new LoginHistoryGridViewModel
                              {
                                  Id = _LoginHistory.Id,
                                  UserName = _LoginHistory.UserName,
                                  LoginTime = String.Format("{0:f}", _LoginHistory.LoginTime),
                                  LogoutTime = String.Format("{0:f}", _LoginHistory.LogoutTime),

                                  Duration = Math.Round(_LoginHistory.Duration, 2),
                                  PublicIP = _LoginHistory.PublicIP,
                                  Latitude = Math.Round(Convert.ToDouble(_LoginHistory.Latitude), 2).ToString(),
                                  Longitude = Math.Round(Convert.ToDouble(_LoginHistory.Longitude), 2).ToString(),
                                  Browser = _LoginHistory.Browser,
                                  OperatingSystem = _LoginHistory.OperatingSystem,
                                  Device = _LoginHistory.Device,
                                  Action = _LoginHistory.Action,
                                  ActionStatus = _LoginHistory.ActionStatus,
                                  CreatedDate = _LoginHistory.CreatedDate,

                              }).OrderByDescending(x => x.Id);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null) return NotFound();
            LoginHistoryCRUDViewModel vm = await _context.LoginHistory.FirstOrDefaultAsync(m => m.Id == id);
            if (vm == null) return NotFound();
            return PartialView("_Details", vm);
        }
    }
}
