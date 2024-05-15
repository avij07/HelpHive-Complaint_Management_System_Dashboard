using System.ComponentModel.DataAnnotations;

namespace ComplaintMngSys.Models.CommonViewModel
{
    public class DefaultIdentityOptionsCRUDViewModel : EntityBase
    {
        //password settings
        public int Id { get; set; }
        [Display(Name = "Password Require Digit")]
        public bool PasswordRequireDigit { get; set; }
        [Display(Name = "Password Required Length")]
        public int PasswordRequiredLength { get; set; }
        [Display(Name = "Password Require Non Alphanumeric")]
        public bool PasswordRequireNonAlphanumeric { get; set; }
        [Display(Name = "Password Require Uppercase")]
        public bool PasswordRequireUppercase { get; set; }
        [Display(Name = "Password Require Lowercase")]
        public bool PasswordRequireLowercase { get; set; }
        [Display(Name = "Password Required Unique Chars")]
        public int PasswordRequiredUniqueChars { get; set; }

        //lockout settings
        [Display(Name = "Lockout Default Lockout Time Span In Minutes")]
        public double LockoutDefaultLockoutTimeSpanInMinutes { get; set; }
        [Display(Name = "Lockout Max Failed Access Attempts")]
        public int LockoutMaxFailedAccessAttempts { get; set; }
        [Display(Name = "Lockout Allowed For New Users")]
        public bool LockoutAllowedForNewUsers { get; set; }

        //user settings
        [Display(Name = "User Require Unique Email")]
        public bool UserRequireUniqueEmail { get; set; }
        [Display(Name = "Sign In Require Confirmed Email")]
        public bool SignInRequireConfirmedEmail { get; set; }

        //cookie settings
        [Display(Name = "Cookie Http Only")]
        public bool CookieHttpOnly { get; set; }
        [Display(Name = "Cookie Expiration")]
        public double CookieExpiration { get; set; }
        [Display(Name = "Cookie Expire Time Span")]
        public double CookieExpireTimeSpan { get; set; }
        [Display(Name = "Login Path")]
        public string LoginPath { get; set; }
        [Display(Name = "Logout Path")]
        public string LogoutPath { get; set; }
        [Display(Name = "Access Denied Path")]
        public string AccessDeniedPath { get; set; }
        [Display(Name = "Sliding Expiration")]
        public bool SlidingExpiration { get; set; }


        public static implicit operator DefaultIdentityOptionsCRUDViewModel(DefaultIdentityOptions _DefaultIdentityOptions)
        {
            return new DefaultIdentityOptionsCRUDViewModel
            {
                Id = _DefaultIdentityOptions.Id,
                PasswordRequireDigit = _DefaultIdentityOptions.PasswordRequireDigit,
                PasswordRequiredLength = _DefaultIdentityOptions.PasswordRequiredLength,
                PasswordRequireNonAlphanumeric = _DefaultIdentityOptions.PasswordRequireNonAlphanumeric,
                PasswordRequireUppercase = _DefaultIdentityOptions.PasswordRequireUppercase,
                PasswordRequireLowercase = _DefaultIdentityOptions.PasswordRequireLowercase,
                PasswordRequiredUniqueChars = _DefaultIdentityOptions.PasswordRequiredUniqueChars,
                LockoutDefaultLockoutTimeSpanInMinutes = _DefaultIdentityOptions.LockoutDefaultLockoutTimeSpanInMinutes,
                LockoutMaxFailedAccessAttempts = _DefaultIdentityOptions.LockoutMaxFailedAccessAttempts,
                LockoutAllowedForNewUsers = _DefaultIdentityOptions.LockoutAllowedForNewUsers,
                UserRequireUniqueEmail = _DefaultIdentityOptions.UserRequireUniqueEmail,
                SignInRequireConfirmedEmail = _DefaultIdentityOptions.SignInRequireConfirmedEmail,
                CookieHttpOnly = _DefaultIdentityOptions.CookieHttpOnly,
                CookieExpiration = _DefaultIdentityOptions.CookieExpiration,
                CookieExpireTimeSpan = _DefaultIdentityOptions.CookieExpireTimeSpan,
                LoginPath = _DefaultIdentityOptions.LoginPath,
                LogoutPath = _DefaultIdentityOptions.LogoutPath,
                AccessDeniedPath = _DefaultIdentityOptions.AccessDeniedPath,
                SlidingExpiration = _DefaultIdentityOptions.SlidingExpiration,
                CreatedDate = _DefaultIdentityOptions.CreatedDate,
                ModifiedDate = _DefaultIdentityOptions.ModifiedDate,
                CreatedBy = _DefaultIdentityOptions.CreatedBy,
                ModifiedBy = _DefaultIdentityOptions.ModifiedBy,
                Cancelled = _DefaultIdentityOptions.Cancelled,

            };
        }

        public static implicit operator DefaultIdentityOptions(DefaultIdentityOptionsCRUDViewModel vm)
        {
            return new DefaultIdentityOptions
            {
                Id = vm.Id,
                PasswordRequireDigit = vm.PasswordRequireDigit,
                PasswordRequiredLength = vm.PasswordRequiredLength,
                PasswordRequireNonAlphanumeric = vm.PasswordRequireNonAlphanumeric,
                PasswordRequireUppercase = vm.PasswordRequireUppercase,
                PasswordRequireLowercase = vm.PasswordRequireLowercase,
                PasswordRequiredUniqueChars = vm.PasswordRequiredUniqueChars,
                LockoutDefaultLockoutTimeSpanInMinutes = vm.LockoutDefaultLockoutTimeSpanInMinutes,
                LockoutMaxFailedAccessAttempts = vm.LockoutMaxFailedAccessAttempts,
                LockoutAllowedForNewUsers = vm.LockoutAllowedForNewUsers,
                UserRequireUniqueEmail = vm.UserRequireUniqueEmail,
                SignInRequireConfirmedEmail = vm.SignInRequireConfirmedEmail,
                CookieHttpOnly = vm.CookieHttpOnly,
                CookieExpiration = vm.CookieExpiration,
                CookieExpireTimeSpan = vm.CookieExpireTimeSpan,
                LoginPath = vm.LoginPath,
                LogoutPath = vm.LogoutPath,
                AccessDeniedPath = vm.AccessDeniedPath,
                SlidingExpiration = vm.SlidingExpiration,
                CreatedDate = vm.CreatedDate,
                ModifiedDate = vm.ModifiedDate,
                CreatedBy = vm.CreatedBy,
                ModifiedBy = vm.ModifiedBy,
                Cancelled = vm.Cancelled,

            };
        }
    }
}
