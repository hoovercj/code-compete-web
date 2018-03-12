using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CodeCompete.Data;

namespace CodeCompete.Pages.Players
{
    public class EditModel : PageModel
    {
        private readonly CodeCompete.Data.ApplicationDbContext _context;

        public List<SelectListItem> Languages { get; }
        public List<SelectListItem> Games { get; }

        public EditModel(CodeCompete.Data.ApplicationDbContext context)
        {
            _context = context;

            Languages = context.ProgrammingLanguage
                .Select(l => new SelectListItem
                {
                    Value = l.ProgrammingLanguageId.ToString(),
                    Text = l.Name,
                }).ToList();

            Games = context.Game
                .Select(l => new SelectListItem
                {
                    Value = l.GameId.ToString(),
                    Text = l.Name,
                }).ToList();
        }

        [BindProperty]
        public Player Player { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Player = await _context.Player
                .Include(p => p.ProgrammingLanguage)
                .Include(p => p.Game)
                .SingleOrDefaultAsync(m => m.PlayerId == id);

            if (Player == null)
            {
                return NotFound();
            }

            Languages.Where(l => l.Value == Player.ProgrammingLanguage.ProgrammingLanguageId.ToString()).FirstOrDefault().Selected = true;
            Games.Where(g => g.Value == Player.Game.GameId.ToString()).FirstOrDefault().Selected = true;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Player).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(Player.PlayerId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PlayerExists(int id)
        {
            return _context.Player.Any(e => e.PlayerId == id);
        }
    }
}
