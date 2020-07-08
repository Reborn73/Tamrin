using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using Tamrin.Entities.Common;

namespace Tamrin.Entities.Course
{
    public class CourseComment : IBaseEntity
    {
        #region Constructor

        public CourseComment()
        {

        }

        #endregion

        #region Properties

        public long Id { get; set; }
        public long UserId { get; set; }
        public long CourseId { get; set; }
        public long? ParentId { get; set; }
        public string Text { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
        public bool IsDeleted { get; set; }

        #endregion

        #region Relations

        public User.User User { get; set; }
        public Course Course { get; set; }
        public CourseComment ParentComment { get; set; }
        public ICollection<CourseComment> ChildComments { get; set; }

        #endregion
    }

    public class CourseCommentConfiguration : IEntityTypeConfiguration<CourseComment>
    {
        public void Configure(EntityTypeBuilder<CourseComment> builder)
        {
            #region Properties

            builder.HasKey(c => c.Id);
            builder.Property(c => c.UserId).IsRequired();
            builder.Property(c => c.CourseId).IsRequired();
            builder.Property(c => c.ParentId).IsRequired(false);
            builder.Property(c => c.Text).IsRequired().HasMaxLength(400).IsUnicode();
            builder.Property(c => c.IsConfirmed).IsRequired();
            builder.Property(c => c.CreateDateTime).IsRequired();
            builder.Property(c => c.LastUpdateDateTime).IsRequired(false);
            builder.Property(c => c.IsDeleted).IsRequired();

            #endregion

            #region Relations

            builder.HasOne(c => c.User).WithMany(u => u.CourseComments).HasForeignKey(c => c.UserId);
            builder.HasOne(c => c.Course).WithMany(c => c.CourseComments).HasForeignKey(c => c.CourseId);
            builder.HasOne(c => c.ParentComment).WithMany(c => c.ChildComments).HasForeignKey(c => c.ParentId);
            builder.HasMany(c => c.ChildComments).WithOne(c => c.ParentComment).HasForeignKey(c => c.ParentId);

            #endregion
        }
    }
}
