using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Tamrin.Entities.Common;

namespace Tamrin.Entities.Course
{
    public class CourseVisit : IBaseEntity
    {
        #region Constructor

        public CourseVisit()
        {

        }

        #endregion

        #region Properties

        public long Id { get; set; }
        public long CourseId { get; set; }
        public string Ip { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
        public bool IsDeleted { get; set; }

        #endregion

        #region Relations

        public Course Course { get; set; }

        #endregion
    }

    public class CourseVisitConfiguration : IEntityTypeConfiguration<CourseVisit>
    {
        public void Configure(EntityTypeBuilder<CourseVisit> builder)
        {
            #region Properties

            builder.HasKey(v => v.Id);
            builder.Property(v => v.CourseId).IsRequired();
            builder.Property(v => v.Ip).IsRequired().HasMaxLength(15).IsUnicode(false);

            #endregion

            #region Relations

            builder.HasOne(v => v.Course).WithMany(c => c.CourseVisits).HasForeignKey(v => v.CourseId);

            #endregion
        }
    }
}
