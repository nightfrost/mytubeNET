using Microsoft.EntityFrameworkCore;

namespace mytube.Models
{
    public class MytubeContext : DbContext
    {
        public DbSet<UserItem> Users { get; set; }

        public DbSet<VideoItem> Videos { get; set; }

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
                video.Property(video => video.Poster);
                video.Property(video => video.ReleaseDate);
                video.Property(video => video.Data).IsRequired();
                video.HasOne(u => u.User)
                    .WithMany(v => v.Videos);
            });
        }


    }
}
