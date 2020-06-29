using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using Tamrin.Entities.Common;
using Tamrin.Entities.Course;

namespace Tamrin.Entities.User
{
    public class User : AuditEntity
    {
        #region Constructor

        public User()
        {

        }

        #endregion

        #region Properties

        public long RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderType GenderType { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsActive { get; set; }
        public string AvatarName { get; set; }
        public string ActiveCode { get; set; }

        #endregion

        #region Relations

        public Role Role { get; set; }
        public ICollection<Course.Course> Courses { get; set; }
        public ICollection<CourseScore> CourseScores { get; set; }
        public ICollection<CourseComment> CourseComments { get; set; }


        #endregion
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            #region Properties

            builder.HasKey(u => u.Id);
            builder.Property(u => u.RoleId).IsRequired();
            builder.Property(u => u.FirstName).IsRequired(false).HasMaxLength(30).IsUnicode();
            builder.Property(u => u.LastName).IsRequired(false).HasMaxLength(40).IsUnicode();
            builder.Property(u => u.GenderType).IsRequired();
            builder.Property(u => u.UserName).IsRequired().HasMaxLength(50).IsUnicode(false);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(120).IsUnicode(false);
            builder.Property(u => u.ActiveCode).IsRequired().HasMaxLength(50).IsUnicode(false);
            builder.Property(u => u.AvatarName).IsRequired().HasMaxLength(50).IsUnicode(false);
            builder.Property(u => u.PasswordHash).IsRequired().HasMaxLength(200).IsUnicode(false);
            builder.Property(u => u.IsActive).IsRequired();
            builder.Property(u => u.IsDeleted).IsRequired();
            builder.Property(u => u.CreateDateTime).IsRequired();
            builder.Property(u => u.LastUpdateDateTime).IsRequired(false);

            #endregion

            #region Relations

            builder.HasOne(u => u.Role).WithMany(r => r.Users).HasForeignKey(u => u.RoleId);
            builder.HasMany(u => u.Courses).WithOne(c => c.Teacher).HasForeignKey(c => c.TeacherId);
            builder.HasMany(u => u.CourseScores).WithOne(s => s.User).HasForeignKey(s => s.UserId);
            builder.HasMany(u => u.CourseComments).WithOne(c=>c.User).HasForeignKey(c=>c.UserId);

            #endregion
        }
    }

    public enum GenderType
    {
        [Display(Name = "آقا/خانم")]
        NotSet,

        [Display(Name = "آقا")]
        Male,

        [Display(Name = "خانم")]
        Female
    }
}
