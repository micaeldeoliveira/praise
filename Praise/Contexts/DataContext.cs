using Praise.Models;
using Microsoft.EntityFrameworkCore;

namespace Praise.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Music> Musics { get; set; }
        public DbSet<Event> Events { get; set; }        
        public DbSet<EventMusic> EventsMusics { get; set; }
        public DbSet<EventUser> EventsUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region User Entity
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<User>().Property(x => x.Id);
            modelBuilder.Entity<User>().Property(x => x.CreatedDate).IsRequired();
            modelBuilder.Entity<User>().Property(x => x.LastModifiedDate);
            modelBuilder.Entity<User>().Property(x => x.Name).HasMaxLength(100).HasColumnType("varchar(100)");
            modelBuilder.Entity<User>().Property(x => x.Username).HasMaxLength(100).HasColumnType("varchar(20)");
            modelBuilder.Entity<User>().Property(x => x.Email).HasMaxLength(100).HasColumnType("varchar(100)");
            modelBuilder.Entity<User>().Property(x => x.Password).IsFixedLength().HasColumnType("char(32)");
            modelBuilder.Entity<User>().Property(x => x.Phone).HasMaxLength(14).HasColumnType("varchar(14)");
            modelBuilder.Entity<User>().Property(x => x.Photo).HasMaxLength(100).HasColumnType("varchar(100)");
            modelBuilder.Entity<User>().Property(x => x.Birthday);
            modelBuilder.Entity<User>().Property(x => x.LastLogon);
            modelBuilder.Entity<User>().Property(x => x.Disabled).IsRequired().HasColumnType("bit");
            modelBuilder.Entity<User>().HasIndex(x => x.Username);
            modelBuilder.Entity<User>().HasIndex(x => x.Email);
            #endregion User Entity

            #region Music Entity
            modelBuilder.Entity<Music>().ToTable("musics");
            modelBuilder.Entity<Music>().Property(x => x.Id);
            modelBuilder.Entity<Music>().Property(x => x.CreatedDate).IsRequired();
            modelBuilder.Entity<Music>().Property(x => x.LastModifiedDate);
            modelBuilder.Entity<Music>().Property(x => x.Title).HasMaxLength(60).HasColumnType("varchar(60)");
            modelBuilder.Entity<Music>().Property(x => x.Reminder).HasMaxLength(60).HasColumnType("varchar(60)");
            modelBuilder.Entity<Music>().Property(x => x.Singer).HasMaxLength(60).HasColumnType("varchar(60)");
            modelBuilder.Entity<Music>().Property(x => x.Lirycs).HasColumnType("text");
            modelBuilder.Entity<Music>().Property(x => x.Notation).HasColumnType("text");
            modelBuilder.Entity<Music>().Property(x => x.Video).HasMaxLength(60).HasColumnType("varchar(2048)");
            modelBuilder.Entity<Music>().Property(x => x.Play).IsRequired().HasColumnType("bit");
            #endregion Music Entity

            #region Event Entity
            modelBuilder.Entity<Event>().ToTable("events");
            modelBuilder.Entity<Event>().Property(x => x.Id);
            modelBuilder.Entity<Event>().Property(x => x.CreatedDate).IsRequired();
            modelBuilder.Entity<Event>().Property(x => x.LastModifiedDate);
            modelBuilder.Entity<Event>().Property(x => x.Name).HasMaxLength(50).HasColumnType("varchar(50)");
            modelBuilder.Entity<Event>().Property(x => x.Date).IsRequired();
            modelBuilder.Entity<Event>().Property(x => x.Local).HasMaxLength(50).HasColumnType("varchar(100)");
            modelBuilder.Entity<Event>().Property(x => x.Note).HasMaxLength(50).HasColumnType("varchar(1000)");
            modelBuilder.Entity<Event>().Property(x => x.StartHour);
            modelBuilder.Entity<Event>().Property(x => x.EndHour);
            modelBuilder.Entity<Event>().Property(x => x.Status).IsRequired().HasColumnType("int");
            #endregion Event Entity

            #region EventUser Relationships 
            modelBuilder.Entity<EventUser>()
                .HasKey(eu => new { eu.EventId, eu.UserId });
            modelBuilder.Entity<EventUser>()
                .HasOne(eu => eu.Event)
                .WithMany(e => e.EventUsers)
                .HasForeignKey(eu => eu.EventId);
            modelBuilder.Entity<EventUser>()
                .HasOne(eu => eu.User)
                .WithMany(u => u.EventUsers)
                .HasForeignKey(eu => eu.UserId);
            //modelBuilder.Entity<EventUser>()
            //    .Property(x => x.Presence)
            //    .IsRequired()
            //    .HasColumnType("bit");
            //modelBuilder.Entity<EventUser>()
            //    .Property(x => x.Note)
            //    .IsRequired()
            //    .HasColumnType("varchar(100)");
            #endregion EventUser Relationships

            #region EventMusic Relationships 
            modelBuilder.Entity<EventMusic>()
                .HasKey(em => new { em.EventId, em.MusicId });
            modelBuilder.Entity<EventMusic>()
                .HasOne(em => em.Event)
                .WithMany(e => e.EventMusics)
                .HasForeignKey(eu => eu.EventId);
            modelBuilder.Entity<EventMusic>()
                .HasOne(em => em.Music)
                .WithMany(u => u.EventMusics)
                .HasForeignKey(em => em.MusicId);
            modelBuilder.Entity<EventMusic>()
                .Property(x => x.Order)
                .IsRequired()
                .HasColumnType("int");
            modelBuilder.Entity<EventMusic>()
                .Property(x => x.Play)
                .IsRequired()
                .HasColumnType("bit");
            #endregion EventMusic Relationships
        }
    }
}