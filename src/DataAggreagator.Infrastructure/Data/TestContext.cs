using DataAggregator.Domain.Entities;
using DataAggregator.Domain.Entities.SpecificCustomers;
using DataAggregator.Domain.Entities.SpecificEvents;
using Microsoft.EntityFrameworkCore;

namespace DataAggregator.Infrastructure.Data;

public partial class TestContext : DbContext
{
    public TestContext()
    {
    }

    public TestContext(DbContextOptions<TestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer101> Customer101s { get; set; }

    public virtual DbSet<Customer145> Customer145s { get; set; }

    public virtual DbSet<Customer2> Customer2s { get; set; }

    public virtual DbSet<EventTypes2> EventTypes2s { get; set; }

    public virtual DbSet<Events101> Events101s { get; set; }

    public virtual DbSet<Events145> Events145s { get; set; }

    public virtual DbSet<Events2> Events2s { get; set; }

    public virtual DbSet<NotificationsBroker> NotificationsBrokers { get; set; }

    public virtual DbSet<Tenant> Tenants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer101>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC07A573F9BB");

            entity.ToTable("Customer_101");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(128);
            entity.Property(e => e.LastLoginDate).HasColumnType("datetime");
            entity.Property(e => e.PasswordHash).HasMaxLength(128);
            entity.Property(e => e.Salutation).HasMaxLength(10);
            entity.Property(e => e.FirstName).IsRequired();
            entity.Property(e => e.LastName).IsRequired();
        });

        modelBuilder.Entity<Customer145>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Customer_145");

            entity.Property(e => e.Email).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.Password).HasMaxLength(128);
            entity.Property(e => e.UserId).HasMaxLength(128);
        });

        modelBuilder.Entity<Customer2>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC07796D9768");

            entity.ToTable("Customer_2");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email).HasMaxLength(128);
            entity.Property(e => e.JobPosition).HasMaxLength(128);
            entity.Property(e => e.GivenName).IsRequired();
            entity.Property(e => e.FamilyName).IsRequired();
            entity.Property(e => e.PasswordHash).HasMaxLength(128);
        });

        modelBuilder.Entity<EventTypes2>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EventTyp__3214EC0760FCEE46");

            entity.ToTable("EventTypes_2");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(64);
        });

        modelBuilder.Entity<Events101>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Events_1__3214EC074CAFE6E5");

            entity.ToTable("Events_101");

            entity.Property(e => e.Id).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.EventDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Events145>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Events_1__3214EC0772FFE734");

            entity.ToTable("Events_145");

            entity.Property(e => e.Id).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.CustomerId).HasMaxLength(128);
            entity.Property(e => e.EventDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Events2>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Events_2__3214EC07D5001053");

            entity.ToTable("Events_2");

            entity.Property(e => e.Id).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.EventDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<NotificationsBroker>(entity =>
        {
            entity.ToTable("NotificationsBroker");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Email).HasMaxLength(128);
            entity.Property(e => e.FinHash).HasMaxLength(128);
            entity.Property(e => e.FirstName).HasMaxLength(128);
            entity.Property(e => e.LastName).HasMaxLength(128);
        });

        modelBuilder.Entity<Tenant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tenants__3214EC07886D1C99");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.OrganisationName).HasMaxLength(128);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
