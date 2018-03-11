using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace CodeCompete.Data
{
    public static class SeedProgrammingLanguages
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any movies.
                if (context.ProgrammingLanguage.Any())
                {
                    return;   // DB has been seeded
                }

                context.ProgrammingLanguage.AddRange(
                    new ProgrammingLanguage
                    {
                        Name = "Javascript"
                    },
                    new ProgrammingLanguage
                    {
                        Name = "Java"
                    },
                    new ProgrammingLanguage
                    {
                        Name = "C#"
                    },
                    new ProgrammingLanguage
                    {
                        Name = "Ruby"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}