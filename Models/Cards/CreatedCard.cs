using System;
namespace PomeloAPI.Models
{
	public class CreatedCard
	{
		public string id { get; set; } = null!;
		public string affinity_group_id { get; set; } = null!;
		public string card_type { get; set; } = null!;
		public string product_type { get; set; } = null!;
		public string status { get; set; } = null!;
		public string shipment_id { get; set; } = null!;
		public string user_id { get; set; } = null!;
		public string start_date { get; set; } = null!;
		public string last_four { get; set; } = null!;
		public string provider { get; set; } = null!;
        public string affinity_group_name { get; set; } = null!;
	}

}

