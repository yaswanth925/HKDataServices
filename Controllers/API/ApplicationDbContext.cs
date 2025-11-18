using HKDataServices.Model;
using Microsoft.EntityFrameworkCore;



namespace HKDataServices.Controllers.API
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<UpdateTrackingStatus> UpdateTrackingStatuses { get; set; } = null;
        public DbSet<Users> Users { get; set; } = null;
        public DbSet<OtpRecord> OtpRecords { get; set; }
        public DbSet<PreSalesTarget> PreSalesTarget { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<PreSalesActivity> PreSalesActivity { get; set; }
        public DbSet<PostSalesService> PostSalesService { get; set; }
        public DbSet<PreSalesTargetList> PreSalesTargetList { get; set; }
        public DbSet<Accounts> Accounts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UpdateTrackingStatus>(entity =>
            {
                entity.ToTable("UpdateTrackingStatus");
                entity.HasKey(e => e.TrackingStatusID);

                entity.Property(e => e.TrackingStatusID)
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
                entity.HasKey(e => e.UserID);

                entity.Property(e => e.UserID)
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

                entity.HasKey(e => e.TargetID);

                entity.Property(e => e.TargetID)
                      .IsRequired()
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.EmployeeName)
                      .HasMaxLength(255)
                      .IsUnicode(false);

                entity.Property(e => e.MonthYear)
                      .IsRequired();

                entity.Property(e => e.TargetYear)
                      .IsRequired();

                entity.Property(e => e.PreSalesVisit)
                      .IsRequired();

                entity.Property(e => e.PreSalesActivity)
                      .IsRequired();

                entity.Property(e => e.PostSalesService)
                      .IsRequired();

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

                entity.HasMany(e => e.TargetListItems)
                      .WithOne(e => e.PreSalesTarget)
                      .HasForeignKey(e => e.TargetID)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PreSalesTargetList>(entity =>
            {
                entity.ToTable("PreSalesTargetList");

                entity.HasKey(e => e.ListID);

                entity.Property(e => e.ListID)
                      .IsRequired()
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.TargetID)
                      .IsRequired();

                entity.Property(e => e.EmployeeName)
                      .HasMaxLength(255)
                      .IsUnicode(false);

                entity.Property(e => e.MonthYear)
                      .IsRequired();

                entity.Property(e => e.TargetYear)
                      .IsRequired();

                entity.Property(e => e.PreSalesVisit)
                      .IsRequired();

                entity.Property(e => e.PreSalesActivity)
                      .IsRequired();

                entity.Property(e => e.PostSalesService)
                      .IsRequired();

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

          
                entity.HasOne(e => e.PreSalesTarget)
                      .WithMany(e => e.TargetListItems)
                      .HasForeignKey(e => e.TargetID)
                      .OnDelete(DeleteBehavior.Cascade);
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
                entity.Property(e => e.ImageFile)
                      .HasColumnType("varbinary(max)");
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
            modelBuilder.Entity<PreSalesActivity>(entity =>
            {
                entity.ToTable("PreSalesActivity");
                entity.HasKey(e => e.ActivityID);

                entity.Property(e => e.ActivityID)
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
                entity.HasKey(e => e.ServiceID);

                entity.Property(e => e.ServiceID)
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

            modelBuilder.Entity<Accounts>(entity =>
            {
                entity.ToTable("Accounts");
                entity.HasKey(e => e.AccountID);

                entity.Property(e => e.AccountID)
                      .IsRequired()
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.CustomerName)
                      .HasMaxLength(255)
                      .IsUnicode(false);

                entity.Property(e => e.MobileNumber).HasMaxLength(15);
                entity.Property(e => e.GSTNumber).HasMaxLength(50);
                entity.Property(e => e.Pincode).HasMaxLength(10);
                entity.Property(e => e.City).HasMaxLength(20);
                entity.Property(e => e.State).HasMaxLength(20);
                entity.Property(e => e.Sales)
                       .IsRequired();
                entity.Property(e => e.FileData)
                      .HasColumnType("varbinary(max)");
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
        }
    }
}





