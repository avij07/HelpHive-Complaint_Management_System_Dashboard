using System.ComponentModel.DataAnnotations;

namespace ComplaintMngSys.Models.CommonViewModel
{
    public class SMTPEmailSettingViewModel : EntityBase
    {
        public static implicit operator SMTPEmailSettingViewModel(SMTPEmailSetting _SMTPEmailSetting)
        {
            return new SMTPEmailSettingViewModel
            {
                Id = _SMTPEmailSetting.Id,
                UserName = _SMTPEmailSetting.UserName,
                Password = _SMTPEmailSetting.Password,
                Host = _SMTPEmailSetting.Host,
                Port = _SMTPEmailSetting.Port,
                IsSSL = _SMTPEmailSetting.IsSSL,
                FromEmail = _SMTPEmailSetting.FromEmail,
                FromFullName = _SMTPEmailSetting.FromFullName,
                IsDefault = _SMTPEmailSetting.IsDefault,

                CreatedDate = _SMTPEmailSetting.CreatedDate,
                ModifiedDate = _SMTPEmailSetting.ModifiedDate,
                CreatedBy = _SMTPEmailSetting.CreatedBy,
                ModifiedBy = _SMTPEmailSetting.ModifiedBy,
            };
        }

        public static implicit operator SMTPEmailSetting(SMTPEmailSettingViewModel vm)
        {
            return new SMTPEmailSetting
            {
                Id = vm.Id,
                UserName = vm.UserName,
                Password = vm.Password,
                Host = vm.Host,
                Port = vm.Port,
                IsSSL = vm.IsSSL,
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
        [Display(Name = "User Name")]
        [Required]
        public string UserName { get; set; }
        [Display(Name = "Password")]
        [Required]
        public string Password { get; set; }
        [Display(Name = "Host(SMTP)")]
        [Required]
        public string Host { get; set; }
        [Display(Name = "Port(SMTP)")]
        [Required]
        public int Port { get; set; }
        [Display(Name = "SSL Enabled")]
        [Required]
        public bool IsSSL { get; set; }
        [Display(Name = "From Email")]
        [Required]
        public string FromEmail { get; set; }
        [Display(Name = "From Full Name")]
        [Required]
        public string FromFullName { get; set; }
        public bool IsDefault { get; set; }
    }
}
