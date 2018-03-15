using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeCompete.Data
{
    public class Player
    {
        [Display(Name = "Created By")]
        public ApplicationUser ApplicationUser { get; set; }
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Display(Name = "Language")]
        public ProgrammingLanguage ProgrammingLanguage { get; set; }
        [Display(Name = "Source Code")]
        public string SourceCode { get; set; }
        public Game Game { get; set; }
        [Display(Name = "Matches")]
        public List<PlayerMatch> PlayerMatches { get; set; }
    }
}
