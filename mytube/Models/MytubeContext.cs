using Microsoft.EntityFrameworkCore;

namespace mytube.Models
{
    public class MytubeContext : DbContext
    {
        public DbSet<UserItem>? Users { get; set; }

        public DbSet<VideoItem>? Videos { get; set; }

        public DbSet<CommentItem>? Comments { get; set; }

        public MytubeContext(DbContextOptions<MytubeContext> options)
        : base(options)
        {
            base.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserItem>(user =>
            {
                user.HasKey(user => user.ID);
                user.Property(user => user.firstName).IsRequired();
                user.Property(user => user.lastName).IsRequired();
                user.Property(user => user.password).IsRequired(); 
                user.Property(user => user.username).IsRequired();
                user.Property(user=> user.email).IsRequired();
                user.Property(user => user.createdAt).IsRequired();
                user.Property(user => user.enabled).IsRequired();
            });

            modelBuilder.Entity<VideoItem>(video =>
            {
                video.HasKey(video => video.ID);
                video.Property(video => video.Name).IsRequired();
                video.Property(video => video.Data).IsRequired();
                video.HasOne(u => u.User)
                    .WithMany(v => v.Videos);
                video.HasOne(u => u.User).WithMany(c => c.Comments);
            });
            modelBuilder.Entity<CommentItem>(comment =>
            {
                comment.HasKey(comment => comment.ID);
                comment.Property(comment => comment.Body).IsRequired();
                comment.Property(comment => comment.Likes);
                comment.Property(comment => comment.Dislikes);
                comment.Property(comment => comment.IsPinned);
                comment.Property(comment => comment.CreatedAt).IsRequired();
                comment.Property(comment => comment.LastUpdatedBy);
                comment.Property(comment => comment.DeletedAt);
                comment.Property(comment => comment.LastUpdatedAt);
                comment.Property(comment => comment.DeletedAt);
                comment.HasMany(u => u.User).WithOne(v => v.Videos);
            });
        }
    }
}
