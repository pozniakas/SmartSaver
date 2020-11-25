using DbEntities.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebAPI
{
    public class DatabaseContext : DbContext
    {
        private const string _schema = "smartsaver";
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Goal> Goal { get; set; }
        public DbSet<User> User { get; set; }

        public DatabaseContext() { }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("transaction", _schema);

                entity.Property(e => e.CounterParty)
                    .HasMaxLength(1000);
                entity.Property(e => e.Details)
                    .HasMaxLength(1000);

                entity.HasOne(e => e.Category)
                    .WithMany(e => e.Transactions)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.User)
                    .WithMany(e => e.Transactions)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category", _schema);

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.HasIndex(e => e.Title)
                    .IsUnique();

                entity.HasOne(e => e.User)
                    .WithMany(e => e.Categories)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Goal>(entity =>
            {
                entity.ToTable("goal", _schema);

                entity.HasOne(e => e.User)
                    .WithMany(e => e.Goals)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user", _schema);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
