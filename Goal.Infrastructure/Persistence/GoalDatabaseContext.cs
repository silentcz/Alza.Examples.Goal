using System;
using System.Collections.Generic;
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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=GoalDatabase;User Id=sa;Password=GoalPassword_123#;TrustServerCertificate=True");

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

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
