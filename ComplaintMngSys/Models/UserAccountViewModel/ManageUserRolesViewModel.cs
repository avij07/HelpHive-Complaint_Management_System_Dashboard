using System.Collections.Generic;

namespace ComplaintMngSys.Models.UserAccountViewModel
{
    public class ManageUserRolesViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool Selected { get; set; }
    }

    public class UpdateRoleViewModel
    {
        public string ApplicationUserId { get; set; }
        public string CurrentURL { get; set; }
        public List<ManageUserRolesViewModel> listManageUserRolesViewModel { get; set; }
    }
}
