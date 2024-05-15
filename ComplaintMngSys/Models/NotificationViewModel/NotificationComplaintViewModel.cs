using System;
using System.ComponentModel.DataAnnotations;

namespace ComplaintMngSys.Models.NotificationViewModel
{
    public class NotificationComplaintViewModel : EntityBase
    {
        [Display(Name = "SL")]
        [Required]
        public Int64 Id { get; set; }
        public Int64 ComplaintId { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public string NotificationFor { get; set; }

        public static implicit operator NotificationComplaintViewModel(Complaint _Complaint)
        {
            return new NotificationComplaintViewModel
            {
                ComplaintId = _Complaint.Id,
                NotificationFor = _Complaint.Complainant,

                CreatedDate = _Complaint.CreatedDate,
                ModifiedDate = _Complaint.ModifiedDate,
                CreatedBy = _Complaint.CreatedBy,
                ModifiedBy = _Complaint.ModifiedBy,
                Cancelled = _Complaint.Cancelled,

            };
        }

        //public static implicit operator Notification(NotificationComplaintViewModel vm)
        //{
        //    return new Notification
        //    {
        //        Id = vm.Id,
        //        ComplaintId = vm.ComplaintId,
        //        NotificationFor = vm.NotificationFor,
        //        Message = vm.Message,
        //        IsRead = vm.IsRead,
        //        CreatedDate = vm.CreatedDate,
        //        ModifiedDate = vm.ModifiedDate,
        //        CreatedBy = vm.CreatedBy,
        //        ModifiedBy = vm.ModifiedBy,
        //        Cancelled = vm.Cancelled,

        //    };
        //}
    }
}