using Microsoft.EntityFrameworkCore;
using ComplaintMngSys.Models;

namespace ComplaintMngSys.Data
{
    public class ApplicationDbContext : AuditableIdentityContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }

        public DbSet<SMTPEmailSetting> SMTPEmailSetting { get; set; }
        public DbSet<SendGridSetting> SendGridSetting { get; set; }
        public DbSet<DefaultIdentityOptions> DefaultIdentityOptions { get; set; }
        public DbSet<LoginHistory> LoginHistory { get; set; }


        //Business
        public DbSet<Complaint> Complaint { get; set; }
        public DbSet<ComplaintStatus> ComplaintStatus { get; set; }
        public DbSet<ComplaintCategory> ComplaintCategory { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<AttachmentFile> AttachmentFile { get; set; }
        public DbSet<Priority> Priority { get; set; }
        public DbSet<CompanyInfo> CompanyInfo { get; set; }
        public DbSet<UserInfoFromBrowser> UserInfoFromBrowser { get; set; }
    }
}
