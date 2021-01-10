using Microsoft.EntityFrameworkCore;
using PhotoSite.Data.Entities;

namespace PhotoSite.Data.Base
{
    public sealed class MainDbContext : DbContext
    {
        public DbSet<Album>? Albums { get; set; }
        public DbSet<Photo>? Photos { get; set; }
        public DbSet<PhotoToTag>? PhotoToTags { get; set; }
        public DbSet<Tag>? Tags { get; set; }
        public DbSet<ConfigParam>? ConfigParam { get; set; }
        public DbSet<Watermark>? Watermarks { get; set; }
        public DbSet<BlackIp>? BlackIps { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<ConfigParam>()
                .HasKey(x => new { x.Name });


            modelBuilder.Entity<PhotoToTag>()
                .HasKey(c => new { c.PhotoId, c.TagId });

            modelBuilder.Entity<PhotoToTag>()
                .HasOne(t => t.Tag)
                .WithMany(p => p!.Photos)
                .HasForeignKey(t => t.TagId);

            modelBuilder.Entity<PhotoToTag>()
                .HasOne(p => p.Photo)
                .WithMany(t => t!.Tags)
                .HasForeignKey(p => p.PhotoId);


            modelBuilder.Entity<Watermark>()
                .HasKey(c => new { c.PhotoId });


            modelBuilder.Entity<Album>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();


            modelBuilder.Entity<BlackIp>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();


            modelBuilder.Entity<Photo>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();


            modelBuilder.Entity<Tag>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

        }
    }
}
