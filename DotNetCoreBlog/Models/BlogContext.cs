using Microsoft.EntityFrameworkCore;

namespace DotNetCoreBlog.Models
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options)
            : base(options)
        {
        }

        public DbSet<BlogPost> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<BlogPost>();

            entity.HasKey(p => p.Id);
            entity.Property(p => p.UrlSlug).HasMaxLength(32);
            entity.Property(p => p.Title).IsRequired().HasMaxLength(100).IsUnicode();
            entity.Property(p => p.Summary).IsRequired().HasMaxLength(500).IsUnicode();
            entity.Property(p => p.Body).IsRequired().IsUnicode();
            entity.Property(p => p.Tags).HasMaxLength(500);

            // URL slugs must be unique across all posts
            entity.HasIndex(p => p.UrlSlug).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
