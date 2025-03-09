using Goal.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Goal.Infrastructure.Persistence;

public partial class GoalDatabaseContext : DbContext
{
    public GoalDatabaseContext()
    {
    }

    public GoalDatabaseContext(DbContextOptions<GoalDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC07E35F98B8");

            entity.ToTable("Product", "GoalSchema");

            entity.HasIndex(e => e.Name, "IX_Product_Name");

            entity.HasIndex(e => new { e.Name, e.Price }, "IX_Product_Name_Price");

            entity.HasIndex(e => e.Price, "IX_Product_Price");

            entity.Property(e => e.ImgUri).HasMaxLength(2083);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    private void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Description)
                .IsRequired(false) // Oznámení, že NULL je povolený
                .HasColumnType("nvarchar(max)"); // Nastavení typu sloupce, pokud je třeba
        });
    }
}
