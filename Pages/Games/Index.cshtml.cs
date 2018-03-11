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
    public class IndexModel : PageModel
    {
        private readonly CodeCompete.Data.ApplicationDbContext _context;

        public IndexModel(CodeCompete.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Game> Game { get;set; }

        public async Task OnGetAsync()
        {
            Game = await _context.Game
                .Include(g => g.ProgrammingLanguage)
                .Include(g => g.ApplicationUser)
                .ToListAsync();
        }
    }
}
