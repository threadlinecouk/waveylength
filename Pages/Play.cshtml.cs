using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Collections.Generic;
using Waveylength.Models;

namespace Waveylength.Pages
{
	public class PlayModel : PageModel
	{
		[BindProperty(SupportsGet = true)]
		public string Code { get; set; }

		// Bind the current user's name from the query string ("user")
		[BindProperty(SupportsGet = true, Name = "user")]
		public string CurrentUser { get; set; }

		[BindProperty(SupportsGet = true)]
		public string Gametype { get; set; }

		// This property will hold the room's host.
		public string RoomHost { get; set; }

		public List<Player> Team1Players { get; set; }
		public List<Player> Team2Players { get; set; }

		public void OnGet()
		{
			if (RoomStore.Rooms.ContainsKey(Code))
			{
				var room = RoomStore.Rooms[Code];
				RoomHost = room.Host;
				Team1Players = room.Players.Where(p => p.Team == 1).ToList();
				Team2Players = room.Players.Where(p => p.Team == 2).ToList();
			}
			else
			{
				// Handle room not found scenario as needed.
			}
		}

		// Handler to move a player.
		public IActionResult OnPostMove(string RoomCode, string PlayerName)
		{
			if (RoomStore.Rooms.ContainsKey(RoomCode))
			{
				var room = RoomStore.Rooms[RoomCode];
				var player = room.Players.FirstOrDefault(p => p.Name == PlayerName);
				if (player != null)
				{
					player.Team = (player.Team == 1) ? 2 : 1;
				}
			}
			return RedirectToPage(new { code = RoomCode, user = CurrentUser, gametype = Gametype });
		}

		// Handler to kick a player.
		public IActionResult OnPostKick(string RoomCode, string PlayerName)
		{
			if (RoomStore.Rooms.ContainsKey(RoomCode))
			{
				var room = RoomStore.Rooms[RoomCode];
				var player = room.Players.FirstOrDefault(p => p.Name == PlayerName);
				if (player != null)
				{
					room.Players.Remove(player);
				}
			}
			return RedirectToPage(new { code = RoomCode, user = CurrentUser, gametype = Gametype });
		}
	}
}
