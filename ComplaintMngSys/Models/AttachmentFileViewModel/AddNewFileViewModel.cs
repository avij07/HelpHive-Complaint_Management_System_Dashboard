using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace ComplaintMngSys.Models.AttachmentFileViewModel
{
    public class AddNewFileViewModel
    {
        public Int64 ComplaintId { get; set; }
        public IList<IFormFile> AttachmentFile { get; set; }
    }
}
