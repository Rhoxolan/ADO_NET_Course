using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022._08._10_PW.Models
{
    public class Sale
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public virtual Game Game { get; set; } = null!;
    }
}
