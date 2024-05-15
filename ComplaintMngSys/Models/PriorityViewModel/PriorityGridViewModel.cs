using System;

namespace ComplaintMngSys.Models.PriorityViewModel
{
    public class PriorityGridViewModel : EntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedDateDisplay { get; set; }
        public string ModifiedDateDisplay { get; set; }
    }
}
