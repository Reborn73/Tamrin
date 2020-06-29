using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tamrin.Entities.Common;

namespace Tamrin.Entities.Course
{
    public class CourseSelectedCategory : AuditEntity
    {
        #region Constructor

        public CourseSelectedCategory()
        {

        }

        #endregion

        #region Properties

        public long CourseId { get; set; }
        public long CategoryId { get; set; }

        #endregion

        #region Relations

        public Course Course { get; set; }
        public Category Category { get; set; }

        #endregion
    }

    public class CourseSelectedCategoryConfiguration : IEntityTypeConfiguration<CourseSelectedCategory>
    {
        public void Configure(EntityTypeBuilder<CourseSelectedCategory> builder)
        {
            #region Properties

            builder.HasKey(csc => csc.Id);
            builder.Property(csc => csc.CourseId).IsRequired();
            builder.Property(csc => csc.CategoryId).IsRequired();
            builder.Property(csc => csc.CreateDateTime).IsRequired();
            builder.Property(csc => csc.LastUpdateDateTime).IsRequired(false);
            builder.Property(csc => csc.IsDeleted).IsRequired();

            #endregion

            #region Relations

            builder.HasOne(csc => csc.Course).WithMany(c => c.Categories).HasForeignKey(csc => csc.CourseId);
            builder.HasOne(csc => csc.Category).WithMany(c => c.Courses).HasForeignKey(csc => csc.CategoryId);

            #endregion
        }
    }
}
