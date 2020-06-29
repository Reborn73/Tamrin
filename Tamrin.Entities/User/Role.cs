using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using Tamrin.Entities.Common;

namespace Tamrin.Entities.User
{
    public class Role : BaseEntity
    {
        #region Constructor

        public Role()
        {

        }

        #endregion

        #region Properties

        public string Name { get; set; }
        public string Title { get; set; }
        public bool IsHide { get; set; }

        #endregion

        #region Relations

        public ICollection<User> Users { get; set; }

        #endregion
    }

    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            #region Properties

            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedNever();
            builder.Property(r => r.Name).IsRequired().HasMaxLength(50).IsUnicode(false);
            builder.Property(r => r.Title).IsRequired().HasMaxLength(50).IsUnicode();
            builder.Property(r => r.IsHide).IsRequired();

            #endregion

            #region Relations

            builder.HasMany(r => r.Users).WithOne(u => u.Role).HasForeignKey(u => u.RoleId);

            #endregion
        }
    }
}
