using System;
using System.ComponentModel.DataAnnotations;

namespace ComplaintMngSys.Models.PriorityViewModel
{
    public class PriorityCRUDViewModel : EntityBase
    {
        [Display(Name = "SL")]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public static implicit operator PriorityCRUDViewModel(Priority _Priority)
        {
            return new PriorityCRUDViewModel
            {
                Id = _Priority.Id,
                Name = _Priority.Name,
                Description = _Priority.Description,
                CreatedDate = _Priority.CreatedDate,
                ModifiedDate = _Priority.ModifiedDate,
                CreatedBy = _Priority.CreatedBy,
                ModifiedBy = _Priority.ModifiedBy,
                Cancelled = _Priority.Cancelled,
            };
        }

        public static implicit operator Priority(PriorityCRUDViewModel vm)
        {
            return new Priority
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