using ComplaintMngSys.Data;
using ComplaintMngSys.Helpers;
using ComplaintMngSys.Models;
using ComplaintMngSys.Models.ReportViewModel;
using ComplaintMngSys.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ComplaintMngSys.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICommon _iCommon;

        public ReportController(ApplicationDbContext context, ICommon iCommon)
        {
            _context = context;
            _iCommon = iCommon;
        }

        [Authorize(Roles = Pages.MainMenu.ComplaintStatusSummaryReport.RoleName)]
        public IActionResult ComplaintStatusSummaryReport()
        {
            ViewBag.StartDate = MinMaxDate().Item1;
            ViewBag.EndDate = MinMaxDate().Item2;
            ManageReportViewModel vm = new ManageReportViewModel();
            vm.ComplaintStatusSummaryViewModel = GetComplaintStatusSummaryData(null, null, false);
            vm.CompanyInfoCRUDViewModel = _iCommon.GetCompanyInfo();
            return View(vm);
        }
        public IActionResult ComplaintStatusSummaryReportCustomRange(string StartDate, string EndDate)
        {
            ManageReportViewModel vm = new ManageReportViewModel();
            vm.ComplaintStatusSummaryViewModel = GetComplaintStatusSummaryData(StartDate, EndDate, true);
            vm.CompanyInfoCRUDViewModel = _iCommon.GetCompanyInfo();
            return View("ComplaintStatusSummaryReport", vm);
        }
        private ComplaintStatusSummaryViewModel GetComplaintStatusSummaryData(string StartDate, string EndDate, bool IsRangeData)
        {
            if (StartDate == null)
                StartDate = DateTime.Today.ToString();
            if (EndDate == null)
                EndDate = DateTime.Today.ToString();
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            List<Complaint> _ComplaintList = new List<Complaint>();
            if (!IsRangeData)
                _ComplaintList = _context.Complaint.Where(x => x.Cancelled == false).ToList();
            else
            {
                _ComplaintList = (from obj in _context.Complaint
                                  where (obj.Cancelled == false) &&
                                  (obj.CreatedDate >= Convert.ToDateTime(StartDate))
                                  && (obj.CreatedDate <= Convert.ToDateTime(EndDate).AddDays(1))
                                  select obj).ToList();
            }
            ComplaintStatusSummaryViewModel _vm = new ComplaintStatusSummaryViewModel
            {
                New = _ComplaintList.Where(x => x.Status == ComplaintStatusTypes.New).Count(),
                Submited = _ComplaintList.Where(x => x.Status == ComplaintStatusTypes.Submited).Count(),
                InProgress = _ComplaintList.Where(x => x.Status == ComplaintStatusTypes.InProgress).Count(),
                Pending = _ComplaintList.Where(x => x.Status == ComplaintStatusTypes.Pending).Count(),
                Resolved = _ComplaintList.Where(x => x.Status == ComplaintStatusTypes.Resolved).Count(),
                Rejected = _ComplaintList.Where(x => x.Status == ComplaintStatusTypes.Rejected).Count(),
                Blocker = _ComplaintList.Where(x => x.Status == ComplaintStatusTypes.Blocker).Count(),
                Closed = _ComplaintList.Where(x => x.Status == ComplaintStatusTypes.Closed).Count(),
                ToDo = _ComplaintList.Where(x => x.Status == ComplaintStatusTypes.ToDo).Count(),
                Total = _ComplaintList.Count()
            };
            return _vm;
        }

        [Authorize(Roles = Pages.MainMenu.AssignedToSummaryReport.RoleName)]
        public IActionResult AssignedToSummaryReport()
        {
            ViewBag.StartDate = MinMaxDate().Item1;
            ViewBag.EndDate = MinMaxDate().Item2;
            ManageReportViewModel vm = new ManageReportViewModel();
            vm.AssignToSummaryViewModel = GetAssignedToSummaryData(null, null, false);
            vm.CompanyInfoCRUDViewModel = _iCommon.GetCompanyInfo();
            return View(vm);
        }
        public IActionResult AssignedToSummaryReportCustomRange(string StartDate, string EndDate)
        {
            ManageReportViewModel vm = new ManageReportViewModel();
            vm.AssignToSummaryViewModel = GetAssignedToSummaryData(StartDate, EndDate, true);
            vm.CompanyInfoCRUDViewModel = _iCommon.GetCompanyInfo();
            return View("AssignedToSummaryReport", vm);
        }

        private List<AssignToSummaryViewModel> GetAssignedToSummaryData(string StartDate, string EndDate, bool IsRangeData)
        {
            if (StartDate == null)
                StartDate = DateTime.Today.ToString();
            if (EndDate == null)
                EndDate = DateTime.Today.ToString();
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            List<Complaint> _ComplaintList = new List<Complaint>();
            if (!IsRangeData)
                _ComplaintList = _context.Complaint.Where(x => x.Cancelled == false).ToList();
            else
            {
                _ComplaintList = (from obj in _context.Complaint
                                  where (obj.Cancelled == false) &&
                                  (obj.CreatedDate >= Convert.ToDateTime(StartDate))
                                  && (obj.CreatedDate <= Convert.ToDateTime(EndDate).AddDays(1))
                                  select obj).ToList();
            }


            var ComplaintGroupBy = _ComplaintList.GroupBy(p => p.AssignTo).Select(g => new
            {
                Id = g.Key == null ? "Unassigned" : g.Key,
                TotalAssigned = g.Count()
            }).ToList();

            var result = (from _ComplaintGroupBy in ComplaintGroupBy
                          join _UserProfile in _context.UserProfile on _ComplaintGroupBy.Id equals _UserProfile.ApplicationUserId
                          select new AssignToSummaryViewModel
                          {
                              ApplicationUserId = _ComplaintGroupBy.Id,
                              UserName = _UserProfile.FirstName + "  " + _UserProfile.LastName,
                              TotalAssigned = _ComplaintGroupBy.TotalAssigned
                          }).ToList();

            var _UnassignedComplaint = ComplaintGroupBy.Where(x => x.Id == "Unassigned").SingleOrDefault();
            if (_UnassignedComplaint != null)
            {
                AssignToSummaryViewModel vm = new AssignToSummaryViewModel();
                vm.UserName = "Unassigned User";
                vm.TotalAssigned = _UnassignedComplaint.TotalAssigned;
                result.Add(vm);
            }
            return result;
        }

        private Tuple<DateTime, DateTime> MinMaxDate()
        {
            DateTime MinDate;
            DateTime MaxDate;
            if (_context.Complaint.Count() == 0)
            {
                MinDate = DateTime.Today;
                MaxDate = DateTime.Today;
            }
            else
            {
                MinDate = _context.Complaint.OrderByDescending(t => t.CreatedDate).Last().CreatedDate;
                MaxDate = _context.Complaint.OrderByDescending(t => t.CreatedDate).First().CreatedDate;
            }
            return Tuple.Create(MinDate, MaxDate);
        }
    }
}
