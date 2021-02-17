using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ServerRentCar.Models
{
    public partial class rentdbContext : DbContext
    {
        public rentdbContext()
        {
        }

        public rentdbContext(DbContextOptions<rentdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<CarRentRecord> CarRentRecords { get; set; }
        public virtual DbSet<CarsType> CarsTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.ToTable("Branch");

                entity.Property(e => e.BranchAdress)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.BranchName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Cordinate)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasKey(e => e.LicensePlate)
                    .HasName("PK__Cars__026BC15D767A3C12");

                entity.Property(e => e.LicensePlate)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Kilometer)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cars__BranchId__33D4B598");

                entity.HasOne(d => d.CarsTypes)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.CarsTypesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cars__CarsTypesI__34C8D9D1");
            });

            modelBuilder.Entity<CarRentRecord>(entity =>
            {
                entity.HasKey(e => e.RentRecordId)
                    .HasName("PK__CarRentR__CE4AC615C0BCF953");

                entity.ToTable("CarRentRecord");

                entity.Property(e => e.ActualRentEndDate).HasColumnType("date");

                entity.Property(e => e.EndRentDate).HasColumnType("date");

                entity.Property(e => e.LicensePlate)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StartRentDate).HasColumnType("date");

                entity.HasOne(d => d.LicensePlateNavigation)
                    .WithMany(p => p.CarRentRecords)
                    .HasForeignKey(d => d.LicensePlate)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CarRentRe__Licen__398D8EEE");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CarRentRecords)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CarRentRe__UserI__38996AB5");
            });

            modelBuilder.Entity<CarsType>(entity =>
            {
                entity.HasKey(e => e.CarsTypesId)
                    .HasName("PK__CarsType__2E84B1E26B778F0C");

                entity.Property(e => e.DelayCostPerDay)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("delayCostPerDay");

                entity.Property(e => e.Gear)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("gear");

                entity.Property(e => e.Manufacture)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("manufacture");

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("model");

                entity.Property(e => e.PricePerDay)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("pricePerDay");

                entity.Property(e => e.YearRelease)
                    .HasColumnType("date")
                    .HasColumnName("yearRelease");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.TeudZeut, "UQ__Users__208B85B87EFA0873")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__Users__A9D1053483CFB4C3")
                    .IsUnique();

                entity.HasIndex(e => e.UserName, "UQ__Users__C9F28456B9B90B11")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TeudZeut)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserPicture).HasMaxLength(1);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
