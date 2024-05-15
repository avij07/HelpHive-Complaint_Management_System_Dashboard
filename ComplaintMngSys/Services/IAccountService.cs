using ComplaintMngSys.Models;
using ComplaintMngSys.Models.UserAccountViewModel;

namespace ComplaintMngSys.Services
{
    public interface IAccountService
    {
        Task<Tuple<ApplicationUser, string>> CreateUserProfile(UserProfileCRUDViewModel vm, string LoginUser);       
    }
}
