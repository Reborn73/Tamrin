using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Tamrin.Entities.Common;

namespace Tamrin.Entities.Course
{
    public class Episode : AuditEntity
    {
        #region Constructor

        public Episode()
        {

        }

        #endregion

        #region Properties

        public long CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public PriceType EpisodePriceType { get; set; }
        public TimeSpan Time { get; set; }

        #endregion

        #region Relations

        public Course Course { get; set; }

        #endregion

    }

    public class EpisodeConfiguration : IEntityTypeConfiguration<Episode>
    {
        public void Configure(EntityTypeBuilder<Episode> builder)
        {
            #region Properties

            builder.HasKey(e => e.Id);
            builder.Property(r => r.CourseId).IsRequired();
            builder.Property(r => r.Title).IsRequired().HasMaxLength(45).IsUnicode();
            builder.Property(r => r.Description).IsRequired(false).HasMaxLength(150).IsUnicode();
            builder.Property(r => r.FileName).IsRequired().HasMaxLength(50).IsUnicode(false);
            builder.Property(r => r.EpisodePriceType).IsRequired();
            builder.Property(r => r.Time).IsRequired();
            builder.Property(r => r.CreateDateTime).IsRequired();
            builder.Property(r => r.LastUpdateDateTime).IsRequired(false);
            builder.Property(r => r.IsDeleted).IsRequired();

            #endregion

            #region Relations

            builder.HasOne(e => e.Course).WithMany(c => c.Episodes).HasForeignKey(e => e.CourseId);

            #endregion
        }
    }
}
