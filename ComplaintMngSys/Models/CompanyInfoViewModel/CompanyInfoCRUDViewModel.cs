using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace ComplaintMngSys.Models.CompanyInfoViewModel
{
    public class CompanyInfoCRUDViewModel : EntityBase
    {
        [Display(Name = "SL")]
        public Int64 Id { get; set; }
        [Display(Name = "Company Name")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Company Logo")]
        public string ApplicationTitle { get; set; }
        public string Currency { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        public IFormFile CompanyLogo { get; set; }
        public string CompanyLogoImagePath { get; set; } = "/upload/blank_logo.png";

        public static implicit operator CompanyInfoCRUDViewModel(CompanyInfo _CompanyInfo)
        {
            return new CompanyInfoCRUDViewModel
            {
                Id = _CompanyInfo.Id,
                Name = _CompanyInfo.Name,
                ApplicationTitle = _CompanyInfo.ApplicationTitle,
                Currency = _CompanyInfo.Currency,
                Address = _CompanyInfo.Address,
                City = _CompanyInfo.City,
                Country = _CompanyInfo.Country,
                Phone = _CompanyInfo.Phone,
                Email = _CompanyInfo.Email,
                Fax = _CompanyInfo.Fax,
                Website = _CompanyInfo.Website,
                CompanyLogoImagePath = _CompanyInfo.CompanyLogoImagePath,

                CreatedDate = _CompanyInfo.CreatedDate,
                ModifiedDate = _CompanyInfo.ModifiedDate,
                CreatedBy = _CompanyInfo.CreatedBy,
                ModifiedBy = _CompanyInfo.ModifiedBy,
                Cancelled = _CompanyInfo.Cancelled
            };
        }

        public static implicit operator CompanyInfo(CompanyInfoCRUDViewModel vm)
        {
            return new CompanyInfo
            {
                Id = vm.Id,
                Name = vm.Name,
                ApplicationTitle = vm.ApplicationTitle,
                Currency = vm.Currency,
                Address = vm.Address,
                City = vm.City,
                Country = vm.Country,
                Phone = vm.Phone,
                Email = vm.Email,
                Fax = vm.Fax,
                Website = vm.Website,
                CompanyLogoImagePath = vm.CompanyLogoImagePath,

                CreatedDate = vm.CreatedDate,
                ModifiedDate = vm.ModifiedDate,
                CreatedBy = vm.CreatedBy,
                ModifiedBy = vm.ModifiedBy,
                Cancelled = vm.Cancelled
            };
        }
    }
}
