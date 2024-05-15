namespace ComplaintMngSys.Models.AccountViewModels
{
    public class CreateUserAccountViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string ConfirmPassword { get; set; }

        public static implicit operator ApplicationUser(CreateUserAccountViewModel vm)
        {
            return new ApplicationUser
            {
                UserName = vm.Email,
                PhoneNumber = vm.PhoneNumber,
                PhoneNumberConfirmed = vm.PhoneNumberConfirmed,
                Email = vm.Email,
                EmailConfirmed = vm.EmailConfirmed,
            };
        }
    }
}
