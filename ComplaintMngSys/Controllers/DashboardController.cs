using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using ComplaintMngSys.Data;
using ComplaintMngSys.Models;
using ComplaintMngSys.Models.DashboardViewModel;
using ComplaintMngSys.Helpers;
using ComplaintMngSys.Services;
using ComplaintMngSys.Models.ReportViewModel;

namespace ComplaintMngSys.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICommon _iCommon;
        public DashboardController(ApplicationDbContext context, ICommon iCommon)
        {
            _context = context;
            _iCommon = iCommon;
        }

        [Authorize(Roles = Pages.MainMenu.Dashboard.RoleName)]
        public IActionResult Index()
        {
            try
            {
                DashboardDataViewModel _DashboardDataViewModel = new DashboardDataViewModel();
                DashboardSummaryViewModel _DashboardSummaryViewModel = new DashboardSummaryViewModel();
                List<Complaint> _Complaint = _context.Complaint.ToList();


                _DashboardSummaryViewModel.TotalComplaint = _Complaint.Where(x => x.Cancelled == false).Count();
                _DashboardSummaryViewModel.TotalComplaintResolved = _Complaint.Where(x => x.Cancelled == false && x.Status == ComplaintStatusTypes.Resolved).Count();

                _DashboardSummaryViewModel.TotalUser = _context.ApplicationUser.Count();

                _DashboardDataViewModel.DashboardSummaryViewModel = _DashboardSummaryViewModel;

                _DashboardDataViewModel.ComplaintList = _iCommon.GetComplaintViewItem().Where(x => x.Cancelled == false).OrderByDescending(x => x.CreatedDate).Take(10).ToList();
                return View(_DashboardDataViewModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public JsonResult GetComplaintStatusGroupBy()
        {
            var result = GetGroupByEarnedList().OrderByDescending(x => x.StatusTotal).ToList();
            return new JsonResult(result.ToDictionary(x => x.ComplaintStatusName, x => x.StatusTotal));
        }

        private List<ComplaintStatusPieChartViewModel> GetGroupByEarnedList()
        {
            var ComplaintGroupBy = _context.Complaint.Where(x => x.Cancelled == false).GroupBy(p => p.Status).Select(g => new
            {
                Id = g.Key,
                StatusTotal = g.Count()
            }).ToList();

            var result = (from _ComplaintGroupBy in ComplaintGroupBy
                          join _ComplaintStatus in _context.ComplaintStatus on _ComplaintGroupBy.Id equals _ComplaintStatus.Id
                          select new ComplaintStatusPieChartViewModel
                          {
                              ComplaintStatusName = _ComplaintStatus.Name,
                              StatusTotal = _ComplaintGroupBy.StatusTotal,
                          }).ToList();

            return result;
        }
    }
}