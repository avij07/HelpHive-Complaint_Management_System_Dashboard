using System;

namespace ComplaintMngSys.Models.CommentViewModel
{
    public class CommentGridViewModel : EntityBase
    {
        public Int64 Id { get; set; }
        public Int64 ComplaintId { get; set; }
        public string ComplaintTitle { get; set; }
        public string Message { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsAdmin { get; set; }
    }
}
