using System;
using System.Collections.Generic;
using HotelsApi.Entities;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace HotelsApi.Context;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Barangay> Barangays { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Hotel> Hotels { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Barangay>(entity =>
        {
            entity.HasKey(e => e.BarangayId).HasName("PRIMARY");

            entity.HasIndex(e => e.CityId, "Barangays_Cities_FK");

            entity.Property(e => e.BarangayId).HasColumnName("barangayId");
            entity.Property(e => e.BarangayName)
                .HasMaxLength(100)
                .HasColumnName("barangayName");
            entity.Property(e => e.CityId).HasColumnName("cityId");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(100)
                .HasColumnName("postalCode");

            entity.HasOne(d => d.City).WithMany(p => p.Barangays)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Barangays_Cities_FK");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PRIMARY");

            entity.HasIndex(e => e.StateId, "Cities_States_FK");

            entity.Property(e => e.CityId).HasColumnName("cityId");
            entity.Property(e => e.CityCode)
                .HasMaxLength(100)
                .HasColumnName("cityCode");
            entity.Property(e => e.CityName)
                .HasMaxLength(100)
                .HasColumnName("cityName");
            entity.Property(e => e.StateId).HasColumnName("stateId");

            entity.HasOne(d => d.State).WithMany(p => p.Cities)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Cities_States_FK");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PRIMARY");

            entity.Property(e => e.CountryId).HasColumnName("countryId");
            entity.Property(e => e.CountryCode)
                .HasMaxLength(100)
                .HasColumnName("countryCode");
            entity.Property(e => e.CountryName)
                .HasMaxLength(100)
                .HasColumnName("countryName");
        });

        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.HasKey(e => e.HotelId).HasName("PRIMARY");

            entity.HasIndex(e => e.BarangayId, "Hotels_Barangays_FK");

            entity.Property(e => e.HotelId).HasColumnName("hotelId");
            entity.Property(e => e.BarangayId).HasColumnName("barangayId");
            entity.Property(e => e.HotelCode)
                .HasMaxLength(100)
                .HasColumnName("hotelCode");
            entity.Property(e => e.HotelDescription).HasColumnName("hotelDescription");
            entity.Property(e => e.HotelName)
                .HasMaxLength(100)
                .HasColumnName("hotelName");

            entity.HasOne(d => d.Barangay).WithMany(p => p.Hotels)
                .HasForeignKey(d => d.BarangayId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Hotels_Barangays_FK");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.StateId).HasName("PRIMARY");

            entity.HasIndex(e => e.CountryId, "States_Countries_FK");

            entity.Property(e => e.StateId).HasColumnName("stateId");
            entity.Property(e => e.CountryId).HasColumnName("countryId");
            entity.Property(e => e.StateCode)
                .HasMaxLength(100)
                .HasColumnName("stateCode");
            entity.Property(e => e.StateName)
                .HasMaxLength(100)
                .HasColumnName("stateName");

            entity.HasOne(d => d.Country).WithMany(p => p.States)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("States_Countries_FK");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PRIMARY");

            entity.HasIndex(e => e.HotelId, "Transactions_Hotels_FK");

            entity.Property(e => e.DateFrom).HasColumnName("dateFrom");
            entity.Property(e => e.DateTo).HasColumnName("dateTo");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(100)
                .HasColumnName("emailAddress");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("fullName");
            entity.Property(e => e.HotelCode)
                .HasMaxLength(100)
                .HasColumnName("hotelCode");
            entity.Property(e => e.HotelId).HasColumnName("hotelId");
            entity.Property(e => e.HotelName)
                .HasMaxLength(100)
                .HasColumnName("hotelName");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(100)
                .HasColumnName("phoneNumber");

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
