using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CodeCompete.Data;

namespace CodeCompete.Pages.Players
{
    public class CreateModel : PageModel
    {
        private readonly CodeCompete.Data.ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public List<SelectListItem> Languages { get; }

        [BindProperty]
        public int SelectedLanguage { get; set; } = 0;

        public List<SelectListItem> Games { get; }

        [BindProperty]
        public int SelectedGame { get; set; } = 0;

        [BindProperty]
        public Player Player { get; set; }

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

            Games = context.Game
                .Select(l => new SelectListItem
                {
                    Value = l.GameId.ToString(),
                    Text = l.Name,
                    Selected = l.GameId == SelectedGame
                }).ToList();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Player.ApplicationUser = await _userManager.GetUserAsync(User);

            if (Player.ApplicationUser == null)
            {
                throw new ApplicationException("Cannot create a game with a user.");
            }

            Player.ProgrammingLanguage = _context.ProgrammingLanguage.Where(l => l.ProgrammingLanguageId == SelectedLanguage).FirstOrDefault();
            Player.Game = _context.Game.Where(l => l.GameId == SelectedGame).FirstOrDefault();

            _context.Player.Add(Player);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}