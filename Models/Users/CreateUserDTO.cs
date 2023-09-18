using System;
namespace PomeloAPI.Models
{
    public class CreateUserDTO
    {
        public string name { get; set; } = null!;
        public string surname { get; set; } = null!;
        public string gender { get; set; } = null!;
        public string email { get; set; } = null!;
        public string phone { get; set; } = null!;
        public string nationality { get; set; } = null!;
        public string birthdate { get; set; } = null!;
        public LegalAddressDTO legal_address { get; set; } = null!;
        public string operation_country { get; set; } = null!;
    }

    public class LegalAddressDTO
    {
        public string street_name { get; set; } = null!;
        public int street_number { get; set; } = 0;
        public int floor { get; set; } = 0;
        public string apartment { get; set; } = null!;
        public int zip_code { get; set; } = 0;
        public string neighborhood { get; set; } = null!;
        public string city { get; set; } = null!;
        public string region { get; set; } = null!;
        public string additional_info { get; set; } = null!;
        public string country { get; set; } = null!;
    }
}

