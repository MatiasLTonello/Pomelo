using System;
namespace PomeloAPI.Models
{
	public class CredentialResult
    {

        public string access_token { get; set; } = null!;

        public string scope { get; set; } = null!;

        public int expires_in { get; set; } = 0;

        public string token_type { get; set; } = null!;
	}
}

