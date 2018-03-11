using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CodeCompete.Data;

namespace CodeCompete.Pages.Games
{
    public class CreateModel : PageModel
    {
        private readonly CodeCompete.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public List<SelectListItem> Languages { get; }

        [BindProperty]
        public int SelectedLanguage { get; set; } = 0;

        public CreateModel(
            CodeCompete.Data.ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            Languages = context.ProgrammingLanguage
                .Select(l => new SelectListItem
                {
                    Value = l.ProgrammingLanguageId.ToString(),
                    Text = l.Name,
                    Selected = l.ProgrammingLanguageId == SelectedLanguage
                }).ToList();
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Game Game { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Game.ApplicationUser = await _userManager.GetUserAsync(User);

            if (Game.ApplicationUser == null)
            {
                throw new ApplicationException("Cannot create a game with a user.");
            }

            Game.ProgrammingLanguage = _context.ProgrammingLanguage.Where(l => l.ProgrammingLanguageId == SelectedLanguage).FirstOrDefault();
            _context.Game.Add(Game);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}