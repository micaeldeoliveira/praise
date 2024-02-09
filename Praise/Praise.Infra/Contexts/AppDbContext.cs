using Microsoft.EntityFrameworkCore;
using Praise.Domain.Entities;

namespace Praise.Infra.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Music> Musics { get; set; }
    //public DbSet<Member> Members { get; set; }    
    //public DbSet<Gig> Gigs { get; set; }
    //public DbSet<GigMusic> GigMusics { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //#region User Entity
        //modelBuilder.Entity<User>().ToTable("users");
        //modelBuilder.Entity<User>().Property(x => x.Id);
        //modelBuilder.Entity<User>().Property(x => x.CreatedDate).IsRequired();
        //modelBuilder.Entity<User>().Property(x => x.LastModifiedDate);
        //modelBuilder.Entity<User>().Property(x => x.Name).HasMaxLength(100).HasColumnType("varchar(100)");
        //modelBuilder.Entity<User>().Property(x => x.Username).HasMaxLength(100).HasColumnType("varchar(20)");
        //modelBuilder.Entity<User>().Property(x => x.Email).HasMaxLength(100).HasColumnType("varchar(100)");
        //modelBuilder.Entity<User>().Property(x => x.Password).IsFixedLength().HasColumnType("char(32)");
        //modelBuilder.Entity<User>().Property(x => x.Phone).HasMaxLength(14).HasColumnType("varchar(14)");
        //modelBuilder.Entity<User>().Property(x => x.Photo).HasMaxLength(100).HasColumnType("varchar(100)");
        //modelBuilder.Entity<User>().Property(x => x.Birthday);
        //modelBuilder.Entity<User>().Property(x => x.LastLogon);
        //modelBuilder.Entity<User>().Property(x => x.Disabled).IsRequired().HasColumnType("bit");
        //modelBuilder.Entity<User>().HasIndex(x => x.Username);
        //modelBuilder.Entity<User>().HasIndex(x => x.Email);
        //#endregion User Entity

        #region Music Entity
        modelBuilder.Entity<Music>().ToTable("Musics");
        modelBuilder.Entity<Music>().Property(x => x.Id);
        modelBuilder.Entity<Music>().Property(x => x.CreatedDate).IsRequired();
        modelBuilder.Entity<Music>().Property(x => x.LastModifiedDate);
        modelBuilder.Entity<Music>().Property(x => x.Title).HasMaxLength(60).HasColumnType("varchar(60)");
        modelBuilder.Entity<Music>().Property(x => x.Reminder).HasMaxLength(60).HasColumnType("varchar(60)");
        modelBuilder.Entity<Music>().Property(x => x.Singer).HasMaxLength(60).HasColumnType("varchar(60)");
        modelBuilder.Entity<Music>().Property(x => x.Lirycs).HasColumnType("text");        
        modelBuilder.Entity<Music>().Property(x => x.Video).HasMaxLength(60).HasColumnType("varchar(2048)");
        modelBuilder.Entity<Music>().Property(x => x.Play).IsRequired().HasColumnType("bit");
        #endregion Music Entity

        //#region Gig Entity
        //modelBuilder.Entity<Gig>().ToTable("Gigs");
        //modelBuilder.Entity<Gig>().Property(x => x.Id);
        //modelBuilder.Entity<Gig>().Property(x => x.CreatedDate).IsRequired();
        //modelBuilder.Entity<Gig>().Property(x => x.LastModifiedDate);
        //modelBuilder.Entity<Gig>().Property(x => x.Name).HasMaxLength(50).HasColumnType("varchar(50)");
        //modelBuilder.Entity<Gig>().Property(x => x.Date).IsRequired();
        //modelBuilder.Entity<Gig>().Property(x => x.Local).HasMaxLength(50).HasColumnType("varchar(100)");
        //modelBuilder.Entity<Gig>().Property(x => x.Note).HasMaxLength(50).HasColumnType("varchar(1000)");
        //modelBuilder.Entity<Gig>().Property(x => x.StartHour);
        //modelBuilder.Entity<Gig>().Property(x => x.EndHour);
        //modelBuilder.Entity<Gig>().Property(x => x.Status).IsRequired().HasColumnType("int");
        //#endregion Event Entity



    }
}