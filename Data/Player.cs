using System;
using System.Collections.Generic;

namespace CodeCompete.Data
{
    public class Player
    {
        public ApplicationUser Author { get; set; }
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ProgrammingLanguage ProgrammingLanguage { get; set; }
        public string SourceCode { get; set; }
        public Game Game { get; set; }
    }
}
