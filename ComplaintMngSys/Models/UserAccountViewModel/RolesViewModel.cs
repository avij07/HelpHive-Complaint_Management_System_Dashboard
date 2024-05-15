using System.ComponentModel.DataAnnotations;

namespace ComplaintMngSys.Models.UserAccountViewModel
{
    public class RolesViewModel
    {
        [Display(Name = "SL")]
        public int Id { get; set; }
        [Display(Name = "Role Name")]
        public string Name { get; set; }
        public string RoleID { get; set; }
    }
}
