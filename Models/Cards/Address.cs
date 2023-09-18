using System;
namespace PomeloAPI.Models
{
	public class Address
	{
        public string? street_name { get; set; } = null;

        public int? street_number { get; set; } = null;

        public int? floor { get; set; } = null;

        public string? apartment { get; set; } = null;

        public int? zip_code { get; set; } = null;

        public string? neighborhood { get; set; } = null;

        public string? city { get; set; } = null;

        public string? region { get; set; } = null;

        public string? additional_info { get; set; } = null;

        public string? country { get; set; } = null;


    }
}

