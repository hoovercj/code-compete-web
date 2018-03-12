using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CodeCompete.Data;

namespace CodeCompete.Pages.Players
{
    public class DetailsModel : PageModel
    {
        private readonly CodeCompete.Data.ApplicationDbContext _context;

        public DetailsModel(CodeCompete.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Player Player { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Player = await _context.Player
                .Include(p => p.ApplicationUser)
                .Include(p => p.ProgrammingLanguage)
                .Include(p => p.Game)
                .SingleOrDefaultAsync(p => p.PlayerId == id);

            if (Player == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
