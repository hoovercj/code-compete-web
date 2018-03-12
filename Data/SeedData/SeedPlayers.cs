using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeCompete.Data
{
    public static class SeedPlayers
    {
        public async static void Initialize(IServiceProvider serviceProvider)
        {
            UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Player.Any())
                {
                    return;   // DB has been seeded
                }

                List<ApplicationUser> users = context.Users.ToList();
                List<ProgrammingLanguage> languages = context.ProgrammingLanguage.ToList();
                List<Game> games = context.Game.ToList();

                context.Player.AddRange(
                    new Player
                    {
                        Name = "Simple Player",
                        Description = "The basics",
                        ProgrammingLanguage = languages[0],
                        Game = games[0],
                        ApplicationUser = users[0],
                        SourceCode = "function() { console.log('I Win!); }"
                    },
                    new Player
                    {
                        Name = "Boss",
                        Description = "The final boss for this game",
                        ProgrammingLanguage = languages[2],
                        Game = games[0],
                        ApplicationUser = users[0],
                        SourceCode = "Console.WriteLine('Tic'ed. Tac'ed. Toe'ed.');"
                    },
                    new Player
                    {
                        Name = "The Connector",
                        ProgrammingLanguage = languages[1],
                        Game = games[1],
                        ApplicationUser = users[1],
                    }
                );

                context.SaveChanges();
            }
        }
    }
}