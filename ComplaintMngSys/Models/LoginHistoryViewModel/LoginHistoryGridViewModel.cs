using System;

namespace ComplaintMngSys.Models.LoginHistoryViewModel
{
    public class LoginHistoryGridViewModel : EntityBase
    {
        public Int64 Id { get; set; }
        public string UserName { get; set; }
        public string LoginTime { get; set; }
        public string LogoutTime { get; set; }
        public double Duration { get; set; }
        public string PublicIP { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Browser { get; set; }
        public string OperatingSystem { get; set; }
        public string Device { get; set; }
        public string Action { get; set; }
        public string ActionStatus { get; set; }

    }
}
