using System;

namespace ComplaintMngSys.Models.NotificationViewModel
{
    public class NotificationGridViewModel : EntityBase
    {
        public Int64 Id { get; set; }
        public Int64 ComplaintId { get; set; }
        public string ComplaintTitle { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public string NotificationFor { get; set; }
        public string CommentBy { get; set; }
        public string CreatedDateDisplay { get; set; }
    }
}
