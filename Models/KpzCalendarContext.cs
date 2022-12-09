using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Calendar.Models;

public partial class KpzCalendarContext : DbContext
{
    public KpzCalendarContext()
    {
    }

    public KpzCalendarContext(DbContextOptions<KpzCalendarContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Calendar> Calendars { get; set; }

    public virtual DbSet<DaysOfWeek> DaysOfWeeks { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=KPZ_Calendar;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calendar>(entity =>
        {
            entity.ToTable("Calendar");

            entity.HasOne(d => d.User).WithMany(p => p.Calendars)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Calendar_Users");
        });

        modelBuilder.Entity<DaysOfWeek>(entity =>
        {
            entity.ToTable("DaysOfWeek");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.ToTable("Event");

            entity.Property(e => e.Description)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.StartDate).HasColumnType("date");
            entity.Property(e => e.Time).HasColumnType("date");

            entity.HasOne(d => d.Calendar).WithMany(p => p.Events)
                .HasForeignKey(d => d.CalendarId)
                .HasConstraintName("FK_Event_Calendar");

            entity.HasMany(d => d.Days).WithMany(p => p.Events)
                .UsingEntity<Dictionary<string, object>>(
                    "EventDay",
                    r => r.HasOne<DaysOfWeek>().WithMany()
                        .HasForeignKey("DaysId")
                        .HasConstraintName("FK_EventDay_Days_DaysId"),
                    l => l.HasOne<Event>().WithMany()
                        .HasForeignKey("EventsId")
                        .HasConstraintName("FK_EventDay_Events_EventsId"),
                    j =>
                    {
                        j.HasKey("EventsId", "DaysId");
                        j.ToTable("EventDay");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
