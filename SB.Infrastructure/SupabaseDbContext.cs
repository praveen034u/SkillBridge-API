using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using SB.Domain.Entities;
using SB.Domain.Model;
using SB.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Text.Json;
using System.Threading.Tasks;

namespace SB.Infrastructure
{
    public partial class SupabaseDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public SupabaseDbContext(DbContextOptions<SupabaseDbContext> options) : base(options) { }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<JobPosting> JobPosting { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserProfile>().ToTable("user_profiles");
            modelBuilder.Entity<Name>().HasNoKey();

            var stringListConverter = new ValueConverter<List<string>, string>(
                 v => JsonConvert.SerializeObject(v),
                 v => JsonConvert.DeserializeObject<List<string>>(v) ?? new List<string>()
                 );
            var salaryConverter = new ValueConverter<SalaryRange, string>(
                 v => JsonConvert.SerializeObject(v),                        // POCO → JSON string
                     v => JsonConvert.DeserializeObject<SalaryRange>(v) ?? new SalaryRange()  // JSON → POCO
                  );
            modelBuilder.Entity<JobPosting>(entity =>
            {
                entity.ToTable("job_postings");   // ✅ correct table mapping

                entity.HasKey(e => e.JobId)
                      .HasName("job_postings_pkey");  // ✅ matches DB constraint name

                entity.Property(e => e.JobId)
                      .HasColumnName("job_id")
                      .HasDefaultValueSql("gen_random_uuid()");

                entity.Property(e => e.EmployerId).HasColumnName("employer_id");
                entity.Property(e => e.Title).HasColumnName("title").IsRequired();
                entity.Property(e => e.Description).HasColumnName("description").IsRequired();
                // entity.Property(e => e.RequiredSkills).HasColumnName("required_skills");
                //entity.Property(e => e.RequiredSkills).HasColumnType("jsonb");
                entity.Property(e => e.RequiredSkills).HasColumnName("required_skills")
                            .HasConversion(stringListConverter)
                            .HasColumnType("jsonb");
                entity.Property(e => e.Location).HasColumnName("location");
                entity.Property(e => e.EmploymentType).HasColumnName("employment_type");
                entity.Property(e => e.SalaryRange).HasColumnName("salary_range").HasConversion(salaryConverter).HasColumnType("jsonb"); 
                entity.Property(e => e.ExperienceRequired).HasColumnName("experience_required");
                entity.Property(e => e.ApplicationDeadline).HasColumnName("application_deadline");
                entity.Property(e => e.Status).HasColumnName("status").HasDefaultValue("open");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("now()");
            });
            base.OnModelCreating(modelBuilder);
            OnModelCreatingParitial(modelBuilder);

        }
        partial void OnModelCreatingParitial(ModelBuilder modelBuilder);
    }
}
