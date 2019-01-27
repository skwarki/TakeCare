using System;

namespace TakeCare.Droid.Models
{
	public class Event
	{
		public int Id { get; set; }
		public string DeviceId { get; set; }
		public int Type { get; set; }
		public string Location { get; set; }
		public string Description { get; set; }
		public byte[] Photo { get; set; }
		public DateTime IssuedDate { get; set; }
	}
}
