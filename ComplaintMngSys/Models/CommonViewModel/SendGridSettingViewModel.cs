using System.ComponentModel.DataAnnotations;

namespace ComplaintMngSys.Models.CommonViewModel
{
    public class SendGridSettingViewModel : EntityBase
    {
        public static implicit operator SendGridSettingViewModel(SendGridSetting _SendGridOptions)
        {
            return new SendGridSettingViewModel
            {
                Id = _SendGridOptions.Id,
                SendGridUser = _SendGridOptions.SendGridUser,
                SendGridKey = _SendGridOptions.SendGridKey,
                FromEmail = _SendGridOptions.FromEmail,
                FromFullName = _SendGridOptions.FromFullName,
                IsDefault = _SendGridOptions.IsDefault,

                CreatedDate = _SendGridOptions.CreatedDate,
                ModifiedDate = _SendGridOptions.ModifiedDate,
                CreatedBy = _SendGridOptions.CreatedBy,
                ModifiedBy = _SendGridOptions.ModifiedBy,
            };
        }

        public static implicit operator SendGridSetting(SendGridSettingViewModel vm)
        {
            return new SendGridSetting
            {
                Id = vm.Id,
                SendGridUser = vm.SendGridUser,
                SendGridKey = vm.SendGridKey,
                FromEmail = vm.FromEmail,
                FromFullName = vm.FromFullName,
                IsDefault = vm.IsDefault,

                CreatedDate = vm.CreatedDate,
                ModifiedDate = vm.ModifiedDate,
                CreatedBy = vm.CreatedBy,
                ModifiedBy = vm.ModifiedBy,
            };
        }

        public int Id { get; set; }
        [Display(Name = "SendGrid User")]
        [Required]
        public string SendGridUser { get; set; }
        [Display(Name = "SendGrid Key")]
        [Required]
        public string SendGridKey { get; set; }
        [Display(Name = "From Email")]
        [Required]
        public string FromEmail { get; set; }
        [Display(Name = "From Full Name")]
        [Required]
        public string FromFullName { get; set; }
        [Display(Name = "Is Default")]
        [Required]
        public bool IsDefault { get; set; }
    }
}
