using ComplaintMngSys.Helpers;
using ComplaintMngSys.Models;
using ComplaintMngSys.Models.UserAccountViewModel;

namespace ComplaintMngSys.Data
{
    public class SeedData
    {
        public IEnumerable<ComplaintCategory> GetComplaintCategoryList()
        {
            return new List<ComplaintCategory>
            {
                new ComplaintCategory { Name = "Software", Description = "Software"},
                new ComplaintCategory { Name = "Hardware", Description = "Hardware"},
                new ComplaintCategory { Name = "Network", Description = "Network"},
                new ComplaintCategory { Name = "Finance", Description = "Finance"},
                new ComplaintCategory { Name = "Commercial", Description = "Commercial"},
         
            };
        }

        public IEnumerable<ComplaintStatus> GetComplaintStatusList()
        {
            return new List<ComplaintStatus>
            {
                new ComplaintStatus { Name = "New", Description = "New Status"},
                new ComplaintStatus { Name = "Submited", Description = "Submited Status"},
                new ComplaintStatus { Name = "In Progress", Description = "In Progress Status"},

                new ComplaintStatus { Name = "Pending", Description = "Pending Status"},
                new ComplaintStatus { Name = "Resolved", Description = "Resolved Status"},
                new ComplaintStatus { Name = "Rejected", Description = "Rejected Status"},
                new ComplaintStatus { Name = "Closed", Description = "Closed Status"},
                new ComplaintStatus { Name = "ToDo", Description = "ToDo Status"}
            };
        }

        public IEnumerable<Priority> GetPriorityList()
        {
            return new List<Priority>
            {
                new Priority { Name = "High", Description = "High Priority"},
                new Priority { Name = "Low", Description = "Low Priority"},
                new Priority { Name = "Medium", Description = "Medium Priority"},

                new Priority { Name = "Critical", Description = "Critical Priority"},
                new Priority { Name = "Moderate", Description = "Moderate Priority"},
                new Priority { Name = "Major", Description = "Major Priority"},
                new Priority { Name = "Others", Description = "Others Priority"},
            };
        }
        public IEnumerable<Complaint> GetComplaintList()
        {
            return new List<Complaint>
            {
                
            };
        }
        public IEnumerable<UserProfileCRUDViewModel> GetUserProfileList()
        {
            return new List<UserProfileCRUDViewModel>
            {
                new UserProfileCRUDViewModel { FirstName = "TestUser1", LastName = "#01", Email = "user1@gmail.com", PasswordHash = "123", ConfirmPassword = "123", PhoneNumber= StaticData.RandomDigits(11), ProfilePicturePath = "/images/DefaultUser/user.png", Address = "Jamshedpur", Country = "India" },
                new UserProfileCRUDViewModel { FirstName = "TestUser2", LastName = "#02", Email = "user2@gmail.com", PasswordHash = "123", ConfirmPassword = "123", PhoneNumber= StaticData.RandomDigits(11), ProfilePicturePath = "/images/DefaultUser/user.png", Address = "Hyderabad", Country = "India"  },
                new UserProfileCRUDViewModel { FirstName = "TestUser3", LastName = "#03", Email = "user3@gmail.com", PasswordHash = "123", ConfirmPassword = "123", PhoneNumber= StaticData.RandomDigits(11), ProfilePicturePath = "/images/DefaultUser/user.png", Address = "Mangalore", Country = "India"  },
                new UserProfileCRUDViewModel { FirstName = "TestUser4", LastName = "#04", Email = "user4@gmail.com", PasswordHash = "123", ConfirmPassword = "123", PhoneNumber= StaticData.RandomDigits(11), ProfilePicturePath = "/images/DefaultUser/user.png", Address = "Manipal", Country = "India"  },
                new UserProfileCRUDViewModel { FirstName = "TestUser5", LastName = "#05", Email = "user5@gmail.com", PasswordHash = "123", ConfirmPassword = "123", PhoneNumber= StaticData.RandomDigits(11), ProfilePicturePath = "/images/DefaultUser/user.png", Address = "Bangalore", Country = "India"  },

                new UserProfileCRUDViewModel { FirstName = "Software_Admin", LastName = "#01", Email = "admin_software1@gmail.com", PasswordHash = "123", ConfirmPassword = "123", PhoneNumber= StaticData.RandomDigits(11), ProfilePicturePath = "/images/DefaultUser/Admin.png", Address = "Jamshedpur", Country = "India" },
                new UserProfileCRUDViewModel { FirstName = "Hardware_Admin", LastName = "#01", Email = "admin_hardware@gmail.com", PasswordHash = "123", ConfirmPassword = "123", PhoneNumber= StaticData.RandomDigits(11), ProfilePicturePath = "/images/DefaultUser/Admin.png", Address = "Mumbai", Country = "India" },
                new UserProfileCRUDViewModel { FirstName = "Network_Admin", LastName = "#01", Email = "admin_network@gmail.com", PasswordHash = "123", ConfirmPassword = "123", PhoneNumber= StaticData.RandomDigits(11), ProfilePicturePath = "/images/DefaultUser/Admin.png", Address = "Pune", Country = "India" },
                new UserProfileCRUDViewModel { FirstName = "Finance_Admin", LastName = "#01", Email = "admin_finance@gmail.com", PasswordHash = "123", ConfirmPassword = "123", PhoneNumber= StaticData.RandomDigits(11), ProfilePicturePath = "/images/DefaultUser/Admin.png", Address = "Agra", Country = "India" },
                new UserProfileCRUDViewModel { FirstName = "Commercial_Admin", LastName = "#01", Email = "admin_commerical@gmail.com", PasswordHash = "123", ConfirmPassword = "123", PhoneNumber= StaticData.RandomDigits(11), ProfilePicturePath = "/images/DefaultUser/Admin.png", Address = "Hyderabad", Country = "India" },
            };
        }
        public CompanyInfo GetCompanyInfo()
        {
            return new CompanyInfo
            {
                Name = "Tata Technologies Ltd",
                ApplicationTitle = "Support Ticket System",
                CompanyLogoImagePath = "/upload/blank_logo.png",
                Currency = "Rs.",
                Address = "Pune, India",
                City = "Pune",
                Country = "India",
                Phone = "132546789",
                Email = "XYZ@GMAIL.COM",
                Website = "www.tata.com",
            };
        }
    }
}
