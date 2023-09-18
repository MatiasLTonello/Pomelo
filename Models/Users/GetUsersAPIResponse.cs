using System;
namespace PomeloAPI.Models
{
    public class GetUsersAPIResponse
    {
        public List<UserData> data { get; set; } = null!;
        public MetaData meta { get; set; } = null!;
    }

    public class MetaData
    {
        public Pagination pagination { get; set; } = null!;
        public List<Filter> filter { get; set; } = null!;
    }

    public class Pagination
    {
        public int total_pages { get; set; }
        public int current_page { get; set; }
    }

    public class Filter
    {
        public string key { get; set; } = null!;
        public List<string> value { get; set; } = null!;
    }

}

