using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Waveylength.Models;
using System.Linq;

namespace Waveylength.Pages
{
	public class JoinRoomModel : PageModel
	{
		[BindProperty]
		public string PlayerName { get; set; }

		[BindProperty]
		public string RoomCode { get; set; }

		public IActionResult OnPost()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			if (!RoomStore.Rooms.ContainsKey(RoomCode))
			{
				ModelState.AddModelError("", "Room not found. Please check the room code.");
				return Page();
			}

			var room = RoomStore.Rooms[RoomCode];

			// If not already in the room, add the new player.
			if (!room.Players.Any(p => p.Name == PlayerName))
			{
				int team1Count = room.Players.Count(p => p.Team == 1);
				int team2Count = room.Players.Count(p => p.Team == 2);
				int teamAssignment = (team1Count <= team2Count) ? 1 : 2;
				room.Players.Add(new Player { Name = PlayerName, Team = teamAssignment });
			}

			// Redirect to Play page; the current user is the joiner.
			return RedirectToPage("Play", new { code = RoomCode, user = PlayerName, gametype = room.Gametype });
		}

	}
}
