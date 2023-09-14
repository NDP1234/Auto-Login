using AutoLogin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AutoLogin.Data
{
    public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        
        public virtual DbSet<ApplicationUser> ApplicationUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       => optionsBuilder.UseMySql("server=localhost;port=3306;database=autologin;user=root;password=1234", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.1.0-mysql"));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("ApplicationUser");

                entity.Property(e => e.GuidforUser).HasColumnName("GUIDForUser");
                entity.Property(e => e.CreationTime)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasColumnType("timestamp");
                entity.Property(e => e.DeletionTime).HasColumnType("timestamp");
               
                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .HasColumnName("first_name");
                entity.Property(e => e.IsActive).HasDefaultValueSql("'1'");
                entity.Property(e => e.IsDelete).HasDefaultValueSql("'0'");
                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .HasColumnName("last_name");
                entity.Property(e => e.ModificationTime)
                    .ValueGeneratedOnAddOrUpdate()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasColumnType("timestamp");
                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .HasColumnName("password");
               
            });

            modelBuilder.Entity<IdentityUserLogin<string>>().HasNoKey();

            modelBuilder.Entity<IdentityUserRole<string>>().HasNoKey();

            modelBuilder.Entity<IdentityUserToken<string>>().HasNoKey();


            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
