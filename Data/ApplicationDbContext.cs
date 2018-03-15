using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeCompete.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Game> Game { get; set; }
        public DbSet<Player> Player { get; set; }
        public DbSet<ProgrammingLanguage> ProgrammingLanguage { get; set; }
        public DbSet<Match> Match { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            // Set-up join entity for many-to-many between matches and players
            builder.Entity<PlayerMatch>()
                .HasKey(p => new { p.PlayerId, p.MatchId });

            builder.Entity<PlayerMatch>()
                .HasOne(pm => pm.Player)
                .WithMany(p => p.PlayerMatches)
                .HasForeignKey(pm => pm.PlayerId);

            builder.Entity<PlayerMatch>()
                .HasOne(pm => pm.Match)
                .WithMany(t => t.PlayerMatches)
                .HasForeignKey(pm => pm.MatchId);
        }
    }
}
