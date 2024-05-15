using ComplaintMngSys.Models.AttachmentFileViewModel;
using ComplaintMngSys.Models.CompanyInfoViewModel;
using System.Collections.Generic;

namespace ComplaintMngSys.Models.ComplaintViewModel
{
    public class ComplaintManageViewModel
    {
        public ComplaintGridViewModel ComplaintGridViewModel { get; set; }
        public ComplaintCRUDViewModel ComplaintCRUDViewModel { get; set; }
        public StatusUpdateViewModel StatusUpdateViewModel { get; set; }
        public AttachmentFileCRUDViewModel AttachmentFileCRUDViewModel { get; set; }
        public List<AttachmentFile> AttachmentFileList { get; set; }
        public List<Comment> CommentList { get; set; }
        public CompanyInfoCRUDViewModel CompanyInfoCRUDViewModel { get; set; }
    }
}
