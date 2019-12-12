using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSPersonal.Models
{
    public class CMSContext : DbContext
    {
        public DbSet<Media> Media { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Page> Page { get; set; }

        public CMSContext(DbContextOptions<CMSContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Media>().Ignore(e => e.ThumbUrl);
            modelBuilder.Entity<Media>().Ignore(e => e.MediaDate);
            modelBuilder.Entity<Media>().Ignore(e => e.Result);
            modelBuilder.Entity<Media>().Ignore(e => e.DisplayUrl);
            modelBuilder.Entity<Media>().Ignore(e => e.FileSize);
            modelBuilder.Entity<Media>().Ignore(e => e.FileType);
            modelBuilder.Entity<Media>().Ignore(e => e.Dimension);

            modelBuilder.Entity<Media>(entity =>
            {
                entity.Property(e => e.AddedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Alt)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.Property(e => e.AddedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Item)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Page>(entity =>
            {
                entity.Property(e => e.AddedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.MetaDescription)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.MetaKeyword)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.MetaTitle)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });
        }
    }
}
