using System;
using System.ComponentModel.DataAnnotations;

namespace ComplaintMngSys.Models.ComplaintCategoryViewModel
{
    public class ComplaintCategoryCRUDViewModel : EntityBase
    {
        [Display(Name = "SL")]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public static implicit operator ComplaintCategoryCRUDViewModel(ComplaintCategory _ComplaintCategory)
        {
            return new ComplaintCategoryCRUDViewModel
            {
                Id = _ComplaintCategory.Id,
                Name = _ComplaintCategory.Name,
                Description = _ComplaintCategory.Description,
                CreatedDate = _ComplaintCategory.CreatedDate,
                ModifiedDate = _ComplaintCategory.ModifiedDate,
                CreatedBy = _ComplaintCategory.CreatedBy,
                ModifiedBy = _ComplaintCategory.ModifiedBy,
                Cancelled = _ComplaintCategory.Cancelled,
            };
        }

        public static implicit operator ComplaintCategory(ComplaintCategoryCRUDViewModel vm)
        {
            return new ComplaintCategory
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


