using System.Collections.Generic;

namespace Waveylength.Models
{
	public static class RoomStore
	{
		// Key: Room Code; Value: Room details
		public static Dictionary<string, Room> Rooms = new Dictionary<string, Room>();
	}

	public class Room
	{
		public string Code { get; set; }
		public string Host { get; set; }
		public string Gametype { get; set; }
		public List<Player> Players { get; set; } = new List<Player>();
	}

	public class Player
	{
		public string Name { get; set; }
		public int Team { get; set; } // 1 or 2
	}
}
