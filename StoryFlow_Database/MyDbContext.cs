using Microsoft.EntityFrameworkCore;
using StoryFlow_Database.Entities;

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
        public DbSet<UserWordLesson> UserWordLessons { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<StoryPoint> StoryPoints { get; set; }
        public DbSet<StorySeason> StorySeazons { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<WordLesson> WordLessons { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<WordPoint> WordPoints { get; set; }
        public DbSet<BlogPostSection> BlogPostSections { get; set; }
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

            modelBuilder.Entity<User>().HasMany(u => u.UserWordLessons)
                .WithOne(us => us.User)
                .HasForeignKey(us => us.UserId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<StorySeason>().HasMany(ss => ss.Stories)
                .WithOne(s => s.StorySeason)
                .HasForeignKey(s => s.StorySeasonId);

            modelBuilder.Entity<Story>().HasMany(s => s.Sentences)
                .WithOne(se => se.Story)
                .HasForeignKey(se => se.StoryId);

            modelBuilder.Entity<Story>().HasMany(s => s.UserStories)
                .WithOne(us => us.Story)
                .HasForeignKey(us => us.StoryId);

            modelBuilder.Entity<WordLesson>().HasMany(w => w.UserWordLessons)
                .WithOne(wl => wl.WordLesson)
                .HasForeignKey(wl => wl.WordLessonId);

            modelBuilder.Entity<Hobby>().HasMany(h => h.UserHobbies)
                .WithOne(uh => uh.Hobby)
                .HasForeignKey(uh => uh.HobbyId);

            modelBuilder.Entity<UserStory>()
                .HasIndex(us => new { us.UserId, us.StoryId })
                .IsUnique();

            modelBuilder.Entity<UserWordLesson>()
                .HasIndex(us => new { us.UserId, us.WordLessonId })
                .IsUnique();

            modelBuilder.Entity<Story>()
                .HasOne(s => s.Quiz)
                .WithOne(q => q.Story)
                .HasForeignKey<Quiz>(q => q.StoryId);

            modelBuilder.Entity<Story>()
                .HasOne(s => s.StoryPoint)
                .WithOne(sp => sp.Story)
                .HasForeignKey<StoryPoint>(sp => sp.StoryId);

            modelBuilder.Entity<Quiz>()
                .HasMany(q => q.Questions)
                .WithOne(qu => qu.Quiz)
                .HasForeignKey(qu => qu.QuizId);

            modelBuilder.Entity<Question>()
                .HasMany(q => q.Answers)
                .WithOne(a => a.Question)
                .HasForeignKey(a => a.QuestionId);

            modelBuilder.Entity<BlogPost>()
                .HasMany(b => b.Sections)
                .WithOne(bps => bps.BlogPost)
                .HasForeignKey(bps => bps.BlogPostId);

            modelBuilder.Entity<BlogPost>()
                .HasIndex(b => b.Slug)
                .IsUnique();

            modelBuilder.Entity<BlogPost>(entity =>
            {
                entity.Property(x => x.MetaTitleContent).HasMaxLength(50);
                entity.Property(x => x.MetaTitleDescription).HasMaxLength(115);
            });

            modelBuilder.Entity<WordLesson>().
                HasMany(wl => wl.Words)
                .WithOne(w => w.WordLesson)
                .HasForeignKey(w => w.WordLessonId);

            modelBuilder.Entity<WordLesson>()
                .HasOne(wl => wl.WordPoint)
                .WithOne(w => w.WordLesson)
                .HasForeignKey<WordPoint>(w => w.WordLessonId);
        }
    }
}
