using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ApplicationConfig
{
    public class Common
    {
        public DateTime?Created_at { get; set; }
        public DateTime? Updated_at { get; set; } = DateTime.UtcNow;

        public string? Created_by { get; set; }
        public string? Updated_by { get; set; }
        public bool ActiveFlag { get; set; } = true;

    }
}
