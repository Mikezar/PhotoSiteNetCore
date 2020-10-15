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
        public DbSet<SiteSettings>? SiteSettings { get; set; }
        public DbSet<Watermark>? Watermarks { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SiteSettings>()
                .Ignore(x => x.Id)
                .HasKey(x => new { x.Name });

            modelBuilder.Entity<PhotoToTag>()
                .Ignore(x => x.Id).HasKey(c => new { c.PhotoId, c.TagId });

            modelBuilder.Entity<PhotoToTag>()
                .HasOne(t => t.Tag)
                .WithMany(p => p!.Photos)
                .HasForeignKey(t => t.TagId);

            modelBuilder.Entity<PhotoToTag>()
                .HasOne(p => p.Photo)
                .WithMany(t => t!.Tags)
                .HasForeignKey(p => p.PhotoId);
        }
    }
}
