using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Final_2252.Models.db;

public partial class FinalDb2252Context : DbContext
{
    public FinalDb2252Context()
    {
    }

    public FinalDb2252Context(DbContextOptions<FinalDb2252Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Airport> Airports { get; set; }

    public virtual DbSet<Fight> Fights { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server = DESKTOP-7R12NSA\\SQLEXPRESS01; Database = finalDB_2252; Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Airport>(entity =>
        {
            entity.HasKey(e => e.AirportId).HasName("PK__Airport__E3DBE08AA7F6A1F5");

            entity.ToTable("Airport");

            entity.Property(e => e.AirportId).HasColumnName("AirportID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.AirportCode).HasMaxLength(50);
            entity.Property(e => e.AirportName).HasMaxLength(255);
        });

        modelBuilder.Entity<Fight>(entity =>
        {
            entity.HasKey(e => e.FightId).HasName("PK__Fight__A156AA268ACB2051");

            entity.ToTable("Fight");

            entity.Property(e => e.FightId).HasColumnName("FightID");
            entity.Property(e => e.BoardingTime)
                .HasColumnType("datetime")
                .HasColumnName("Boarding_time");
            entity.Property(e => e.DepartDate)
                .HasColumnType("date")
                .HasColumnName("Depart_date");
            entity.Property(e => e.FightNo).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.Gate).HasMaxLength(25);
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.MiddleName).HasMaxLength(255);
            entity.Property(e => e.Seat).HasMaxLength(25);
            entity.Property(e => e.Seq).HasMaxLength(25);
            entity.Property(e => e.Zone).HasMaxLength(25);

            entity.HasOne(d => d.AirportDestinationNavigation).WithMany(p => p.FightAirportDestinationNavigations)
                .HasForeignKey(d => d.AirportDestination)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Fight__AirportDe__5AEE82B9");

            entity.HasOne(d => d.AirportSourceNavigation).WithMany(p => p.FightAirportSourceNavigations)
                .HasForeignKey(d => d.AirportSource)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Fight__AirportSo__59FA5E80");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
