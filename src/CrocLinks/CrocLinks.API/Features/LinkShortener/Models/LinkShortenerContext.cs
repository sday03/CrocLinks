using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CrocLinks.API.Features.LinkShortener.Models
{
    public partial class LinkShortenerContext : DbContext
    {
        public LinkShortenerContext()
        {
        }

        public LinkShortenerContext(DbContextOptions<LinkShortenerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Link> Links { get; set; }
        public virtual DbSet<LinkUsage> LinkUsages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Link>(entity =>
            {
                entity.ToTable("Link");

                entity.Property(e => e.LinkId).HasColumnName("LinkID");

                entity.Property(e => e.LinkCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LinkExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.LinkToken)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.OriginalLink)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ShortenedLink)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LinkUsage>(entity =>
            {
                entity.ToTable("LinkUsage");

                entity.HasIndex(e => e.LinkId, "IX_LinkUsage_01");

                entity.Property(e => e.LinkUsageId).HasColumnName("LinkUsageID");

                entity.Property(e => e.LinkClickedDate).HasColumnType("datetime");

                entity.Property(e => e.LinkId).HasColumnName("LinkID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
