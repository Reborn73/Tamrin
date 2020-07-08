using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tamrin.Entities.Common;

namespace Tamrin.Entities.Course
{
    public class CourseScore : IBaseEntity
    {
        #region Constructor

        public CourseScore()
        {

        }

        #endregion

        #region Properties

        public long Id { get; set; }
        public long UserId { get; set; }
        public long CourseId { get; set; }
        public int ImageAndSoundQuality { get; set; }
        public int MasterIsMasteryOfTheSubject { get; set; }
        public int CourseQuality { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
        public bool IsDeleted { get; set; }

        #endregion

        #region Relations

        public User.User User { get; set; }
        public Course Course { get; set; }

        #endregion
    }

    public class CourseScoreConfiguration : IEntityTypeConfiguration<CourseScore>
    {
        public void Configure(EntityTypeBuilder<CourseScore> builder)
        {
            #region Properties

            builder.HasKey(s => s.Id);
            builder.Property(s => s.UserId).IsRequired();
            builder.Property(s => s.CourseId).IsRequired();
            builder.Property(s => s.ImageAndSoundQuality).IsRequired();
            builder.Property(s => s.MasterIsMasteryOfTheSubject).IsRequired();
            builder.Property(s => s.CourseQuality).IsRequired();
            builder.Property(s => s.CreateDateTime).IsRequired();
            builder.Property(s => s.LastUpdateDateTime).IsRequired(false);
            builder.Property(s => s.IsDeleted).IsRequired();

            #endregion

            #region Relations

            builder.HasOne(s => s.User).WithMany(u => u.CourseScores).HasForeignKey(s => s.UserId);
            builder.HasOne(s => s.Course).WithMany(c=>c.CourseScores).HasForeignKey(s => s.CourseId);

            #endregion
        }
    }
}
