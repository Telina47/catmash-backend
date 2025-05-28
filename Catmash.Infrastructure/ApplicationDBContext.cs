using Catmash.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catmash.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Cat> Cats => Set<Cat>();
        public DbSet<Vote> Votes => Set<Vote>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Cat
            builder.Entity<Cat>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).IsRequired();
                entity.Property(c => c.ImageUrl).IsRequired();
                entity.Property(c => c.Score).HasDefaultValue(1000);
                entity.Property(c => c.Wins).HasDefaultValue(0);
                entity.Property(c => c.Losses).HasDefaultValue(0);
            });

            // Vote
            builder.Entity<Vote>(entity =>
            {
                entity.HasKey(v => v.Id);
                entity.Property(v => v.VoterId).IsRequired();
                entity.Property(v => v.WinnerCatId).IsRequired();
                entity.Property(v => v.LoserCatId).IsRequired();
                entity.Property(v => v.VotedAt).IsRequired();

                entity.HasIndex(v => v.VoterId);
            });

            // ApplicationUser (Identity)
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(u => u.Nom).IsRequired();
                entity.Property(u => u.Prenom).IsRequired();
            });
        }
    }
}
