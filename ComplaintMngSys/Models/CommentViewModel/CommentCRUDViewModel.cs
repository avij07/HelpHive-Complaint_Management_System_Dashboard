using System;
using System.ComponentModel.DataAnnotations;

namespace ComplaintMngSys.Models.CommentViewModel
{
    public class CommentCRUDViewModel : EntityBase
    {
        [Display(Name = "SL")]
        [Required]
        public Int64 Id { get; set; }
        public Int64 ComplaintId { get; set; }
        public string Message { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsAdmin { get; set; }


        public static implicit operator CommentCRUDViewModel(Comment _Comment)
        {
            return new CommentCRUDViewModel
            {
                Id = _Comment.Id,
                ComplaintId = _Comment.ComplaintId,
                Message = _Comment.Message,
                IsDeleted = _Comment.IsDeleted,
                IsAdmin = _Comment.IsAdmin,
                CreatedDate = _Comment.CreatedDate,
                ModifiedDate = _Comment.ModifiedDate,
                CreatedBy = _Comment.CreatedBy,
                ModifiedBy = _Comment.ModifiedBy,
                Cancelled = _Comment.Cancelled,

            };
        }

        public static implicit operator Comment(CommentCRUDViewModel vm)
        {
            return new Comment
            {
                Id = vm.Id,
                ComplaintId = vm.ComplaintId,
                Message = vm.Message,
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
