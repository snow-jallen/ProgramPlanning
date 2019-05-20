using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProgramPlanning.Models;

namespace ProgramPlanning.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseOutcome> CourseOutcomes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Course>(entity =>
            {
                entity.ToTable("course");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Num).HasColumnName("num");

                entity.Property(e => e.Prefix)
                    .IsRequired()
                    .HasColumnName("prefix");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title");
            });

            builder.Entity<CourseOutcome>(entity =>
            {
                entity.ToTable("courseoutcome");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CourseId).HasColumnName("courseid");

                entity.Property(e => e.Outcome)
                    .IsRequired()
                    .HasColumnName("outcome");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseOutcomes)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("courseid");
            });
        }
    }
}
