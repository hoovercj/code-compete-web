using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeCompete.Data
{
    public static class SeedApplicationUsers
    {
        public async static void Initialize(IServiceProvider serviceProvider)
        {
            UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Users.Any())
                {
                    return;   // DB has been seeded
                }

                List<ApplicationUser> users = new List<ApplicationUser>
                {
                    new ApplicationUser { Email = "admin@example.com", UserName = "admin@example.com" },
                    new ApplicationUser { Email = "user@example.com", UserName = "user@example.com" },
                    new ApplicationUser { Email = "test@example.com", UserName = "test@example.com" },
                };

                foreach (ApplicationUser user in users)
                {
                    await userManager.CreateAsync(user, "Abc!123");
                }

                await context.SaveChangesAsync();
            }
        }
    }
}