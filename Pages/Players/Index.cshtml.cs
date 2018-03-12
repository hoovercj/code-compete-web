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
    public class IndexModel : PageModel
    {
        private readonly CodeCompete.Data.ApplicationDbContext _context;

        public IndexModel(CodeCompete.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Player> Player { get;set; }

        public async Task OnGetAsync()
        {
            Player = await _context.Player
                .Include(p => p.ProgrammingLanguage)
                .Include(p => p.Game)
                .Include(p => p.ApplicationUser)
                .ToListAsync();
        }
    }
}
