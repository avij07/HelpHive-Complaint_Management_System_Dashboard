using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace ComplaintMngSys.Models.AttachmentFileViewModel
{
    public class AttachmentFileCRUDViewModel : EntityBase
    {
        [Display(Name = "SL")]
        [Required]
        public Int64 Id { get; set; }
        public Int64 ComplaintId { get; set; }
        public string FilePath { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
        public IFormFile AttachmentFile { get; set; }
        public Int64 Length { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsAdmin { get; set; }

        public static implicit operator AttachmentFileCRUDViewModel(AttachmentFile _AttachmentFile)
        {
            return new AttachmentFileCRUDViewModel
            {
                Id = _AttachmentFile.Id,
                ComplaintId = _AttachmentFile.ComplaintId,
                FilePath = _AttachmentFile.FilePath,
                ContentType = _AttachmentFile.ContentType,
                FileName = _AttachmentFile.FileName,
                Length = _AttachmentFile.Length,
                IsDeleted = _AttachmentFile.IsDeleted,
                IsAdmin = _AttachmentFile.IsAdmin,
                CreatedDate = _AttachmentFile.CreatedDate,
                ModifiedDate = _AttachmentFile.ModifiedDate,
                CreatedBy = _AttachmentFile.CreatedBy,
                ModifiedBy = _AttachmentFile.ModifiedBy,
                Cancelled = _AttachmentFile.Cancelled,
            };
        }

        public static implicit operator AttachmentFile(AttachmentFileCRUDViewModel vm)
        {
            return new AttachmentFile
            {
                Id = vm.Id,
                ComplaintId = vm.ComplaintId,
                FilePath = vm.FilePath,
                ContentType = vm.ContentType,
                FileName = vm.FileName,
                Length = vm.Length,
                IsDeleted = vm.IsDeleted,
                IsAdmin = vm.IsAdmin,
                CreatedDate = vm.CreatedDate,
                ModifiedDate = vm.ModifiedDate,
                CreatedBy = vm.CreatedBy,
                ModifiedBy = vm.ModifiedBy,
                Cancelled = vm.Cancelled,
            };
        }
    }
}

