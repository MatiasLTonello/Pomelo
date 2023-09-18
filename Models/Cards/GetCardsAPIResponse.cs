using System;
using PomeloAPI.Models;

namespace PomeloAPI.Models
{
	public class GetCardsAPIResponse
	{
        public List<CreatedCard> data { get; set; } = null!;
        public MetaData meta { get; set; } = null!;
    }
}

