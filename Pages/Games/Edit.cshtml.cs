using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CodeCompete.Data;

namespace CodeCompete.Pages.Games
{
    public class EditModel : PageModel
    {
        private readonly CodeCompete.Data.ApplicationDbContext _context;

        public List<SelectListItem> Languages { get; }

        public EditModel(CodeCompete.Data.ApplicationDbContext context)
        {
            _context = context;

            Languages = context.ProgrammingLanguage
                .Select(l => new SelectListItem
                {
                    Value = l.ProgrammingLanguageId.ToString(),
                    Text = l.Name,
                }).ToList();
        }

        [BindProperty]
        public Game Game { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Game = await _context.Game
                .Include(g => g.ProgrammingLanguage)
                .SingleOrDefaultAsync(m => m.GameId == id);

            if (Game == null)
            {
                return NotFound();
            }

            Languages.Where(l => l.Value == Game.ProgrammingLanguage.ProgrammingLanguageId.ToString()).FirstOrDefault().Selected = true;


            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.Attach(Game).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(Game.GameId))
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

        private bool GameExists(int id)
        {
            return _context.Game.Any(e => e.GameId == id);
        }
    }
}
