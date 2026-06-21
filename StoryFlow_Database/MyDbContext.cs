using Microsoft.EntityFrameworkCore;
using StoryFlow_Database.Entities;
using StoryFlow_Shared.Enums;

namespace StoryFlow_Database
{
    public class MyDbContext : DbContext
    {

        public DbSet<Hobby> Hobbies { get; set; }
        public DbSet<Sentence> Sentences { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserHobby> UserHobbies { get; set; }
        public DbSet<UserSentence> UserSentences { get; set; }
        public DbSet<UserStory> UserStories { get; set; }
        public DbSet<Role> Roles { get; set; }
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(u => u.UserHobbies)
                .WithOne(uh => uh.User)
                .HasForeignKey(uh => uh.UserId);

            modelBuilder.Entity<User>().HasMany(u => u.UserSentences)
                .WithOne(us => us.User)
                .HasForeignKey(us => us.UserId);

            modelBuilder.Entity<User>().HasMany(u => u.UserStories)
                .WithOne(us => us.User)
                .HasForeignKey(us => us.UserId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<Story>().HasMany(s => s.Sentences)
                .WithOne(se => se.Story)
                .HasForeignKey(se => se.StoryId);

            modelBuilder.Entity<Story>().HasMany(s => s.UserStories)
                .WithOne(us => us.Story)
                .HasForeignKey(us => us.StoryId);

            modelBuilder.Entity<Hobby>().HasMany(h => h.UserHobbies)
                .WithOne(uh => uh.Hobby)
                .HasForeignKey(uh => uh.HobbyId);

            modelBuilder.Entity<UserStory>()
                .HasIndex(us => new { us.UserId, us.StoryId })
                .IsUnique();
        }
    }
}
