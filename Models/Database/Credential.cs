using System;
namespace PomeloAPI.Models
{
	public class Credential
	{
		public string client_id { get; set; } = null!;

		public string client_secret { get; set; } = null!;

		public string audience { get; set; } = null!;

		public string grant_type { get; set; } = null!;

	}
}

