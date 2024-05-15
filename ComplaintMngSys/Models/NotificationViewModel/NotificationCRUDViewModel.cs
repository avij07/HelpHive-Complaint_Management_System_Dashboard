using System;
using System.ComponentModel.DataAnnotations;

namespace ComplaintMngSys.Models.NotificationViewModel
{
    public class NotificationCRUDViewModel : EntityBase
    {
        [Display(Name = "SL")]
        [Required]
        public Int64 Id { get; set; }
        public Int64 ComplaintId { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public string NotificationFor { get; set; }

        public static implicit operator NotificationCRUDViewModel(Notification _Notification)
        {
            return new NotificationCRUDViewModel
            {
                Id = _Notification.Id,
                ComplaintId = _Notification.ComplaintId,
                NotificationFor = _Notification.NotificationFor,
                Message = _Notification.Message,
                IsRead = _Notification.IsRead,
                CreatedDate = _Notification.CreatedDate,
                ModifiedDate = _Notification.ModifiedDate,
                CreatedBy = _Notification.CreatedBy,
                ModifiedBy = _Notification.ModifiedBy,
                Cancelled = _Notification.Cancelled,

            };
        }

        public static implicit operator Notification(NotificationCRUDViewModel vm)
        {
            return new Notification
            {
                Id = vm.Id,
                ComplaintId = vm.ComplaintId,
                NotificationFor = vm.NotificationFor,
                Message = vm.Message,
                IsRead = vm.IsRead,
                CreatedDate = vm.CreatedDate,
                ModifiedDate = vm.ModifiedDate,
                CreatedBy = vm.CreatedBy,
                ModifiedBy = vm.ModifiedBy,
                Cancelled = vm.Cancelled,

            };
        }

        public static implicit operator NotificationCRUDViewModel(Complaint _Complaint)
        {
            return new NotificationCRUDViewModel
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

        //public static implicit operator Complaint(NotificationCRUDViewModel vm)
        //{
        //    return new Complaint
        //    {
        //        Id = vm.ComplaintId,
        //        Complainant = vm.NotificationFor,

        //        CreatedDate = vm.CreatedDate,
        //        ModifiedDate = vm.ModifiedDate,
        //        CreatedBy = vm.CreatedBy,
        //        ModifiedBy = vm.ModifiedBy,
        //        Cancelled = vm.Cancelled,

        //    };
        //}

    }
}