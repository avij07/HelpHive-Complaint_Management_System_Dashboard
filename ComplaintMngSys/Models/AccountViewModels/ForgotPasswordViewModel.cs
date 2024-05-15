using System.ComponentModel.DataAnnotations;

namespace ComplaintMngSys.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
