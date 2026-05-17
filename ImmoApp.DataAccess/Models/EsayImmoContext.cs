using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ImmoApp.DataAccess.Models;

public partial class EsayImmoContext : DbContext
{
    public EsayImmoContext()
    {
    }

    public EsayImmoContext(DbContextOptions<EsayImmoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EstateImage> EstateImages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=EsayImmo;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EstateImage>(entity =>
        {
            entity.HasKey(e => e.IdImage).HasName("PK__Estate_I__C28C621C82125642");

            entity.ToTable("Estate_Images");

            entity.Property(e => e.IdImage).HasColumnName("id_image");
            entity.Property(e => e.IdEstate).HasColumnName("id_estate");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(500)
                .HasColumnName("image_path");
            entity.Property(e => e.IsMain)
                .HasDefaultValue(false)
                .HasColumnName("is_main");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
