using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ChatService.Entity.Tenant.Entities;
using System;
using System.Collections.Generic;


namespace ChatService.Repository
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<RoomUser> RoomUsers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Message1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Message");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Room");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.RoomName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RoomUser>(entity =>
            {
                entity.ToTable("RoomUser");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
