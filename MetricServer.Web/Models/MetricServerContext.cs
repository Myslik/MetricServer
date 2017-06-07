using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MetricServer.Web.Models
{
    public partial class MetricServerContext : DbContext
    {
        public MetricServerContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Measurement> Measurement { get; set; }
        public virtual DbSet<Metric> Metric { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Measurement>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValueSql("'lightgray'");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Metric)
                    .WithMany(p => p.Measurement)
                    .HasForeignKey(d => d.MetricId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Measurement_Metric");
            });

            modelBuilder.Entity<Metric>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}