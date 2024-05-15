using System;
using System.ComponentModel.DataAnnotations;

namespace ComplaintMngSys.Models.ComplaintStatusViewModel
{
    public class ComplaintStatusCRUDViewModel : EntityBase
    {
        [Display(Name = "SL")]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public static implicit operator ComplaintStatusCRUDViewModel(ComplaintStatus _ComplaintStatus)
        {
            return new ComplaintStatusCRUDViewModel
            {
                Id = _ComplaintStatus.Id,
                Name = _ComplaintStatus.Name,
                Description = _ComplaintStatus.Description,
                CreatedDate = _ComplaintStatus.CreatedDate,
                ModifiedDate = _ComplaintStatus.ModifiedDate,
                CreatedBy = _ComplaintStatus.CreatedBy,
                ModifiedBy = _ComplaintStatus.ModifiedBy,
                Cancelled = _ComplaintStatus.Cancelled,

            };
        }

        public static implicit operator ComplaintStatus(ComplaintStatusCRUDViewModel vm)
        {
            return new ComplaintStatus
            {
                Id = vm.Id,
                Name = vm.Name,
                Description = vm.Description,
                CreatedDate = vm.CreatedDate,
                ModifiedDate = vm.ModifiedDate,
                CreatedBy = vm.CreatedBy,
                ModifiedBy = vm.ModifiedBy,
                Cancelled = vm.Cancelled,

            };
        }
    }
}