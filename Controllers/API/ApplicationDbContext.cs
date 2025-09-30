using HKDataServices.Model;
using HKDataServices.Repository;
using Microsoft.EntityFrameworkCore;

namespace HKDataServices.Controllers.API
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<UpdateTrackingStatus> UpdateTrackingStatuses { get; set; } = null;
        public DbSet<Users> Users { get; set; } = null;
       
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
                      .HasMaxLength(225)
                      .IsUnicode(false);

                entity.Property(e => e.StatusType)
                      .HasColumnType("char(50)")
                      .HasMaxLength(50)
                      .IsFixedLength()
                      .IsUnicode(false);

                entity.Property(e => e.FileName)
                      .HasMaxLength(255)
                      .IsUnicode(false);

                entity.Property(e => e.FileData)
                      .HasColumnType("varbinary(max)");

                entity.Property(e => e.Remarks)
                      .HasMaxLength(255)
                      .IsUnicode(false);

                entity.Property(e => e.Createdby)
                      .HasMaxLength(255)
                      .IsUnicode(false);

                entity.Property(e => e.Created)
                      .HasColumnType("datetime");

                entity.Property(e => e.Modifiedby)
                      .HasMaxLength(255)
                      .IsUnicode(false);

                entity.Property(e => e.Modified)
                      .HasColumnType("datetime");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(e => e.ID);

                entity.Property(e => e.ID)
                      .ValueGeneratedOnAdd()
                      .HasDefaultValueSql("newid()");

                entity.Property(e => e.FirstName)
                      .HasMaxLength(50)
                      .IsUnicode(false);

                entity.Property(e => e.LastName)
                      .HasMaxLength(50)
                      .IsUnicode(false);

                entity.Property(e => e.MobileNumber)
                      .HasColumnType("varchar(15)")
                      .HasMaxLength(10)
                      .IsUnicode(false);

                entity.Property(e => e.EmailID)
                      .HasColumnType("varchar(255)")
                      .HasMaxLength(255)
                      .IsUnicode(false);

                entity.Property(e => e.Password)
                      .HasColumnType("varchar(255)")
                      .HasMaxLength(255)
                      .IsUnicode(false);

                entity.Property(e => e.Createdby)
                      .HasMaxLength(255)
                      .IsUnicode(false);

                entity.Property(e => e.Created)
                      .HasColumnType("datetime");

                entity.Property(e => e.Modifiedby)
                      .HasMaxLength(255)
                      .IsUnicode(false);

                entity.Property(e => e.Modified)
                      .HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                      .HasColumnType("bit")
                      .IsRequired();
            });
          
        }
    }
}
