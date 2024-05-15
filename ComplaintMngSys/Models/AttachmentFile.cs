using System;

namespace ComplaintMngSys.Models
{
    public class AttachmentFile : EntityBase
    {
        public Int64 Id { get; set; }
        public Int64 ComplaintId { get; set; }
        public string FilePath { get; set; } = "/upload/blank-doc.txt";
        public string ContentType { get; set; }
        public string FileName { get; set; }
        public Int64 Length { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsDeleted { get; set; }
    }
}
