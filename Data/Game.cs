using System;
using System.Collections.Generic;

namespace CodeCompete.Data
{
    public class Game
    {
        public ApplicationUser Author { get; set; }
        public int GameId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ProgrammingLanguage ProgrammingLanguage { get; set; }
        public string SourceCode { get; set; }
        public List<Player> Players { get; set; }
    }
}
