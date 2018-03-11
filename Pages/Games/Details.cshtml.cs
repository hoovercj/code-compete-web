using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CodeCompete.Data;

namespace CodeCompete.Pages.Games
{
    public class DetailsModel : PageModel
    {
        private readonly CodeCompete.Data.ApplicationDbContext _context;

        public DetailsModel(CodeCompete.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Game Game { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Game = await _context.Game
                .Include(g => g.ApplicationUser)
                .Include(g => g.ProgrammingLanguage)
                .SingleOrDefaultAsync(m => m.GameId == id);

            if (Game == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
