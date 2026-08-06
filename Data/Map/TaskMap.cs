using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskSystem.Models;

namespace TaskSystem.Data.Map
{
    public class TaskMap : IEntityTypeConfiguration<TaskModel>
    {
        public void Configure(EntityTypeBuilder<TaskModel> builder)
        {
            builder.ToTable("Tasks");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(1000);
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.UpdatedDate).IsRequired(false);

            //builder.HasOne(x => x.User).WithMany(u => u.Tasks).HasForeignKey(x => x.UserId);
        }
    }
}
