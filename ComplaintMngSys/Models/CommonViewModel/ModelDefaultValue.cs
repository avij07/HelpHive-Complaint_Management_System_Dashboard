using ComplaintMngSys.Models;
using System;

namespace ComplaintMngSys.Helpers
{
    public class ModelDefaultValue
    {
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public bool Cancelled { get; set; }

        public static implicit operator ModelDefaultValue(EntityBase _EntityBase)
        {
            return new ModelDefaultValue
            {
                CreatedDate = _EntityBase.CreatedDate,
                ModifiedDate = _EntityBase.ModifiedDate,
                CreatedBy = _EntityBase.CreatedBy,
                ModifiedBy = _EntityBase.ModifiedBy,
                Cancelled = _EntityBase.Cancelled,

            };
        }

        public static implicit operator EntityBase(ModelDefaultValue vm)
        {
            return new EntityBase
            {
                CreatedDate = vm.CreatedDate,
                ModifiedDate = vm.ModifiedDate,
                CreatedBy = vm.CreatedBy,
                ModifiedBy = vm.ModifiedBy,
                Cancelled = vm.Cancelled,

            };
        }

        public static class SetModelDefaultValue
        {
            public static ModelDefaultValue ABC()
            {

                return null;
            }
        }
    }
}
