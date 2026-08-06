using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskSystem.Models;

namespace TaskSystem.Data.Map
{
    public class UserMap : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(255);
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.UpdatedDate).IsRequired(false);

            /*builder.HasMany(u => u.Tasks)
           .WithOne(t => t.User)
           .HasForeignKey(t => t.UserId)
           .IsRequired(false);*/
        }
    }
}
