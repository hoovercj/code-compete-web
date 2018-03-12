using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeCompete.Data
{
    public static class SeedGames
    {
        public async static void Initialize(IServiceProvider serviceProvider)
        {
            UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Game.Any())
                {
                    return;   // DB has been seeded
                }

                List<ApplicationUser> users = await context.Users.ToListAsync();
                List<ProgrammingLanguage> languages = await context.ProgrammingLanguage.ToListAsync();

                await context.Game.AddRangeAsync(
                    new Game
                    {
                        Name = "Tic tac toe",
                        Description = "Oldie but goodie",
                        ProgrammingLanguage = languages[0],
                        ApplicationUser = users[0],
                        SourceCode = "function() { console.log('X's Win!); }"
                    },
                    new Game
                    {
                        Name = "Battleship",
                        Description = "What movie?",
                        ProgrammingLanguage = languages[2],
                        ApplicationUser = users[0],
                        SourceCode = "Console.WriteLine('You sank my battleship');"
                    },
                    new Game
                    {
                        Name = "Connect 4",
                        ProgrammingLanguage = languages[1],
                        ApplicationUser = users[1],
                        SourceCode = "TicTacToe Game = (new EnterpriseTicTacToeFactoryBuilder()).Create();"
                    },
                    new Game
                    {
                        Name = "Hangman",
                        Description = "Guess again",
                        ProgrammingLanguage = languages[3],
                        ApplicationUser = users[2],
                    },
                    new Game
                    {
                        Name = "Mastermind",
                        Description = "Can you crack the code?",
                        ProgrammingLanguage = languages[0],
                        ApplicationUser = users[0],
                        SourceCode = "function() { console.log('Guess again'); }"
                    }
                );

                await context.SaveChangesAsync();
            }
        }
    }
}