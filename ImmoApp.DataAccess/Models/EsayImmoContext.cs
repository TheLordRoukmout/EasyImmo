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

    public virtual DbSet<EstateDocument> EstateDocuments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=EsayImmo;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EstateDocument>(entity =>
        {
            entity.HasKey(e => e.IdDocument).HasName("PK__Estate_D__D5F2A16F0E21B6B4");

            entity.ToTable("Estate_Documents");

            entity.Property(e => e.IdDocument).HasColumnName("id_document");
            entity.Property(e => e.DocumentName)
                .HasMaxLength(255)
                .HasColumnName("document_name");
            entity.Property(e => e.DocumentPath)
                .HasMaxLength(500)
                .HasColumnName("document_path");
            entity.Property(e => e.IdEstate).HasColumnName("id_estate");
            entity.Property(e => e.UploadDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("upload_date");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
