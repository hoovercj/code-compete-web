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
    public class DeleteModel : PageModel
    {
        private readonly CodeCompete.Data.ApplicationDbContext _context;

        public DeleteModel(CodeCompete.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Match = await _context.Match
                .Include(m => m.Game)
                .Include(m => m.PlayerMatches)
                .ThenInclude(m => m.Player)
                .FirstAsync(m => m.MatchId == id);

            if (Match != null)
            {
                Match.Game.Matches.Remove(Match);
                _context.Game.Update(Match.Game);

                foreach (PlayerMatch p in Match.PlayerMatches)
                {
                    p.Player.PlayerMatches.Remove(p);
                    _context.Player.Update(p.Player);
                }
                Match.PlayerMatches.Clear();
                _context.Match.Remove(Match);

                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
