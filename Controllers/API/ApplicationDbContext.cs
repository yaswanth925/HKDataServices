using HKDataServices.Model;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace HKDataServices.Controllers.API
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<UpdateTrackingStatus> UpdateTrackingStatuses { get; set; } = null;
        public DbSet<Users> Users { get; set; } = null;
        public DbSet<OtpRecord> OtpRecords { get; set; }
        public DbSet<PreSalesTarget> PreSalesTargets { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<PreSalesActivity> PreSalesActivity { get; set; }
        public DbSet<PostSalesService> PostSalesService { get; set; }


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

                entity.Property(e => e.CreatedBy)
                      .HasMaxLength(255)
                      .IsUnicode(false);

                entity.Property(e => e.Created)
                      .HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy)
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

                entity.Property(e => e.CreatedBy)
                      .HasMaxLength(255)
                      .IsUnicode(false);

                entity.Property(e => e.Created)
                      .HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy)
                      .HasMaxLength(255)
                      .IsUnicode(false);

                entity.Property(e => e.Modified)
                      .HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                      .HasColumnType("bit")
                      .IsRequired();
            });
            modelBuilder.Entity<PreSalesTarget>(entity =>
            {
                entity.ToTable("PreSalesTarget");
                entity.HasKey(e => e.EmployeeName);
                entity.Property(e => e.EmployeeName)
                      .HasMaxLength(255)
                      .IsUnicode(false);
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.ToTable("Customers");
                entity.HasKey(e => e.CustomerID);

                entity.Property(e => e.CustomerID)
                      .IsRequired()
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.CustomerName)
                      .HasMaxLength(255)
                      .IsUnicode(false);

                entity.Property(e => e.MobileNumber).HasMaxLength(15);
                entity.Property(e => e.EmailId).HasMaxLength(255);
                entity.Property(e => e.GSTNumber).HasMaxLength(50);
                entity.Property(e => e.Address).HasMaxLength(255);
                entity.Property(e => e.Pincode).HasMaxLength(10);
                entity.Property(e => e.City).HasMaxLength(20);
                entity.Property(e => e.State).HasMaxLength(20);
                entity.Property(e => e.Description).HasMaxLength(255);
            });
            modelBuilder.Entity<PreSalesActivity>(entity =>
            {
                entity.ToTable("PreSalesActivity");
                entity.HasKey(e => e.PreSalesActivityID);

                entity.Property(e => e.PreSalesActivityID)
                      .IsRequired()
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.CustomerID)
                      .IsRequired()
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.ActivityType).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.FileData)
                      .HasColumnType("varbinary(max)");

                entity.Property(e => e.PoValue).HasMaxLength(50);

                entity.Property(e => e.ImageFile)
                      .HasColumnType("varbinary(max)");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.Created)
                      .HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(255);

                entity.Property(e => e.Modified)
                      .HasColumnType("datetime");
            });

            modelBuilder.Entity<PostSalesService>(entity =>
            {
                entity.ToTable("PostSalesService");
                entity.HasKey(e => e.PostSalesServiceID);

                entity.Property(e => e.PostSalesServiceID)
                      .IsRequired()
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.CustomerID)
                      .IsRequired()
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.ImageFile)
                      .HasColumnType("varbinary(max)");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);
                entity.Property(e => e.Created)

                      .HasColumnType("datetime");
                entity.Property(e => e.ModifiedBy).HasMaxLength(255);

                entity.Property(e => e.Modified)
                      .HasColumnType("datetime");
            });
    }   }
}


        
    

