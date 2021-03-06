using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CodeCompete.Data;

namespace CodeCompete.Pages.Matches
{
    public class DetailsModel : PageModel
    {
        private readonly CodeCompete.Data.ApplicationDbContext _context;

        public DetailsModel(CodeCompete.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Match Match { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Match = await _context.Match
                .Include(m => m.Game)
                .Include(m => m.PlayerMatches)
                .ThenInclude(m => m.Player)
                .SingleOrDefaultAsync(m => m.MatchId == id);

            if (Match == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
