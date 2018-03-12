using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace CodeCompete.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            SeedApplicationUsers.Initialize(serviceProvider);
            SeedProgrammingLanguages.Initialize(serviceProvider);
            SeedGames.Initialize(serviceProvider);
            SeedPlayers.Initialize(serviceProvider);
        }
    }
}