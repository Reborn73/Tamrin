using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tamrin.Entities.Common;

namespace Tamrin.Entities.Course
{
    public class Category : IBaseEntity
    {
        #region Constructor

        public Category()
        {

        }

        #endregion

        #region Properties

        public long Id { get; set; }
        public long? ParentId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
        public bool IsDeleted { get; set; }

        #endregion

        #region Relations

        public Category ParentCategory { get; set; }
        public ICollection<Category> ChildCategories { get; set; }
        public ICollection<CourseSelectedCategory> Courses { get; set; }

        #endregion
    }

    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            #region Properties

            builder.HasKey(c => c.Id);
            builder.Property(c => c.ParentId).IsRequired(false);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(50).IsUnicode(false);
            builder.Property(c => c.Title).IsRequired().HasMaxLength(50).IsUnicode();
            builder.Property(c => c.CreateDateTime).IsRequired();
            builder.Property(c => c.LastUpdateDateTime).IsRequired(false);
            builder.Property(c => c.IsDeleted).IsRequired();

            #endregion

            #region Relations

            builder.HasOne(c => c.ParentCategory).WithMany(c => c.ChildCategories).HasForeignKey(c => c.ParentId);
            builder.HasMany(c => c.ChildCategories).WithOne(c => c.ParentCategory).HasForeignKey(c => c.ParentId);
            builder.HasMany(c => c.Courses).WithOne(csc => csc.Category).HasForeignKey(csc => csc.CategoryId);

            #endregion
        }
    }
}
