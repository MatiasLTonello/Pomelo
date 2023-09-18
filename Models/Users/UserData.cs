using System;
namespace PomeloAPI.Models
{
    public class UserData
    {
        public string id { get; set; } = null!;
        public string name { get; set; } = null!;
        public string surname { get; set; } = null!;
        public string identification_type { get; set; } = null!;
        public int identification_value { get; set; } = 0;
        public DateTime birthdate { get; set; } = DateTime.Now;
        public string gender { get; set; } = null!;
        public string email { get; set; } = null!;
        public string phone { get; set; } = null!;
        public string tax_identification_type { get; set; } = null!;
        public long tax_identification_value { get; set; } = 0;
        public string nationality { get; set; } = null!;
        public string status { get; set; } = null!;
        public string operation_country { get; set; } = null!;
        public Address legal_address { get; set; } = null!;
    }



}

