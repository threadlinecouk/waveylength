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
			// Generate a unique 6-character lobby code
			var generatedCode = Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();

			// Store the lobby name (or any other data) in the dictionary
			InMemoryLobbies[generatedCode] = LobbyName;

			// Redirect to the Play page with the generated code as a parameter
			return RedirectToPage("Play", new { code = generatedCode });
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
