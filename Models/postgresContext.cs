using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SmartSaver.Models
{
    public partial class postgresContext : DbContext
    {
        public postgresContext()
        {
        }

        public postgresContext(DbContextOptions<postgresContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Goal> Goal { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }
        public virtual DbSet<Transaction> Transaction { get; set; }
        public virtual DbSet<TransactionTag> TransactionTag { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=postgres");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category", "smartsaver");

                entity.HasIndex(e => e.Title)
                    .HasName("uq_smartsaver.category__title")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DedicatedAmount)
                    .HasColumnName("dedicated_amount")
                    .HasColumnType("numeric");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Goal>(entity =>
            {
                entity.ToTable("goal", "smartsaver");

                entity.HasIndex(e => new { e.Title, e.Description })
                    .HasName("uq_smartsaver.goal__title_description")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("numeric");

                entity.Property(e => e.Creationdate)
                    .HasColumnName("creationdate")
                    .HasColumnType("date");

                entity.Property(e => e.Deadlinedate)
                    .HasColumnName("deadlinedate")
                    .HasColumnType("date");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(1000);

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("tag", "smartsaver");

                entity.HasIndex(e => e.Title)
                    .HasName("uq_smartsaver.tag__title")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("transaction", "smartsaver");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("numeric");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CounterParty)
                    .HasColumnName("counter_party")
                    .HasMaxLength(1000);

                entity.Property(e => e.Details)
                    .HasColumnName("details")
                    .HasMaxLength(1000);

                entity.Property(e => e.TrTime).HasColumnName("tr_time");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Transaction)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_smartsaver.transaction__category_id");
            });

            modelBuilder.Entity<TransactionTag>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("transaction_tag", "smartsaver");

                entity.HasIndex(e => new { e.TransactionId, e.TagId })
                    .HasName("uq_smartsaver.transaction_tag__transaction_id_tag_id")
                    .IsUnique();

                entity.Property(e => e.TagId).HasColumnName("tag_id");

                entity.Property(e => e.TransactionId).HasColumnName("transaction_id");

                entity.HasOne(d => d.Tag)
                    .WithMany()
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_smartsaver.transaction_tag__tag_id");

                entity.HasOne(d => d.Transaction)
                    .WithMany()
                    .HasForeignKey(d => d.TransactionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_smartsaver.transaction_tag__transaction_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
