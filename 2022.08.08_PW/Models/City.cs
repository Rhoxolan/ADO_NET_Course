using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022._08._08_PW.Models
{
    public class City
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public virtual Country Country { get; set; } = null!;

        public virtual ICollection<Publisher>? Publishers { get; set; }
    }
}
