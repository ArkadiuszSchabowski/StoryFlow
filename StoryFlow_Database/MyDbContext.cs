using Microsoft.EntityFrameworkCore;
using StoryFlow_Database.Entities;

namespace StoryFlow_Database
{
    public class MyDbContext : DbContext
    {
        public DbSet<Sentence> Sentences { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserSentence> UserSentences { get; set; }
        public DbSet<UserStory> UserStories { get; set; }
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(u => u.UserSentences)
                .WithOne(us => us.User)
                .HasForeignKey(us => us.UserId);

            modelBuilder.Entity<Sentence>().HasMany(s => s.UserSentences)
                .WithOne(s => s.Sentence)
                .HasForeignKey(s => s.SentenceId);

            modelBuilder.Entity<User>().HasMany(u => u.UserStories)
                .WithOne(us => us.User)
                .HasForeignKey(us => us.UserId);

            modelBuilder.Entity<Story>().HasMany(s => s.UserStories)
                .WithOne(s => s.Story)
                .HasForeignKey(s => s.StoryId);
        }
    }
}
