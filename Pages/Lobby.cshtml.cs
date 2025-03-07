using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Waveylength.Pages
{
	public class LobbyModel : PageModel
	{
		// BindProperty allows form fields to be automatically bound to these properties
		[BindProperty]
		public string LobbyName { get; set; }

		[BindProperty]
		public string LobbyCode { get; set; }

		public void OnGet()
		{
			// Triggered on GET requests (e.g., when first navigating to the page)
		}

		public IActionResult OnPostCreateLobby()
		{
			var generatedCode = Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();
			return RedirectToPage("RoomSettings", new { code = generatedCode });
		}




		public IActionResult OnPostJoinLobby()
		{
			if (InMemoryLobbies.ContainsKey(LobbyCode))
			{
				// Lobby exists; redirect to the Play page
				return RedirectToPage("Play", new { code = LobbyCode });
			}
			else
			{
				// Lobby does not exist; add an error message to be shown on the page
				ModelState.AddModelError("", "Lobby code not found.");
				return Page();
			}
		}


		private static Dictionary<string, string> InMemoryLobbies = new Dictionary<string, string>();

	}
}
