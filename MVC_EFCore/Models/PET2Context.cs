using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MVC_EFCore.Models
{
    public partial class PET2Context : DbContext
    {
        public PET2Context()// constructor cơ bản
        {
        }
        public PET2Context(DbContextOptions<PET2Context> options) // constructor base
            : base(options)
        {
        }
        // Mỗi DbSet đại diện cho 1 bảng trong DB, nếu không có DbSet
        // hoặc DbSet không public thì không thể trỏ đến các bảng
        // DbSet có thể được gọi trực tiếp thông qua DbContext
        public virtual DbSet<Pet> Pets { get; set; } = null!;
        public virtual DbSet<Sen> Sens { get; set; } = null!;
        // 2 Phương thức config 
        // Onconfiguring => Để cấu hình các kết nối với CSDL
        // OnModelCreating => Để thực hiện cấu hình các bảng/đối tượng
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=SHANGHAIK;Database=PET2;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pet>(entity =>
            {
                entity.ToTable("Pet");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ImgUrl)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ImgURL");

                entity.Property(e => e.Loai).HasMaxLength(100);

                entity.Property(e => e.SenId).HasColumnName("SenID");

                entity.Property(e => e.Ten).HasMaxLength(100);

                entity.HasOne(d => d.Sen)
                    .WithMany(p => p.Pets)
                    .HasForeignKey(d => d.SenId)
                    .HasConstraintName("FK_SEN_PET");
            });

            modelBuilder.Entity<Sen>(entity =>
            {
                entity.ToTable("Sen");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DiaChi).HasMaxLength(100);

                entity.Property(e => e.Sdt)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SDT")
                    .IsFixedLength();

                entity.Property(e => e.Ten).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
