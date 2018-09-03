using System.Collections.Generic;

namespace Web.Controllers
{
    public class UserLoadParams
    {
        public int Page { get; set; } = 1;

        public Dictionary<string, string> FilterParams { get; set; }
    }
}