using Microsoft.EntityFrameworkCore;
using SB.Domain.Entities;
using SB.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Infrastructure
{
    public class SupabaseDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public SupabaseDbContext(DbContextOptions<SupabaseDbContext> options) : base(options) { }

        public DbSet<UserProfile> UserProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfile>().ToTable("user_profiles");
            modelBuilder.Entity<Name>().HasNoKey();
            base.OnModelCreating(modelBuilder);
        }
    }
}
