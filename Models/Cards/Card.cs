using System;
namespace PomeloAPI.Models
{
	public class Card
	{
		public string user_id { get; set; } = null!;
		public string affinity_group_id { get; set; } = null!;
		public string card_type { get; set; } = null!;
        public Address? address { get; set; } = new Address();
        public string? previous_card_id = null;
        public string? pin { get; set; } = null;
    }
}

