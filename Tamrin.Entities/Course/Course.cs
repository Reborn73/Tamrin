using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using Tamrin.Entities.Common;

namespace Tamrin.Entities.Course
{
    public class Course : IBaseEntity
    {
        #region Constructor

        public Course()
        {

        }

        #endregion

        #region Properties

        public long Id { get; set; }
        public long TeacherId { get; set; }
        public string Title { get; set; }
        public PriceType CoursePriceType { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string ImageName { get; set; }
        public CourseLevel CourseLevel { get; set; }
        public CourseStatus CourseStatus { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
        public bool IsDeleted { get; set; }

        #endregion

        #region Relations

        public User.User Teacher { get; set; }
        public ICollection<Episode> Episodes { get; set; }
        public ICollection<CourseSelectedCategory> Categories { get; set; }
        public ICollection<CourseVisit> CourseVisits { get; set; }
        public ICollection<CourseScore> CourseScores { get; set; }
        public ICollection<CourseComment> CourseComments { get; set; }

        #endregion
    }

    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            #region Properties

            builder.HasKey(c => c.Id);
            builder.Property(c => c.TeacherId).IsRequired();
            builder.Property(c => c.Title).IsRequired().HasMaxLength(75).IsUnicode();
            builder.Property(c => c.CoursePriceType).IsRequired();
            builder.Property(c => c.Description).IsRequired(false).HasMaxLength(200).IsUnicode();
            builder.Property(c => c.Price).IsRequired();
            builder.Property(c => c.ImageName).IsRequired().HasMaxLength(50).IsUnicode(false);
            builder.Property(c => c.CourseLevel).IsRequired();
            builder.Property(c => c.CourseStatus).IsRequired();
            builder.Property(c => c.CreateDateTime).IsRequired();
            builder.Property(c => c.LastUpdateDateTime).IsRequired(false);
            builder.Property(c => c.IsDeleted).IsRequired();

            #endregion

            #region Relations

            builder.HasOne(c => c.Teacher).WithMany(u => u.Courses).HasForeignKey(c => c.TeacherId);
            builder.HasMany(c => c.Episodes).WithOne(e => e.Course).HasForeignKey(e => e.CourseId);
            builder.HasMany(c => c.Categories).WithOne(csc => csc.Course).HasForeignKey(csc => csc.CourseId);
            builder.HasMany(c => c.CourseVisits).WithOne(v => v.Course).HasForeignKey(v => v.CourseId);
            builder.HasMany(c => c.CourseScores).WithOne(s => s.Course).HasForeignKey(s => s.CourseId);
            builder.HasMany(c => c.CourseComments).WithOne(c=>c.Course).HasForeignKey(c=>c.CourseId);

            #endregion
        }
    }

    public enum PriceType
    {
        [Display(Name = "رایگان")]
        Free,

        [Display(Name = "نقدی")]
        Cash
    }

    public enum CourseLevel
    {
        [Display(Name = "مقدماتی")]
        Preliminary,

        [Display(Name = "متوسط")]
        Medium,

        [Display(Name = "پیشرفته")]
        Advanced
    }

    public enum CourseStatus
    {
        [Display(Name = "شروع نشده")]
        NotStarted,

        [Display(Name = "در حال برگزاری")]
        OnPerforming
        ,

        [Display(Name = "به اتمام رسیده")]
        Finished,

        [Display(Name = "کنسل شده")]
        Canceled
    }
}
