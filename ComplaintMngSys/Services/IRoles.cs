using ComplaintMngSys.Models.CommonViewModel;
using System.Threading.Tasks;

namespace ComplaintMngSys.Services
{
    public interface IRoles
    {
        Task GenerateRolesFromPagesAsync();

        Task AddToRoles(string applicationUserId);
        Task<MainMenuViewModel> RolebaseMenuLoad(string applicationUserId);
    }
}
