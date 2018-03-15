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
    public class IndexModel : PageModel
    {
        private readonly CodeCompete.Data.ApplicationDbContext _context;

        public IndexModel(CodeCompete.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Match> Match { get;set; }

        public async Task OnGetAsync()
        {
            Match = await _context.Match
                .Include(m => m.Game)
                .Include(m => m.PlayerMatches)
                .ThenInclude(m => m.Player)
                .ToListAsync();
        }
    }
}
