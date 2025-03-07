using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Waveylength.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Waveylength.Pages
{
	public class RoomSettingsModel : PageModel
	{
		[BindProperty(SupportsGet = true, Name = "code")]
		public string RoomCode { get; set; }

		[BindProperty, Required(ErrorMessage = "Your name is required.")]
		public string HostName { get; set; }

		[BindProperty]
		public string GameStyle { get; set; } = "Give Clue";

		public void OnGet()
		{
			// Optionally check if RoomCode is provided.
		}

		public IActionResult OnPost()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			// Create a new Room object.
			Room newRoom = new Room
			{
				Code = RoomCode,
				Host = HostName,       // Save the host's name.
				Gametype = GameStyle,
				Players = new List<Player>()
			};

			// Add the host as a player (on team 1).
			newRoom.Players.Add(new Player { Name = HostName, Team = 1 });

			// Store the room.
			RoomStore.Rooms[RoomCode] = newRoom;

			// Redirect to Play page with the current user set as the host.
			return RedirectToPage("Play", new { code = RoomCode, user = HostName, gametype = GameStyle });
		}

	}
}
