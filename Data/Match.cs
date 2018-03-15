using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeCompete.Data
{
    public class Match
    {
        public int MatchId { get; set; }
        public Game Game { get; set; }
        public string Result { get; set; }
        [Display(Name = "Players")]
        public List<PlayerMatch> PlayerMatches { get; set; }
    }
}
