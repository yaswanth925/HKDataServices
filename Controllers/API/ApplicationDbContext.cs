using HKDataServices.Model;
using Microsoft.EntityFrameworkCore;

namespace HKDataServices.Controllers.API
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<UpdateTrackingStatus> UpdateTrackingStatuses => Set<UpdateTrackingStatus>();
        // If you need other tables later:
        // public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UpdateTrackingStatus>(entity =>
            {
                entity.ToTable("UpdateTrackingStatus");
                entity.HasKey(e => e.ID);

                entity.Property(e => e.ID)
                      .ValueGeneratedOnAdd()
                      .HasDefaultValueSql("newid()");

                entity.Property(e => e.AWBNumber)
                      .HasMaxLength(225).IsUnicode(false);

                entity.Property(e => e.StatusType)
                      .HasColumnType("char(50)")
                      .HasMaxLength(50)
                      .IsFixedLength()
                      .IsUnicode(false);

                entity.Property(e => e.FileName)
                      .HasMaxLength(225).IsUnicode(false);

                entity.Property(e => e.FileData)
                      .HasColumnType("varbinary(max)");

                entity.Property(e => e.Remarks)
                      .HasMaxLength(225).IsUnicode(false);

                entity.Property(e => e.Createdby)
                      .HasMaxLength(225).IsUnicode(false);

                entity.Property(e => e.Created)
                      .HasColumnType("datetime");

                entity.Property(e => e.Modifiedby)
                      .HasMaxLength(225).IsUnicode(false);

                entity.Property(e => e.Modified)
                      .HasColumnType("datetime");
            });
        }
    }
}