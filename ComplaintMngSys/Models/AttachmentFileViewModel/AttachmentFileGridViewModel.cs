using System;

namespace ComplaintMngSys.Models.AttachmentFileViewModel
{
    public class AttachmentFileGridViewModel : EntityBase
    {
        public Int64 Id { get; set; }
        public Int64 ComplaintId { get; set; }
        public string ComplaintName { get; set; }
        public string FilePath { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
        public Int64 Length { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsAdmin { get; set; }
    }
}
