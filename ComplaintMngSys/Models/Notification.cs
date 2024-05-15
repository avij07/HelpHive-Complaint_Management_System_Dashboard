using System;

namespace ComplaintMngSys.Models
{
    public class Notification : EntityBase
    {
        public Int64 Id { get; set; }
        public Int64 ComplaintId { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public string NotificationFor { get; set; }
    }
}
