using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CodeCompete.Data;

namespace CodeCompete.Pages.Matches
{
    public class CreateModel : PageModel
    {
        private readonly CodeCompete.Data.ApplicationDbContext _context;

        public List<SelectListItem> Players { get; }

        [BindProperty]
        public List<int> SelectedPlayers { get; set; } = new List<int>();

        public List<SelectListItem> Games { get; }

        [BindProperty]
        public int SelectedGame { get; set; } = 0;


        public CreateModel(CodeCompete.Data.ApplicationDbContext context)
        {
            _context = context;

            Games = context.Game
                .Select(g => new SelectListItem
                {
                    Value = g.GameId.ToString(),
                    Text = g.Name,
                }).ToList();

            Players = context.Player
                .Select(p => new SelectListItem
                {
                    Value = p.PlayerId.ToString(),
                    Text = p.Game.Name + " = " + p.Name,
                }).ToList();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Match Match { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (SelectedPlayers.Count == 0)
            {
                throw new ApplicationException("Cannot create a match without any players.");
            }

            Match.Game = _context.Game.Where(g => g.GameId == SelectedGame).FirstOrDefault();
            Match.PlayerMatches = new List<PlayerMatch>();
            foreach(int id in SelectedPlayers)
            {
                Player player = _context.Player.Where(p => p.PlayerId == id).FirstOrDefault();

                PlayerMatch playerMatch = new PlayerMatch()
                {
                    Player = player,
                    Match = Match
                };

                Match.PlayerMatches.Add(playerMatch);

                // player.PlayerMatches.Add(playerMatch);
                // Match.PlayerMatches.Add(playerMatch);
            }

            _context.Match.Add(Match);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}