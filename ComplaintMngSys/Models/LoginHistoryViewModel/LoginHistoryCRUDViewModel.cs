using System;
using System.ComponentModel.DataAnnotations;

namespace ComplaintMngSys.Models.LoginHistoryViewModel
{
    public class LoginHistoryCRUDViewModel : EntityBase
    {
        [Display(Name = "SL")]
        [Required]
        public Int64 Id { get; set; }
        public string UserName { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
        public double Duration { get; set; }
        public string PublicIP { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Browser { get; set; }
        [Display(Name = "Operating System")]
        public string OperatingSystem { get; set; }
        public string Device { get; set; }
        public string Action { get; set; }
        public string ActionStatus { get; set; }


        public static implicit operator LoginHistoryCRUDViewModel(LoginHistory _LoginHistory)
        {
            return new LoginHistoryCRUDViewModel
            {
                Id = _LoginHistory.Id,
                UserName = _LoginHistory.UserName,
                LoginTime = _LoginHistory.LoginTime,
                LogoutTime = _LoginHistory.LogoutTime,
                Duration = _LoginHistory.Duration,
                PublicIP = _LoginHistory.PublicIP,
                Latitude = _LoginHistory.Latitude,
                Longitude = _LoginHistory.Longitude,
                Browser = _LoginHistory.Browser,
                OperatingSystem = _LoginHistory.OperatingSystem,
                Device = _LoginHistory.Device,
                Action = _LoginHistory.Action,
                ActionStatus = _LoginHistory.ActionStatus,
                CreatedDate = _LoginHistory.CreatedDate,
                ModifiedDate = _LoginHistory.ModifiedDate,
                CreatedBy = _LoginHistory.CreatedBy,
                ModifiedBy = _LoginHistory.ModifiedBy,
                Cancelled = _LoginHistory.Cancelled,

            };
        }

        public static implicit operator LoginHistory(LoginHistoryCRUDViewModel vm)
        {
            return new LoginHistory
            {
                Id = vm.Id,
                UserName = vm.UserName,
                LoginTime = vm.LoginTime,
                LogoutTime = vm.LogoutTime,
                Duration = vm.Duration,
                PublicIP = vm.PublicIP,
                Latitude = vm.Latitude,
                Longitude = vm.Longitude,
                Browser = vm.Browser,
                OperatingSystem = vm.OperatingSystem,
                Device = vm.Device,
                Action = vm.Action,
                ActionStatus = vm.ActionStatus,
                CreatedDate = vm.CreatedDate,
                ModifiedDate = vm.ModifiedDate,
                CreatedBy = vm.CreatedBy,
                ModifiedBy = vm.ModifiedBy,
                Cancelled = vm.Cancelled,

            };
        }
    }
}

