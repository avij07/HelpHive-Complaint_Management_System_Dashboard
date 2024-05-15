using System;

namespace ComplaintMngSys.Models
{
    public class LoginHistory : EntityBase
    {
        public Int64 Id { get; set; }
        public string UserName { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
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
