using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeCompete.Data
{
    public class Game
    {
        public int GameId { get; set; }
        [Display(Name = "Created By")]
        public ApplicationUser ApplicationUser { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Display(Name = "Language")]
        public ProgrammingLanguage ProgrammingLanguage { get; set; }
        [Display(Name = "Source Code")]
        public string SourceCode { get; set; }
        public List<Player> Players { get; set; }
    }
}
