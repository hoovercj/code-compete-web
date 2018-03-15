using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeCompete.Data
{
    public class PlayerMatch
    {
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public int MatchId { get; set; }
        public Match Match { get; set; }
    }
}
