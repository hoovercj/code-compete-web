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
        public ProgrammingLanguage ProgrammingLanguage { get; set; }
        public string SourceCode { get; set; }
        public Game Game { get; set; }
    }
}
