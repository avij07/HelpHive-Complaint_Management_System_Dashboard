using System.ComponentModel.DataAnnotations;

namespace ComplaintMngSys.Models.ComplaintViewModel
{
    public class ComplaintCRUDViewModel : EntityBase
    {
        [Display(Name = "SL")]
        [Required]
        public Int64 Id { get; set; }
        public string Code { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public int Category { get; set; }
        [Display(Name = "Assign To"), Required]
        public string AssignTo { get; set; }
        public int? Priority { get; set; }
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; } = DateTime.Today;
        public int Status { get; set; }
        public string Remarks { get; set; }
        public string Complainant { get; set; }
        public IFormFile AttachmentFile { get; set; }
        public string AttachmentFileName { get; set; }
        public string CurrentURL { get; set; }
        public bool IsAdmin { get; set; }
        public string CurrentUserId { get; set; }

        public static implicit operator ComplaintCRUDViewModel(Complaint _Complaint)
        {
            return new ComplaintCRUDViewModel
            {
                Id = _Complaint.Id,
                Code = _Complaint.Code,
                Name = _Complaint.Name,
                Description = _Complaint.Description,
                Category = _Complaint.Category,
                AssignTo = _Complaint.AssignTo,
                Priority = _Complaint.Priority,
                DueDate = _Complaint.DueDate,
                Status = _Complaint.Status,
                Remarks = _Complaint.Remarks,
                Complainant = _Complaint.Complainant,
                CreatedDate = _Complaint.CreatedDate,
                ModifiedDate = _Complaint.ModifiedDate,
                CreatedBy = _Complaint.CreatedBy,
                ModifiedBy = _Complaint.ModifiedBy,
                Cancelled = _Complaint.Cancelled,

            };
        }

        public static implicit operator Complaint(ComplaintCRUDViewModel vm)
        {
            return new Complaint
            {
                Id = vm.Id,
                Code = vm.Code,
                Name = vm.Name,
                Description = vm.Description,
                Category = vm.Category,
                AssignTo = vm.AssignTo,
                Priority = vm.Priority,
                DueDate = vm.DueDate,
                Status = vm.Status,
                Remarks = vm.Remarks,
                Complainant = vm.Complainant,
                CreatedDate = vm.CreatedDate,
                ModifiedDate = vm.ModifiedDate,
                CreatedBy = vm.CreatedBy,
                ModifiedBy = vm.ModifiedBy,
                Cancelled = vm.Cancelled,
            };
        }
    }
}
