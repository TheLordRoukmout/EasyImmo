using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ImmoApp.DataAccess.Models;

public partial class ImmoDbContext : DbContext
{
    public ImmoDbContext()
    {
    }

    public ImmoDbContext(DbContextOptions<ImmoDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AgencyService> AgencyServices { get; set; }

    public virtual DbSet<Agent> Agents { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientEvent> ClientEvents { get; set; }

    public virtual DbSet<EstateStatusHistory> EstateStatusHistories { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<RealEstate> RealEstates { get; set; }

    public virtual DbSet<TypeEstate> TypeEstates { get; set; }

    public virtual DbSet<TypeEvent> TypeEvents { get; set; }

    public virtual DbSet<TypeService> TypeServices { get; set; }

    public virtual DbSet<TypeStatusOffer> TypeStatusOffers { get; set; }

    public virtual DbSet<UserProg> UserProgs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=EsayImmo;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AgencyService>(entity =>
        {
            entity.HasKey(e => e.IdService).HasName("PK__Agency_S__D06FB5A8796E9C9F");

            entity.ToTable("Agency_Services");

            entity.Property(e => e.IdService).HasColumnName("id_service");
            entity.Property(e => e.DateService)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date_service");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IdAgent).HasColumnName("id_agent");
            entity.Property(e => e.IdClient).HasColumnName("id_client");
            entity.Property(e => e.IdTypeService).HasColumnName("id_typeService");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("price");

            entity.HasOne(d => d.IdAgentNavigation).WithMany(p => p.AgencyServices)
                .HasForeignKey(d => d.IdAgent)
                .HasConstraintName("FK__Agency_Se__id_ag__60A75C0F");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.AgencyServices)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Agency_Se__id_cl__5FB337D6");

            entity.HasOne(d => d.IdTypeServiceNavigation).WithMany(p => p.AgencyServices)
                .HasForeignKey(d => d.IdTypeService)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Agency_Se__id_ty__5EBF139D");
        });

        modelBuilder.Entity<Agent>(entity =>
        {
            entity.HasKey(e => e.IdAgent).HasName("PK__Agent__0C90BC72C497CE60");

            entity.ToTable("Agent");

            entity.Property(e => e.IdAgent).HasColumnName("id_agent");
            entity.Property(e => e.Active)
                .HasDefaultValue(true)
                .HasColumnName("active");
            entity.Property(e => e.HireDate).HasColumnName("hire_date");
            entity.Property(e => e.IdUser).HasColumnName("id_user");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Agents)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Agent__id_user__49C3F6B7");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient).HasName("PK__Clients__6EC2B6C05EDC46E1");

            entity.Property(e => e.IdClient).HasColumnName("id_client");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(100)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(100)
                .HasColumnName("lastname");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.TypeClient)
                .HasMaxLength(50)
                .HasColumnName("type_client");
        });

        modelBuilder.Entity<ClientEvent>(entity =>
        {
            entity.HasKey(e => new { e.IdClient, e.IdEvent }).HasName("PK__Client_E__87D152E6792420B6");

            entity.ToTable("Client_Event");

            entity.Property(e => e.IdClient).HasColumnName("id_client");
            entity.Property(e => e.IdEvent).HasColumnName("id_event");
            entity.Property(e => e.RoleInEvent)
                .HasMaxLength(50)
                .HasColumnName("role_in_event");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.ClientEvents)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Client_Ev__id_cl__5812160E");

            entity.HasOne(d => d.IdEventNavigation).WithMany(p => p.ClientEvents)
                .HasForeignKey(d => d.IdEvent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Client_Ev__id_ev__59063A47");
        });

        modelBuilder.Entity<EstateStatusHistory>(entity =>
        {
            entity.HasKey(e => e.IdHistory).HasName("PK__Estate_s__0B4F24D3ED06333A");

            entity.ToTable("Estate_status_history");

            entity.Property(e => e.IdHistory).HasColumnName("id_history");
            entity.Property(e => e.DateEnd)
                .HasColumnType("datetime")
                .HasColumnName("date_end");
            entity.Property(e => e.DateStart)
                .HasColumnType("datetime")
                .HasColumnName("date_start");
            entity.Property(e => e.IdEstate).HasColumnName("id_estate");
            entity.Property(e => e.IdStatusOffer).HasColumnName("id_statusOffer");

            entity.HasOne(d => d.IdEstateNavigation).WithMany(p => p.EstateStatusHistories)
                .HasForeignKey(d => d.IdEstate)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Estate_st__id_es__4222D4EF");

            entity.HasOne(d => d.IdStatusOfferNavigation).WithMany(p => p.EstateStatusHistories)
                .HasForeignKey(d => d.IdStatusOffer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Estate_st__id_st__4316F928");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.IdEvent).HasName("PK__Event__913E426F197EC3D2");

            entity.ToTable("Event");

            entity.Property(e => e.IdEvent).HasColumnName("id_event");
            entity.Property(e => e.DateEvent)
                .HasColumnType("datetime")
                .HasColumnName("date_event");
            entity.Property(e => e.IdEstate).HasColumnName("id_estate");
            entity.Property(e => e.IdTypeEvent).HasColumnName("id_typeEvent");
            entity.Property(e => e.Notes).HasColumnName("notes");

            entity.HasOne(d => d.IdEstateNavigation).WithMany(p => p.Events)
                .HasForeignKey(d => d.IdEstate)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Event__id_estate__5535A963");

            entity.HasOne(d => d.IdTypeEventNavigation).WithMany(p => p.Events)
                .HasForeignKey(d => d.IdTypeEvent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Event__id_typeEv__5441852A");
        });

        modelBuilder.Entity<RealEstate>(entity =>
        {
            entity.HasKey(e => e.IdEstate).HasName("PK__Real_est__86991D90A10DF2DC");

            entity.ToTable("Real_estate");

            entity.HasIndex(e => e.Reference, "UQ__Real_est__FD90DA9906BC1E9A").IsUnique();

            entity.Property(e => e.IdEstate).HasColumnName("id_estate");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creation_date");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IdOwner).HasColumnName("id_owner");
            entity.Property(e => e.IdTypeEstate).HasColumnName("id_typeEstate");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(10)
                .HasColumnName("postal_code");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Reference)
                .HasMaxLength(50)
                .HasColumnName("reference");
            entity.Property(e => e.Surface)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("surface");
            entity.Property(e => e.Title)
                .HasMaxLength(150)
                .HasColumnName("title");

            entity.HasOne(d => d.IdOwnerNavigation).WithMany(p => p.RealEstates)
                .HasForeignKey(d => d.IdOwner)
                .HasConstraintName("FK__Real_esta__id_ow__4E88ABD4");

            entity.HasOne(d => d.IdTypeEstateNavigation).WithMany(p => p.RealEstates)
                .HasForeignKey(d => d.IdTypeEstate)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Real_esta__id_ty__3C69FB99");
        });

        modelBuilder.Entity<TypeEstate>(entity =>
        {
            entity.HasKey(e => e.IdTypeEstate).HasName("PK__Type_est__8D985971E83DBD74");

            entity.ToTable("Type_estate");

            entity.HasIndex(e => e.Label, "UQ__Type_est__4823FDB2D9F2C0A9").IsUnique();

            entity.Property(e => e.IdTypeEstate).HasColumnName("id_typeEstate");
            entity.Property(e => e.Label)
                .HasMaxLength(50)
                .HasColumnName("label");
        });

        modelBuilder.Entity<TypeEvent>(entity =>
        {
            entity.HasKey(e => e.IdTypeEvent).HasName("PK__Type_eve__523F92307C992195");

            entity.ToTable("Type_event");

            entity.HasIndex(e => e.Label, "UQ__Type_eve__4823FDB2D04F90B6").IsUnique();

            entity.Property(e => e.IdTypeEvent).HasColumnName("id_typeEvent");
            entity.Property(e => e.Label)
                .HasMaxLength(50)
                .HasColumnName("label");
        });

        modelBuilder.Entity<TypeService>(entity =>
        {
            entity.HasKey(e => e.IdTypeService).HasName("PK__Type_ser__778D732A13B6EA5F");

            entity.ToTable("Type_service");

            entity.HasIndex(e => e.Label, "UQ__Type_ser__4823FDB295CC097E").IsUnique();

            entity.Property(e => e.IdTypeService).HasColumnName("id_typeService");
            entity.Property(e => e.Label)
                .HasMaxLength(50)
                .HasColumnName("label");
        });

        modelBuilder.Entity<TypeStatusOffer>(entity =>
        {
            entity.HasKey(e => e.IdStatusOffer).HasName("PK__Type_sta__BEE5E3A050665C66");

            entity.ToTable("Type_statusOffer");

            entity.HasIndex(e => e.Label, "UQ__Type_sta__4823FDB26D05B258").IsUnique();

            entity.Property(e => e.IdStatusOffer).HasColumnName("id_statusOffer");
            entity.Property(e => e.Label)
                .HasMaxLength(50)
                .HasColumnName("label");
        });

        modelBuilder.Entity<UserProg>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__UserProg__D2D14637D44F133D");

            entity.ToTable("UserProg");

            entity.HasIndex(e => e.Username, "UQ__UserProg__F3DBC572D11DB980").IsUnique();

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
