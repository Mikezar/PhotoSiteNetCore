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
        public DbSet<TextAttribute>? TextAttributes { get; set; }
        public DbSet<Watermark>? Watermarks { get; set; }

        private readonly string _connectionString;

        /// <summary>
        /// ctor
        /// </summary>
        public MainDbContext(string connectionString)
        {
            _connectionString = connectionString;
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }
    }
}
